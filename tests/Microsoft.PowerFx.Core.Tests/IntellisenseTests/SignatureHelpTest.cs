//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.IO;
using Microsoft.PowerFx.Core.Texl.Intellisense.SignatureHelp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerFx.Tests.IntellisenseTests
{
    [TestClass]
    public class SignatureHelpTest : IntellisenseTestBase
    {
        static bool RegenerateSignatureHelp = false;

        /// <summary>
        /// The current test id.  This is incremented by <see cref="CheckSignatureHelpTest"/> each time it is
        /// called
        /// </summary>
        static int SignatureHelpId;

        /// <summary>
        /// Resolves to the directory in the src folder that corresponds to the current directory, which may
        /// instead include the subpath bin/(Debug|Release).AnyCPU, depending on whether the assembly was
        /// built in debug or release mode
        /// </summary>
        private static string _signatureHelpDirectory = Path.Join(Directory.GetCurrentDirectory(), "IntellisenseTests", "TestSignatures")
            .Replace(Path.Join("bin", "Debug.AnyCPU"), "src")
            .Replace(Path.Join("bin", "Release.AnyCPU"), "src");

        /// <summary>
        /// We want the source directory wherein the tests are located, instead of the bin directory from
        /// where the tests are run.
        /// Because <see cref="SignatureHelpId"/> will change dependent on how many tests had been run,
        /// we use a method here as opposed to a static value.
        /// </summary>
        /// <returns>
        /// The path of the current test's expected output
        /// </returns>
        private static string GetSignatureHelpPath() => Path.Join(_signatureHelpDirectory, SignatureHelpId + ".json");

        /// <summary>
        /// Reads the current signature help test, located in the TestSignatures directory, deserializes and
        /// asserts and compares its value with <see cref="SignatureHelp"/>, then increments the index,
        /// <see cref="SignatureHelpId"/>.
        /// </summary>
        /// <param name="signatureHelp">
        /// Signature help value to test
        /// </param>
        private void CheckSignatureHelpTest(SignatureHelp signatureHelp)
        {
            var directory = _signatureHelpDirectory;
            var signatureHelpPath = GetSignatureHelpPath();

            SignatureHelpId++;

            if (File.Exists(signatureHelpPath))
            {
                var expectedSignatureHelp = ReadSignatureHelpFile(signatureHelpPath);
                var actualSignatureHelp = SerializeSignatureHelp(signatureHelp);

                if (RegenerateSignatureHelp)
                {
                    if (!JToken.DeepEquals(actualSignatureHelp, expectedSignatureHelp))
                    {
                        WriteSignatureHelp(signatureHelpPath, signatureHelp);
                    }
                }
                else
                {
                    Assert.IsTrue(JToken.DeepEquals(actualSignatureHelp, expectedSignatureHelp));
                }
            }
            else
            {
                Assert.IsTrue(RegenerateSignatureHelp, "Snapshot regeneration must be explicitly enabled to make new snapshot tests. Target path is: " + signatureHelpPath);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                WriteSignatureHelp(signatureHelpPath, signatureHelp);
            }
        }

        private JObject ReadSignatureHelpFile(string signatureHelpPath) => JObject.Parse(File.ReadAllText(signatureHelpPath));
        private JObject SerializeSignatureHelp(SignatureHelp signatureHelp) => JObject.Parse(JsonConvert.SerializeObject(signatureHelp));
        private void WriteSignatureHelp(string path, SignatureHelp signatureHelp) => File.WriteAllText(path, JsonConvert.SerializeObject(signatureHelp, Formatting.Indented));

        /// <summary>
        /// These use json value comparisons to test the signature help output of
        /// Intellisense.<see cref="Suggest"/>.  The expected value of each test is stored in ./TestSignatures,
        /// which are named according to the order in which they appear below.
        /// </summary>
        /// <param name="expression"></param>
        [DataTestMethod, Owner("jokellih")]
        [DataRow("ForAll(|")]
        [DataRow("Filter(|")]
        [DataRow("Filter([{Value:\"test\"}],|")]
        [DataRow("Filter([{Value:\"test\"}],Value=\"\",|")]
        [DataRow("Filter([{Column1: 0, Column2: 0, Column3: 0}], 0, Column1, Column2, Column3|")]
        [DataRow("ForAll([0,1,2,3], Value + 1, Value + 1, Value + 1|")]
        [DataRow("ForAll([0,1,2,3], Value + 1, Value +| 1, Value + 1")]
        [DataRow("If(true, If(true, 0, 1|))")]
        [DataRow("If(true, If(true, 0, 1)|)")]
        [DataRow("Filter|")]
        [DataRow("|")]
        public void TestSignatureHelp(string expression) => CheckSignatureHelpTest(Suggest(expression).SignatureHelp);

        [TestMethod, Owner("jokellih")]
        public void TestRegenerateSignatureHelpIsOff() => Assert.IsFalse(RegenerateSignatureHelp);
    }
}