//------------------------------------------------------------------------------
// <copyright file company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.IR.Symbols;
using Microsoft.PowerFx.Core.Utils;

namespace Microsoft.PowerFx.Core.IR.Nodes
{
    internal sealed class ScopeAccessNode : IntermediateNode
    {
        /// <summary>
        /// Either a ScopeSymbol or a ScopeAccessSymbol
        /// A ScopeSymbol here represents access to the whole scope record,
        /// A ScopeAccessSymbol here represents access to a single field from the scope
        /// </summary>
        public IScopeSymbol Value;

        public ScopeAccessNode(IRContext irContext, IScopeSymbol symbol) : base(irContext)
        {
            Contracts.AssertValue(symbol);

            Value = symbol;
        }

        public override TResult Accept<TResult, TContext>(IRNodeVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }


        public override string ToString()
        {
            return $"ScopeAccess({Value})";
        }
    }
}
