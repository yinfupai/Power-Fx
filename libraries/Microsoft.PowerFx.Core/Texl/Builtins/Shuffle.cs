//------------------------------------------------------------------------------
// <copyright file="Shuffle.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.PowerFx.Core.App.ErrorContainers;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Shuffle(source:*)
    internal sealed class ShuffleFunction : BuiltinFunction
    {
        // Multiple invocations with the same args may result in different results.
        public override bool IsStateless => false;
        public override bool IsSelfContained => true;
        public override bool SupportsParamCoercion => false;

        public ShuffleFunction()
            : base("Shuffle", TexlStrings.AboutShuffle, FunctionCategories.Table, DType.EmptyTable, 0, 1, 1, DType.EmptyTable)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.ShuffleArg1 };
        }

        public override bool CheckInvocation(TexlNode[] args, DType[] argTypes, IErrorContainer errors, out DType returnType, out Dictionary<TexlNode, DType> nodeToCoercedTypeMap)
        {
            Contracts.AssertValue(args);
            Contracts.AssertAllValues(args);
            Contracts.AssertValue(argTypes);
            Contracts.Assert(args.Length == argTypes.Length);
            Contracts.AssertValue(errors);

            bool isValid = base.CheckInvocation(args, argTypes, errors, out returnType, out nodeToCoercedTypeMap);

            bool fError = false;
            returnType = argTypes[0].ToTable(ref fError);
            
            return isValid && !fError;
        }
    }
}
