//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.AppMagic.Authoring.Texl.SourceInformation
{
    internal class IdentifierSource : ITexlSource
    {
        public Identifier Identifier { get; }

        public IEnumerable<Token> Tokens => new[] { Identifier.Token };

        public IEnumerable<ITexlSource> Sources => new[] { this };

        public IdentifierSource(Identifier identifier)
        {
            Contracts.AssertValue(identifier);
            this.Identifier = identifier;
        }

        public ITexlSource Clone(Dictionary<TexlNode, TexlNode> newNodes, Span span)
        {
            Contracts.AssertValue(newNodes);
            Contracts.AssertAllValues(newNodes.Values);
            Contracts.AssertAllValues(newNodes.Keys);
            return new IdentifierSource(Identifier.Clone(span));
        }
    }
}
