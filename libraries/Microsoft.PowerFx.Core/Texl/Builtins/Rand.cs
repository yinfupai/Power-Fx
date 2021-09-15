//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Rand()
    // Equivalent DAX/Excel function: Rand
    internal sealed class RandFunction : BuiltinFunction
    {
        // Multiple invocations may produce different return values.
        public override bool IsStateless => false;
        public override bool IsSelfContained => true;

        public RandFunction()
            : base("Rand", TexlStrings.AboutRand, FunctionCategories.MathAndStat, DType.Number, 0, 0, 0)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            return EnumerableUtils.Yield<TexlStrings.StringGetter[]>();
        }
    }
}
