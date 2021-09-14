//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.AppMagic.Authoring.Texl.SourceInformation
{
    /// <summary>
    /// A recursive reference to another node as a piece of the source for 
    /// its parent node.
    /// </summary>
    internal class NodeSource : ITexlSource
    {
        public TexlNode Node { get; }

        public IEnumerable<Token> Tokens => Node.SourceList.Tokens;

        public IEnumerable<ITexlSource> Sources => new[] { this };

        public NodeSource(TexlNode node)
        {
            Contracts.AssertValue(node);
            this.Node = node;
        }

        public ITexlSource Clone(Dictionary<TexlNode, TexlNode> newNodes, Span span)
        {
            Contracts.AssertAllValues(newNodes.Keys);
            Contracts.AssertAllValues(newNodes.Values);
            Contracts.AssertValue(newNodes);

            return new NodeSource(newNodes[Node]);
        }

        public override string ToString()
        {
            return Node.ToString();
        }
    }
}
