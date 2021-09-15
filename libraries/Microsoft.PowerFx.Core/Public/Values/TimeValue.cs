//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.IR;
using System;
using System.Diagnostics.Contracts;

namespace Microsoft.PowerFx
{
    public class TimeValue : PrimitiveValue<TimeSpan>
    {
        internal TimeValue(IRContext irContext, TimeSpan ts) : base(irContext, ts)
        {
            Contract.Assert(IRContext.ResultType == FormulaType.Time);
        }

        public override void Visit(IValueVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
