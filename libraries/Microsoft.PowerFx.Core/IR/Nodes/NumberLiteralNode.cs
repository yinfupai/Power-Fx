//------------------------------------------------------------------------------
// <copyright file company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;
using Microsoft.AppMagic.Authoring.Texl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.PowerFx.Core.IR
{
    internal sealed class NumberLiteralNode : IntermediateNode
    {
        public readonly double LiteralValue;

        public NumberLiteralNode(IRContext irContext, double value) : base(irContext)
        {
            LiteralValue = value;
        }

        public override TResult Accept<TResult, TContext>(IRNodeVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override string ToString()
        {
            return $"Number({LiteralValue})";
        }
    }
}
