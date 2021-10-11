//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // TimeZoneOffset()
    internal sealed class TimeZoneOffsetFunction : BuiltinFunction
    {
        public override bool RequiresErrorContext => true;
        public override bool IsSelfContained => true;
        public override bool SupportsParamCoercion => true;

        public TimeZoneOffsetFunction()
            : base("TimeZoneOffset", TexlStrings.AboutTimeZoneOffset, FunctionCategories.DateTime, DType.Number, 0, 0, 1, DType.DateTime)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new TexlStrings.StringGetter[] { };
            yield return new[] { TexlStrings.TimeZoneOffsetArg1 };
        }
    }
}
