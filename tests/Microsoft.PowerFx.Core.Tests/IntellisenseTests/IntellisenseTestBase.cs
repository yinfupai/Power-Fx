//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AppMagic.Authoring;
using Microsoft.AppMagic.Authoring.Texl;
using Microsoft.PowerPlatform.Language;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.PowerFx.Tests.IntellisenseTests
{
    /// <summary>
    /// Provides methods that may be used by Intellisense tests
    /// </summary>
    public class IntellisenseTestBase
    {
        /// <summary>
        /// This method receives a test case string, along with an optional context type that defines the valid
        /// names and types in the expression and invokes Intellisense.Suggest on it, and returns a the result
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="contextTypeString"></param>
        /// <returns></returns>
        internal IIntellisenseResult Suggest(string expression, string contextTypeString = null)
        {
            Assert.IsNotNull(expression);

            var cursorMatches = Regex.Matches(expression, @"\|");
            Assert.IsTrue(cursorMatches.Count == 1, "Invalid cursor.  Exactly one cursor must be specified.");
            var cursorPosition = cursorMatches.First().Index;

            expression = expression.Replace("|", string.Empty);

            RecordType contextType;
            if (contextTypeString != null)
            {
                DType.TryParse(contextTypeString, out var contextDType);
                contextType = FormulaType.Build(contextDType) as RecordType;

                Assert.IsNotNull(contextType, "Context type must be a record type");
            }
            else
            {
                // We leave the context type as an empty record when none is provided
                contextType = new RecordType();
            }

            return Suggest(expression, contextType, cursorPosition);
        }

        internal IIntellisenseResult Suggest(string expression, FormulaType parameterType, int cursorPosition)
        {
            var formula = new Formula(expression);
            formula.EnsureParsed(TexlParser.Flags.None);

            var binding = TexlBinding.Run(
                new Glue2DocumentBinderGlue(),
                formula.ParseTree,
                new SimpleResolver(EnumStore.EnumSymbols),
                ruleScope: parameterType._type,
                useThisRecordForRuleScope: false
            );

            var context = new IntellisenseContext(expression, cursorPosition);
            var intellisense = IntellisenseProvider.GetIntellisense();
            var suggestions = intellisense.Suggest(context, binding, formula);

            return suggestions;
        }
    }
}