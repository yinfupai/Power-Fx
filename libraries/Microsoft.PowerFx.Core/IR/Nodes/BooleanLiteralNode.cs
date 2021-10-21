//------------------------------------------------------------------------------
// <copyright file company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.PowerFx.Core.IR.Nodes
{
    internal sealed class BooleanLiteralNode : IntermediateNode
    {
        public readonly bool LiteralValue;
        public BooleanLiteralNode(IRContext irContext, bool value) : base(irContext)
        {
            LiteralValue = value;
        }

        public override TResult Accept<TResult, TContext>(IRNodeVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override string ToString()
        {
            return $"Bool({LiteralValue})";
        }
    }
}
