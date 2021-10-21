//------------------------------------------------------------------------------
// <copyright file company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.Utils;

namespace Microsoft.PowerFx.Core.IR.Nodes
{
    internal sealed class SingleColumnTableAccessNode : IntermediateNode
    {
        public readonly IntermediateNode From;
        public readonly DName Field;

        public SingleColumnTableAccessNode(IRContext irContext, IntermediateNode from, DName field) : base(irContext)
        {
            Contracts.AssertValid(field);
            Contracts.AssertValue(from);

            From = from;
            Field = field;
        }

        public override TResult Accept<TResult, TContext>(IRNodeVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override string ToString()
        {
            return $"SingleColumnAccess({From}, {Field})";
        }
    }
}
