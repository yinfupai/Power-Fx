//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core;
using Microsoft.PowerFx.Core.Public;
using Microsoft.PowerFx.Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.PowerFx.Tests
{
    [TestClass]
    public class GetTokensUtilsTest
    {
        [TestMethod]
        public void GetTokensTest()
        {
            var scope = RecalcEngineScope.FromJson(new RecalcEngine(), "{\"A\":1,\"B\":[1,2,3]}");
            var checkResult = scope.Check("A+CountRows(B)");

            var result = GetTokensUtils.GetTokens(checkResult._binding, GetTokensFlags.None);
            Assert.AreEqual(0, result.Count);

            result = GetTokensUtils.GetTokens(checkResult._binding, GetTokensFlags.UsedInExpression);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(TokenResultType.Variable, result["A"]);
            Assert.AreEqual(TokenResultType.Variable, result["B"]);
            Assert.AreEqual(TokenResultType.Function, result["CountRows"]);

            result = GetTokensUtils.GetTokens(checkResult._binding, GetTokensFlags.AllFunctions);
            Assert.IsFalse(result.ContainsKey("A"));
            Assert.IsFalse(result.ContainsKey("B"));
            Assert.AreEqual(TokenResultType.Function, result["Abs"]);
            Assert.AreEqual(TokenResultType.Function, result["CountRows"]);
            Assert.AreEqual(TokenResultType.Function, result["Year"]);

            result = GetTokensUtils.GetTokens(checkResult._binding, GetTokensFlags.UsedInExpression | GetTokensFlags.AllFunctions);
            Assert.AreEqual(TokenResultType.Variable, result["A"]);
            Assert.AreEqual(TokenResultType.Variable, result["B"]);
            Assert.AreEqual(TokenResultType.Function, result["Abs"]);
            Assert.AreEqual(TokenResultType.Function, result["Year"]);
            Assert.AreEqual(TokenResultType.Function, result["CountRows"]);
        }

        [TestMethod]
        public void GetTokensFromBadFormulaTest()
        {
            var scope = RecalcEngineScope.FromJson(new RecalcEngine(), "{\"A\":1,\"B\":[1,2,3]}");
            var checkResult = scope.Check("A + CountRows(B) + C + NoFunction(123)");

            var result = GetTokensUtils.GetTokens(checkResult._binding, GetTokensFlags.None);
            Assert.AreEqual(0, result.Count);

            result = GetTokensUtils.GetTokens(checkResult._binding, GetTokensFlags.UsedInExpression);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(TokenResultType.Variable, result["A"]);
            Assert.AreEqual(TokenResultType.Variable, result["B"]);
            Assert.AreEqual(TokenResultType.Function, result["CountRows"]);
            Assert.IsFalse(result.ContainsKey("C"));
            Assert.IsFalse(result.ContainsKey("NoFunction"));

            result = GetTokensUtils.GetTokens(checkResult._binding, GetTokensFlags.AllFunctions);
            Assert.IsFalse(result.ContainsKey("A"));
            Assert.IsFalse(result.ContainsKey("B"));
            Assert.AreEqual(TokenResultType.Function, result["Abs"]);
            Assert.AreEqual(TokenResultType.Function, result["CountRows"]);
            Assert.AreEqual(TokenResultType.Function, result["Year"]);
            Assert.IsFalse(result.ContainsKey("C"));
            Assert.IsFalse(result.ContainsKey("NoFunction"));

            result = GetTokensUtils.GetTokens(checkResult._binding, GetTokensFlags.UsedInExpression | GetTokensFlags.AllFunctions);
            Assert.AreEqual(TokenResultType.Variable, result["A"]);
            Assert.AreEqual(TokenResultType.Variable, result["B"]);
            Assert.AreEqual(TokenResultType.Function, result["Abs"]);
            Assert.AreEqual(TokenResultType.Function, result["Year"]);
            Assert.AreEqual(TokenResultType.Function, result["CountRows"]);
            Assert.IsFalse(result.ContainsKey("C"));
            Assert.IsFalse(result.ContainsKey("NoFunction"));
        }
    }
}