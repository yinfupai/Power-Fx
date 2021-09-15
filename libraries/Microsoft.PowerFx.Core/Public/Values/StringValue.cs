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
    public class StringValue : PrimitiveValue<string>
    {
        internal StringValue(IRContext irContext, string value) : base(irContext, value)
        {
            Contract.Assert(IRContext.ResultType == FormulaType.String);
        }

        public override void Visit(IValueVisitor visitor)
        {
            visitor.Visit(this);
        }

        internal StringValue ToLower()
        {
            return new StringValue(IRContext.NotInSource(FormulaType.String), Value.ToLowerInvariant());
        }
    }
}
