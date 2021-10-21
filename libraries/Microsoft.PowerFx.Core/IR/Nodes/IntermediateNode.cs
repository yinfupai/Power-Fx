//------------------------------------------------------------------------------
// <copyright file company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.PowerFx.Core.IR.Nodes
{
    internal abstract class IntermediateNode
    {
        public IRContext IRContext { get; }

        public IntermediateNode(IRContext irContext)
        {
            IRContext = irContext;
        }

        public abstract TResult Accept<TResult, TContext>(IRNodeVisitor<TResult, TContext> visitor, TContext context);
    }
}
