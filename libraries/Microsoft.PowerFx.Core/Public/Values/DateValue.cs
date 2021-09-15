//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.IR;
using System;
using System.Diagnostics.Contracts;
using System.Text.Json;

namespace Microsoft.PowerFx
{
    public class DateValue : PrimitiveValue<DateTimeOffset>
    {
        internal DateValue(IRContext irContext, DateTimeOffset value) : base(irContext, value)
        {
            Contract.Assert(IRContext.ResultType == FormulaType.Date || IRContext.ResultType == FormulaType.DateTime || IRContext.ResultType == FormulaType.DateTimeNoTimeZone);
        }

        public override void Visit(IValueVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
