//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // IsNumeric(expression:E)
    internal sealed class IsNumericFunction : BuiltinFunction
    {
        public override bool IsSelfContained => true;

        public IsNumericFunction()
            : base("IsNumeric", TexlStrings.AboutIsNumeric, FunctionCategories.Information, DType.Boolean, 0, 1, 1)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.IsNumericArg1 };
        }
    }
}
