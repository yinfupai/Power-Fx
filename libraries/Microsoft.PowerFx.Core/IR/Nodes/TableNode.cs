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
    internal sealed class TableNode : IntermediateNode
    {
        public readonly IReadOnlyList<IntermediateNode> Values;

        public TableNode(IRContext irContext, params IntermediateNode[] values) : base(irContext)
        {
            Contracts.AssertAllValues(values);

            Values = values;
        }

        public override TResult Accept<TResult, TContext>(IRNodeVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }
        
        public override string ToString()
        {
            return $"Table({string.Join(",", Values)})";
        }
    }
}
