//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.PowerFx.Tests
{
    [TestClass]
    public class RecalcEngineTests
    {
        [TestMethod]
        public void PublicSurfaceTests()
        {
            var asm = typeof(RecalcEngine).Assembly;

            var ns = "Microsoft.PowerFx";
            HashSet<string> allowed = new HashSet<string>()
            {
                $"{ns}.{nameof(RecalcEngine)}",
                $"{ns}.{nameof(ReflectionFunction)}",
                $"{ns}.{nameof(PowerFxFunction)}",
                $"{ns}.{nameof(RecalcEngineScope)}"
            };

            StringBuilder sb = new StringBuilder();
            foreach (var type in asm.GetTypes().Where(t => t.IsPublic))
            {
                var name = type.FullName;
                if (!allowed.Contains(name))
                {
                    sb.Append(name);
                    sb.Append("; ");
                }

                allowed.Remove(name);
            }

            Assert.AreEqual(0, sb.Length, $"Unexpected public types: {sb}");

            // Types we expect to be in the assembly aren't there. 
            if (allowed.Count > 0)
            {
                Assert.Fail("Types missing: " + string.Join(",", allowed.ToArray()));
            }
        }


        [TestMethod]
        public void EvalWithGlobals()
        {
            var engine = new RecalcEngine();

            var context = FormulaValue.NewRecord(new
            {
                x = 15
            });
            var result = engine.Eval("With({y:2}, x+y)", context);
            
            Assert.AreEqual(17.0, ((NumberValue)result).Value);
        }

        [TestMethod]
        public void BasicRecalc()
        {
            var engine = new RecalcEngine();
            engine.UpdateVariable("A", 15);
            engine.SetFormula("B", "A*2", OnUpdate);
            AssertUpdate("B-->30;");

            engine.UpdateVariable("A", 20);
            AssertUpdate("B-->40;");

            // Ensure we can update to null. 
            engine.UpdateVariable("A", FormulaValue.NewBlank(FormulaType.Number));
            AssertUpdate("B-->0;");
        }

        // depend on grand child directly 
        [TestMethod]
        public void Recalc2()
        {
            var engine = new RecalcEngine();
            engine.UpdateVariable("A", 1);
            engine.SetFormula("B", "A*10", OnUpdate);
            AssertUpdate("B-->10;");

            engine.SetFormula("C", "B+5", OnUpdate); 
            AssertUpdate("C-->15;");

            // depend on grand child directly 
            engine.SetFormula("D", "B+A", OnUpdate);
            AssertUpdate("D-->11;");

            // Updating A will recalc both D and B. 
            // But D also depends on B, so verify D pulls new value of B. 
            engine.UpdateVariable("A", 2);

            // Batched up (we don't double fire)            
            AssertUpdate("B-->20;C-->25;D-->22;");
        }

        // Don't fire for formulas that aren't touched by an update
        [TestMethod]
        public void RecalcNoExtraCallbacks()
        {
            var engine = new RecalcEngine();
            engine.UpdateVariable("A1", 1);
            engine.UpdateVariable("A2", 5);

            engine.SetFormula("B", "A1+A2", OnUpdate);
            AssertUpdate("B-->6;");

            engine.SetFormula("C", "A2*10", OnUpdate);
            AssertUpdate("C-->50;");
                        
            engine.UpdateVariable("A1", 2);
            AssertUpdate("B-->7;"); // Don't fire C, not touched

            engine.UpdateVariable("A2", 7);
            AssertUpdate("B-->9;C-->70;");
        }

        [TestMethod]
        public void BasicEval()
        {
            var engine = new RecalcEngine();
            engine.UpdateVariable("M", 10.0);
            engine.UpdateVariable("M2", -4);
            var result = engine.Eval("M + Abs(M2)");
            Assert.AreEqual(14.0, ((NumberValue)result).Value);
        }

        [TestMethod]
        public void FormulaErrorUndefined()
        {
            var engine = new RecalcEngine();

            // formula fails since 'B' is undefined. 
            Assert.ThrowsException<InvalidOperationException>(() =>
               engine.SetFormula("A", "B*2", OnUpdate));
        }

        [TestMethod]
        public void CantChangeType()
        {
            var engine = new RecalcEngine();
            engine.UpdateVariable("a", FormulaValue.New(12));

            // not supported: Can't change a variable's type.
            Assert.ThrowsException<NotSupportedException>(() =>
                engine.UpdateVariable("a", FormulaValue.New("str"))
            );

        }

        [TestMethod]
        public void FormulaCantRedefine()
        {
            var engine = new RecalcEngine();

            engine.SetFormula("A", "2", OnUpdate);

            // Can't redefine an existing formula. 
            Assert.ThrowsException<InvalidOperationException>(() =>
               engine.SetFormula("A", "3", OnUpdate));
        }

        [TestMethod]
        public void PropagateNull()
        {
            var engine = new RecalcEngine();
            engine.SetFormula("A", expr: "Blank()", OnUpdate);
            engine.SetFormula("B", "A", OnUpdate);

            var b = engine.GetValue("B");
            Assert.IsTrue(b is BlankValue);
        }

        // Record with null values. 
        [TestMethod]
        public void ChangeRecord()
        {
            var engine = new RecalcEngine();

            engine.UpdateVariable("R", FormulaValue.RecordFromFields(
                new NamedValue("F1", FormulaValue.NewBlank(FormulaType.Number)),
                new NamedValue("F2", FormulaValue.New(6))));

            engine.SetFormula("A", "R.F2 + 3 + R.F1", OnUpdate);
            AssertUpdate("A-->9;");

            engine.UpdateVariable("R", FormulaValue.RecordFromFields(
                new NamedValue("F1", FormulaValue.New(2)),
                new NamedValue("F2", FormulaValue.New(7))));
            AssertUpdate("A-->12;");
        }

        [TestMethod]
        public void CheckSuccess()
        {
            var engine = new RecalcEngine();
            var result = engine.Check("3*2+x",
                new RecordType().Add(
                    new NamedFormulaType("x", FormulaType.Number)));

            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.ReturnType is NumberType);
            Assert.AreEqual(1, result.TopLevelIdentifiers.Count);
            Assert.AreEqual("x", result.TopLevelIdentifiers.First());
        }

        [TestMethod]
        public void CheckParseError()
        {
            var engine = new RecalcEngine();
            var result = engine.Check("3*1+");

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(1, result.Errors.Length);
            Assert.IsTrue(result.Errors[0].ToString().StartsWith(
                "Error 4-4: Expected an operand"));
        }

        [TestMethod]
        public void CheckBindError()
        {
            var engine = new RecalcEngine();
            var result = engine.Check("3+foo+2"); // foo is undefined 

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(1, result.Errors.Length);
            Assert.IsTrue(result.Errors[0].ToString().StartsWith(
                "Error 2-5: Name isn't valid. This identifier isn't recognized"));
        }

        [TestMethod]
        public void CheckBindEnum()
        {
            var engine = new RecalcEngine();
            var result = engine.Check("TimeUnit.Hours");

            Assert.IsTrue(result.IsSuccess);

            // The resultant type will be the underlying type of the enum provided to 
            // check.  In the case of TimeUnit, this is StringType
            Assert.IsTrue(result.ReturnType is StringType);
            Assert.AreEqual(0, result.TopLevelIdentifiers.Count);
        }

        #region Test

        StringBuilder _updates = new StringBuilder();


        void AssertUpdate(string expected)
        {
            Assert.AreEqual(expected, _updates.ToString());            
            _updates.Clear();
        }

        void OnUpdate(string name, FormulaValue newValue)
        {
            var str = newValue.ToObject()?.ToString();

            _updates.Append($"{name}-->{str};");
        }
        #endregion
    }
}
