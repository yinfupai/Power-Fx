//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.IR;
using System.Diagnostics.Contracts;
using System.Text.Json;

namespace Microsoft.PowerFx
{
    public class BooleanValue : PrimitiveValue<bool>
    {
        internal BooleanValue(IRContext irContext, bool value) : base(irContext, value)
        {
            Contract.Assert(IRContext.ResultType == FormulaType.Boolean);
        }

        public override void Visit(IValueVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
