//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Today()
    // Equivalent DAX/Excel function: Today
    internal sealed class TodayFunction : BuiltinFunction
    {
        // Multiple invocations may result in different return values.
        public override bool IsStateless => false;
        public override bool IsGlobalReliant => true;
        public override bool IsSelfContained => true;

        public TodayFunction()
            : base("Today", TexlStrings.AboutToday, FunctionCategories.DateTime, DType.Date, 0, 0, 0)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            return EnumerableUtils.Yield<TexlStrings.StringGetter[]>();
        }
    }
}
