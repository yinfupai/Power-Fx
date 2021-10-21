//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.PowerFx.Core.Texl.Intellisense
{
    internal static class IntellisenseProvider
    {
        internal static readonly ISuggestionHandler[] suggestionHandlers = {
            new Intellisense.CommentNodeSuggestionHandler(),
            new Intellisense.NullNodeSuggestionHandler(),
            new Intellisense.FunctionRecordNameSuggestionHandler(),
            new Intellisense.ErrorNodeSuggestionHandler(),
            new Intellisense.BlankNodeSuggestionHandler(),
            new Intellisense.DottedNameNodeSuggestionHandler(),
            new Intellisense.FirstNameNodeSuggestionHandler(),
            new Intellisense.CallNodeSuggestionHandler(),
            new Intellisense.BinaryOpNodeSuggestionHandler(),
            new Intellisense.UnaryOpNodeSuggestionHandler(),
            new Intellisense.BoolLitNodeSuggestionHandler(),
            new Intellisense.StrNumLitNodeSuggestionHandler(),
            new Intellisense.RecordNodeSuggestionHandler(),
        };

        internal static IIntellisense GetIntellisense()
        {
            return new Intellisense(suggestionHandlers);
        }
    }
}
