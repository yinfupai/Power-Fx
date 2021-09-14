//------------------------------------------------------------------------------
// <copyright file company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic;
using Microsoft.AppMagic.Authoring.Texl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.PowerFx.Core.IR
{
    internal sealed class BinaryOpNode : IntermediateNode
    {
        public readonly BinaryOpKind Op;
        public readonly IntermediateNode Left;
        public readonly IntermediateNode Right;

        public BinaryOpNode(IRContext irContext, BinaryOpKind op, IntermediateNode left, IntermediateNode right) : base(irContext)
        {
            Contracts.AssertValue(left);
            Contracts.AssertValue(right);

            Op = op;
            Left = left;
            Right = right;
        }


        public override TResult Accept<TResult, TContext>(IRNodeVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override string ToString()
        {
            return $"BinaryOp({Op}, {Left}, {Right})"; 
        }
    }
}
