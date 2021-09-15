//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal partial class Intellisense
    {
        internal sealed class BoolLitNodeSuggestionHandler : NodeKindSuggestionHandler
        {
            public BoolLitNodeSuggestionHandler()
                : base(NodeKind.BoolLit)
            { }

            internal override bool TryAddSuggestionsForNodeKind(IntellisenseData intellisenseData)
            {
                Contracts.AssertValue(intellisenseData);

                TexlNode curNode = intellisenseData.CurNode;
                int cursorPos = intellisenseData.CursorPos;
                BoolLitNode boolNode = curNode.CastBoolLit();
                var tokenSpan = curNode.Token.Span;

                if (cursorPos < tokenSpan.Min)
                {
                    // Cursor is before the token start.
                    IntellisenseHelper.AddSuggestionsForValuePossibilities(intellisenseData, curNode);
                }
                else if (cursorPos <= tokenSpan.Lim)
                {
                    // Cursor is in the middle of the token.
                    int replacementLength = tokenSpan.Min == cursorPos ? 0 : tokenSpan.Lim - tokenSpan.Min;
                    intellisenseData.SetMatchArea(tokenSpan.Min, cursorPos, replacementLength);
                    intellisenseData.BoundTo = boolNode.Value ? TexlLexer.KeywordTrue : TexlLexer.KeywordFalse;
                    IntellisenseHelper.AddSuggestionsForValuePossibilities(intellisenseData, curNode);
                }
                else if (IntellisenseHelper.CanSuggestAfterValue(cursorPos, intellisenseData.Script))
                {
                    // Verify that cursor is after a space after the current node's token.
                    // Suggest binary keywords.
                    IntellisenseHelper.AddSuggestionsForAfterValue(intellisenseData, DType.Boolean);
                }

                return true;
            }
        }
    }
}