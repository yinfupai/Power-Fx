//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.PowerFx.Core.Public.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.PowerFx.Tests
{
    [TestClass]
    public class TypeTests
    {
        [TestMethod]
        public void RecordType()
        {
            RecordType r1 = new RecordType()
                .Add(new NamedFormulaType("Num", FormulaType.Number))
                .Add(new NamedFormulaType("B", FormulaType.Boolean));

            RecordType r2 = new RecordType()
                .Add(new NamedFormulaType("B", FormulaType.Boolean))
                .Add(new NamedFormulaType("Num", FormulaType.Number));

            // Structural equivalence
            Assert.AreEqual(r1, r2);

            // Test op==
            Assert.IsTrue(r1 == r2);
            Assert.IsFalse(null == r2);
            Assert.IsFalse(r1 == null);

            Assert.IsTrue(r1 != null);
            Assert.IsTrue(null != r1);
            Assert.IsFalse(r1 != r2);

            Assert.IsTrue(r1.Equals(r2));
            Assert.IsFalse(r1.Equals(null));


            Assert.AreEqual(r1.GetHashCode(), r2.GetHashCode());
        }        
    }
}