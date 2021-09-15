//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // RandBetween()
    // Equivalent DAX/Excel function: RandBetween
    internal sealed class RandBetweenFunction : BuiltinFunction
    {
        // Multiple invocations may produce different return values.
        public override bool IsStateless => false;
        public override bool IsSelfContained => true;
        public override bool RequiresErrorContext => true;

        public RandBetweenFunction()
            : base("RandBetween",
                  TexlStrings.AboutRandBetween,
                  FunctionCategories.MathAndStat,
                  returnType: DType.Number,
                  maskLambdas: 0,
                  arityMin: 2,
                  arityMax: 2,
                  DType.Number,
                  DType.Number
                  )
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new[] { TexlStrings.RandBetweenArg1, TexlStrings.RandBetweenArg2 };
        }
    }
}