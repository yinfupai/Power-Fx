//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Pi()
    // Equivalent Excel function: PI
    internal sealed class PiFunction : BuiltinFunction
    {
        public override bool IsSelfContained => true;
        public override bool SupportsParamCoercion => true;

        public PiFunction()
            : base("Pi",
                  TexlStrings.AboutPi,
                  FunctionCategories.MathAndStat,
                  DType.Number, // return type
                  0,            // no lambdas
                  0,            // min arity of 0
                  0)            // max arity of 0
        { }
        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new TexlStrings.StringGetter[] { };
        }
    }
}
