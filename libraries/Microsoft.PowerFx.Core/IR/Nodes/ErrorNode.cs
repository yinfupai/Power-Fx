//------------------------------------------------------------------------------
// <copyright file company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring.Texl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.PowerFx.Core.IR
{
    /*
    * Error nodes show up in the IR Tree where we could not bind the parse tree correctly.
    * This leaves the decision on how to handle this error up to implementers.
    */
    internal sealed class ErrorNode : IntermediateNode
    {
        // ErrorHint contains the stringified part of the Parse Tree
        // that resulted in this error node.
        // This mostly exists for debug purposes.
        public readonly string ErrorHint;

        public ErrorNode(IRContext irContext, string hint) : base(irContext)
        {
            ErrorHint = hint;
        }

        public override TResult Accept<TResult, TContext>(IRNodeVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }
        public override string ToString()
        {
            return $"Error({ErrorHint})";
        }
    }
}
