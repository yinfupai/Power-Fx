//------------------------------------------------------------------------------
// <copyright file company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic;
using Microsoft.AppMagic.Authoring.Texl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.PowerFx.Core.IR
{
    internal sealed class ChainingNode : IntermediateNode
    {
        public readonly IList<IntermediateNode> Nodes;

        public ChainingNode(IRContext irContext, IList<IntermediateNode> nodes) : base(irContext)
        {
            Contracts.AssertAllValues(nodes);

            Nodes = nodes;
        }

        public override TResult Accept<TResult, TContext>(IRNodeVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override string ToString()
        {
            return $"Chained({string.Join(",", Nodes)})";
        }
    }
}
