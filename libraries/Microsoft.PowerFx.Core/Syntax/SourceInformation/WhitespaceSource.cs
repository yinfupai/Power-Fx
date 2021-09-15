//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AppMagic.Authoring.Texl.SourceInformation
{
    /// <summary>
    /// A series of whitespace tokens that are part of the source for the
    /// holding TexlNode.
    /// </summary>
    internal sealed class WhitespaceSource : ITexlSource
    {
        public IEnumerable<Token> Tokens { get; }

        public IEnumerable<ITexlSource> Sources => new[] { this };

        public WhitespaceSource(IEnumerable<Token> tokens)
        {
            Contracts.AssertValue(tokens);
            Contracts.AssertAllValues(tokens);
            Tokens = tokens;
        }

        public ITexlSource Clone(Dictionary<TexlNode, TexlNode> newNodes, Span newSpan)
        {
            Contracts.AssertValue(newNodes);
            Contracts.AssertAllValues(newNodes.Values);
            Contracts.AssertAllValues(newNodes.Keys);
            return new WhitespaceSource(Tokens.Select(token => token.Clone(newSpan)));
        }

        public override string ToString()
        {
            return " ";
        }
    }
}
