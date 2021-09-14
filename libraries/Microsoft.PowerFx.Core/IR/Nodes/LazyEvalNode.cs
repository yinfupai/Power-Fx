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
    /// <summary>
    /// Wraps an child IR Node that is evaluated on-demand by the LazyEval node's parent
    /// </summary>
    internal sealed class LazyEvalNode : IntermediateNode
    {
        public readonly IntermediateNode Child;

        public LazyEvalNode(IRContext irContext, IntermediateNode wrapped) : base(irContext)
        {
            Contracts.AssertValue(wrapped);

            Child = wrapped;
        }


        public override TResult Accept<TResult, TContext>(IRNodeVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }


        public override string ToString()
        {
            return $"Lazy({Child})";
        }
    }
}
