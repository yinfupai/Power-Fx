//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.IR;
using System.Diagnostics.Contracts;

namespace Microsoft.PowerFx
{
    public class NumberValue : PrimitiveValue<double>
    {
        internal NumberValue(IRContext irContext, double value) : base(irContext, value)
        {
            Contract.Assert(IRContext.ResultType == FormulaType.Number);
        }

        public override void Visit(IValueVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
