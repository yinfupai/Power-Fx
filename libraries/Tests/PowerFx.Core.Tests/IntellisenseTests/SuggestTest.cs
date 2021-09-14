//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.PowerFx.Tests.IntellisenseTests
{
    [TestClass]
    public class SuggestTests : IntellisenseTestBase
    {
        /// <summary>
        /// This method does the same as <see cref="Suggest"/>, but filters the suggestions by their text so
        /// that they can be more easily compared
        /// </summary>
        /// <param name="expression">
        /// Test case wherein the presence of the `|` character indicates cursor position.  See
        /// <see cref="TestSuggest"/> for more details.
        /// </param>
        /// <param name="contextTypeString">
        /// The type that defines names and types that are valid in <see cref="expression"/>
        /// </param>
        /// <returns>
        /// List of string representing suggestions
        /// </returns>
        private string[] SuggestStrings(string expression, string contextTypeString = null)
        {
            Assert.IsNotNull(expression);

            var intellisense = Suggest(expression, contextTypeString);
            return intellisense.Suggestions.Select(suggestion => suggestion.DisplayText.Text).ToArray();
        }

        /// <summary>
        /// Compares expected suggestions with suggestions made by PFx Intellisense for a given
        /// <see cref="expression"/> and cursor position. The cursor position is determined by the index of
        /// the | character in <see cref="expression"/>, which will be removed but the test is run. Note that
        /// the use of the `|` char is for this reason disallowed from test cases except to indicate cursor
        /// position. Note also that these test suggestion order as well as contents.
        /// </summary>
        /// <param name="expression">
        /// Expression on which intellisense will be run
        /// </param>
        /// <param name="expectedSuggestions">
        /// A list of arguments that will be compared with the names of the output of
        /// <see cref="Workspace.Suggest"/> in order
        /// </param>
        [DataTestMethod, Owner("jokellih")]
        // CommentNodeSuggestionHandler
        [DataRow("// No| intellisense inside comment")]
        // RecordNodeSuggestionHandler
        [DataRow("{} |", "As", "exactin", "in")]
        [DataRow("{'complex record':{column:{}}} |", "As", "exactin", "in")]
        // DottedNameNodeSuggestionHandler
        [DataRow("{a:{},b:{},c:{}}.|", "a", "b", "c")]
        [DataRow("{abc:{},ab:{},a:{}}.|ab", "ab", "a", "abc")]
        [DataRow("{abc:{},ab:{},a:{}}.ab|", "ab", "abc")]
        [DataRow("{abc:{},ab:{},a:{}}.ab|c", "abc", "ab")]
        [DataRow("{abc:{},ab:{},a:{}}.abc|", "abc")]
        [DataRow("{abc:{},ab:{},a:{}}.| abc", "a", "ab", "abc")]
        [DataRow("{abc:{},ab:{},a:{}}.abc |", "As", "exactin", "in")]
        [DataRow("{az:{},z:{}}.|", "az", "z")]
        [DataRow("{az:{},z:{}}.z|", "z", "az")]
        // We don't recommend anything for one column tables only if the one column table is referenced
        // by the following dotted name access.
        [DataRow("[\"test\"].Value.| ")]
        [DataRow("[{test:\",test\"}].test.| ")]
        // We do, however, if the one column table is a literal.
        [DataRow("[\"test\"].| ", "Value")]
        [DataRow("Calendar.|", "MonthsLong", "MonthsShort", "WeekdaysLong", "WeekdaysShort")]
        [DataRow("Calendar.Months|", "MonthsLong", "MonthsShort")]
        [DataRow("Color.AliceBl|", "AliceBlue")]
        [DataRow("Color.Pale|", "PaleGoldenRod", "PaleGreen", "PaleTurquoise", "PaleVioletRed")]
        // CallNodeSuggestionHandler
        [DataRow("ForAll|([1],Value)", "ForAll")]
        [DataRow("at|(", "Atan", "Atan2", "Concat", "Concatenate", "Date", "DateAdd", "DateDiff", "DateTimeValue", "DateValue")]
        [DataRow("Atan |(")]
        [DataRow("Clock.A|(", "Clock.AmPm", "Clock.AmPmShort")]
        [DataRow("ForAll([\"test\"],EndsWith(|))", "Value")]
        [DataRow("ForAll([1],Value) |", "As", "exactin", "in")]
        // BoolLitNodeSuggestionHandler
        [DataRow("true|", "true")]
        [DataRow("tru|e", "true", "Trunc")]
        [DataRow("false |", "-", "&", "&&", "*", "/", "^", "||", "+", "<", "<=", "<>", "=", ">", ">=", "And", "As", "exactin", "in", "Or")]
        // BinaryOpNodeSuggestionHandler
        [DataRow("1 +|", "+")]
        [DataRow("1 |+", "-", "&", "&&", "*", "/", "^", "||", "+", "<", "<=", "<>", "=", ">", ">=", "And", "As", "exactin", "in", "Or")]
        [DataRow("\"1\" in|", "in", "exactin")]
        [DataRow("true &|", "&", "&&")]
        // UnaryOpNodeSuggestionHandler
        [DataRow("Not| false", "Not", "Note", "Notebook", "NotFound", "NotificationType", "NotificationType.Error", "NotificationType.Information", "NotificationType.Success", "NotificationType.Warning", "NotSupported", "FileNotFound")]
        [DataRow("| Not false")]
        [DataRow("Not |")]
        // StrNumLitNodeSuggestionHandler
        [DataRow("1 |", "-", "&", "&&", "*", "/", "^", "||", "+", "<", "<=", "<>", "=", ">", ">=", "And", "As", "exactin", "in", "Or")]
        [DataRow("1|0")]
        [DataRow("\"Clock|\"")]
        // FirstNameNodeSuggestionHandler
        [DataRow("Tru|", "true", "Trunc")] // Though it recommends only a boolean, the suggestions are still provided by the first name handler
        [DataRow("[@Bo|]", "BorderStyle", "VirtualKeyboardMode")]
        // FunctionRecordNameSuggestionHandler
        [DataRow("Error({Kin|d:0})", "Kind:")]
        [DataRow("Error({|Kind:0, Test:\"\"})", "Kind:", "Test:")]
        // ErrorNodeSuggestionHandler
        [DataRow("ForAll([0],`|", "ThisRecord", "Value")]
        [DataRow("ForAll(-],|", "ThisRecord")]
        [DataRow("ForAll()~|")]
        // BlankNodeSuggestionHandler
        [DataRow("|")]
        // AddSuggestionsForEnums
        [DataRow("Edit|", "EditPermissions", "DataSourceInfo.EditPermission", "DisplayMode.Edit", "FormMode.Edit", "Icon.Edit", "RecordInfo.EditPermission", "SelectedState.Edit")]
        [DataRow("DisplayMode.E|", "Edit", "Disabled", "View")]
        [DataRow("Disabled|", "Disabled")]
        [DataRow("DisplayMode.D|", "Disabled", "Edit")]
        [DataRow("DisplayMode|", "DisplayMode", "DisplayMode.Disabled", "DisplayMode.Edit", "DisplayMode.View")]
        public void TestSuggest(string expression, params string[] expectedSuggestions)
        {
            var actualSuggestions = SuggestStrings(expression);
            CollectionAssert.AreEqual(expectedSuggestions, actualSuggestions);
        }

        /// <summary>
        /// In cases for which Intellisense produces exceedingly numerous results it may be sufficient that
        /// they (the cases) be validated based on whether they return suggestions at all
        /// </summary>
        [DataTestMethod, Owner("jokellih")]
        // CallNodeSuggestionHandler
        [DataRow("| ForAll([1],Value)")]
        // BoolLitNodeSuggestionHandler
        [DataRow("t|rue")]
        [DataRow("f|alse")]
        [DataRow("| false")]
        // UnaryOpNodeSuggestionHandler
        [DataRow("|Not false")]
        // FirstNameNodeSuggestionHandler
        [DataRow("| Test", "![Test: s]")]
        [DataRow("[@|]")]
        [DataRow("[@|")]
        public void TestNonEmptySuggest(string expression, string context = null)
        {
            var actualSuggestions = SuggestStrings(expression, context);
            Assert.IsTrue(actualSuggestions.Length > 0);
        }

        [DataTestMethod, Owner("jokellih")]
        // FirstNameNodeSuggestionHandleractualSuggestions = IntellisenseResult
        [DataRow("Test|", "![Test1: s, Test2: n, Test3: h]", "Test1", "Test2", "Test3")]
        [DataRow("RecordName[|", "![RecordName: ![StringName: s, NumberName: n]]", "@NumberName", "@StringName")]
        [DataRow("RecordName[|", "![RecordName: ![]]")]
        [DataRow("Test |", "![Test: s]", "-", "&", "&&", "*", "/", "^", "||", "+", "<", "<=", "<>", "=", ">", ">=", "And", "As", "exactin", "in", "Or")]
        // ErrorNodeSuggestionHandler
        [DataRow("ForAll(Table,`|", "![Table: *[Column: s]]", "Column", "ThisRecord")]
        public void TestSuggestWithContext(string expression, string context, params string[] expectedSuggestions)
        {
            Assert.IsNotNull(context);

            var actualSuggestions = SuggestStrings(expression, context);
            CollectionAssert.AreEqual(expectedSuggestions, actualSuggestions);
        }
    }
}