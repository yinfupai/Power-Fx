//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal partial class Intellisense
    {
        internal sealed class BinaryOpNodeSuggestionHandler : NodeKindSuggestionHandler
        {
            public BinaryOpNodeSuggestionHandler()
                : base(NodeKind.BinaryOp)
            { }

            internal override bool TryAddSuggestionsForNodeKind(IntellisenseData intellisenseData)
            {
                Contracts.AssertValue(intellisenseData);

                TexlNode curNode = intellisenseData.CurNode;
                // Cursor is in the operation token.
                // Suggest binary operators.
                BinaryOpNode binaryOpNode = curNode.CastBinaryOp();
                var tokenSpan = binaryOpNode.Token.Span;

                string keyword = binaryOpNode.Op == BinaryOp.Error ? tokenSpan.GetFragment(intellisenseData.Script) : TexlParser.GetTokString(binaryOpNode.Token.Kind);
                int replacementLength = tokenSpan.Min == intellisenseData.CursorPos ? 0 : tokenSpan.Lim - tokenSpan.Min;
                intellisenseData.SetMatchArea(tokenSpan.Min, intellisenseData.CursorPos, replacementLength);
                intellisenseData.BoundTo = binaryOpNode.Op == BinaryOp.Error ? string.Empty : keyword;
                AddSuggestionsForBinaryOperatorKeyWords(intellisenseData);

                return true;
            }

            internal static void AddSuggestionsForBinaryOperatorKeyWords(IntellisenseData intellisenseData)
            {
                Contracts.AssertValue(intellisenseData);

                // TASK: 76039: Intellisense: Update intellisense to filter suggestions based on the expected type of the text being typed in UI
                IntellisenseHelper.AddSuggestionsForMatches(intellisenseData, TexlLexer.LocalizedInstance.GetBinaryOperatorKeywords(), SuggestionKind.BinaryOperator, SuggestionIconKind.Other, requiresSuggestionEscaping: false);
            }
        }
    }
}