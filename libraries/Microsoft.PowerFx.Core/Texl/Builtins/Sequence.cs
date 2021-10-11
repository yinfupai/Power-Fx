//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Sequence(records:n, start:n, step:n): *[Value:n]
    internal sealed class SequenceFunction : BuiltinFunction
    {
        public override bool RequiresErrorContext => true;
        public override bool IsSelfContained => true;
        public override bool SupportsParamCoercion => true;

        public SequenceFunction()
            : base("Sequence", TexlStrings.AboutSequence, FunctionCategories.MathAndStat, DType.CreateTable(new TypedName(DType.Number, new DName("Value"))), 0, 1, 3, DType.Number, DType.Number, DType.Number)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new[] { TexlStrings.SequenceArg1 };
            yield return new[] { TexlStrings.SequenceArg1, TexlStrings.SequenceArg2 };
            yield return new[] { TexlStrings.SequenceArg1, TexlStrings.SequenceArg2, TexlStrings.SequenceArg3 };
        }
    }
}
