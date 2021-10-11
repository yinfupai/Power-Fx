//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.PowerFx.Core.Tests;
using System.Threading.Tasks;

namespace Microsoft.PowerFx.Interpreter.Tests
{
    [TestClass]

    public class ExpressionEvaluationTests
    {
        [TestMethod]
        public void RunInterpreterTestCases()
        {
            var runner = new TestRunner(new InterpreterRunner());
            runner.AddDir();
            var result = runner.RunTests();

            // This number should go to 0 over time
            Assert.AreEqual(166, result.failed);
        }

        // Use this for local testing of a single testcase (uncomment "TestMethod")
        // [TestMethod]
        public void RunSingleTestCase()
        {
            var runner = new TestRunner(new InterpreterRunner());
            runner.AddFile("Testing.txt");
            var result = runner.RunTests();

            Assert.AreEqual(0, result.failed);
        }

        internal class InterpreterRunner : BaseRunner
        {
            private RecalcEngine _engine = new RecalcEngine();

            public override Task<FormulaValue> RunAsync(string expr)
            {
                var result = _engine.Eval(expr);
                return Task.FromResult(result);
            }
        }
    }
}
