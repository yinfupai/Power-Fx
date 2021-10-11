//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Now()
    // Equivalent DAX/Excel function: Now
    internal sealed class NowFunction : BuiltinFunction
    {
        // Multiple invocations may produce different return values.
        public override bool IsStateless => false;
        public override bool IsGlobalReliant => true;
        public override bool IsSelfContained => true;
        public override bool SupportsParamCoercion => true;

        public NowFunction()
            : base("Now", TexlStrings.AboutNow, FunctionCategories.DateTime, DType.DateTime, 0, 0, 0)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            return EnumerableUtils.Yield<TexlStrings.StringGetter[]>();
        }
    }
}
