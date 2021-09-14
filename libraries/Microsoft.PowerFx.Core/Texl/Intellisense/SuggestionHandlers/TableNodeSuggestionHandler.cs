//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal partial class Intellisense
    {
        internal sealed class TableNodeSuggestionHandler : NodeKindSuggestionHandler
        {
            public TableNodeSuggestionHandler()
                : base(NodeKind.Table)
            { }

            internal override bool TryAddSuggestionsForNodeKind(IntellisenseData intellisenseData)
            {
                Contracts.AssertValue(intellisenseData);

                TexlNode curNode = intellisenseData.CurNode;
                int cursorPos = intellisenseData.CursorPos;

                var tokenSpan = curNode.Token.Span;

                // Only suggest after table nodes
                if (cursorPos <= tokenSpan.Lim)
                    return true;

                if (IntellisenseHelper.CanSuggestAfterValue(cursorPos, intellisenseData.Script))
                {
                    // Verify that cursor is after a space after the current node's token.
                    // Suggest binary keywords.
                    IntellisenseHelper.AddSuggestionsForAfterValue(intellisenseData, DType.EmptyTable);
                }

                return true;
            }
        }
    }
}