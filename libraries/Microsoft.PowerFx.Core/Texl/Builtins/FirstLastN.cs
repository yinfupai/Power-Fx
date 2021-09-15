//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // FirstN(source:*, [count:n])
    // LastN(source:*, [count:n])
    internal sealed class FirstLastNFunction : FunctionWithTableInput
    {
        public override bool IsSelfContained => true;

        public FirstLastNFunction(bool isFirst)
            : base(isFirst ? "FirstN" : "LastN", isFirst ? TexlStrings.AboutFirstN : TexlStrings.AboutLastN, FunctionCategories.Table,
            DType.EmptyTable, 0, 1, 2, DType.EmptyTable, DType.Number)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.FirstLastNArg1 };
            yield return new [] { TexlStrings.FirstLastNArg1, TexlStrings.FirstLastNArg2 };
        }

        public override bool CheckInvocation(TexlNode[] args, DType[] argTypes, IErrorContainer errors, out DType returnType)
        {
            Contracts.AssertValue(args);
            Contracts.AssertValue(argTypes);
            Contracts.Assert(args.Length == argTypes.Length);
            Contracts.AssertValue(errors);
            Contracts.Assert(MinArity <= args.Length && args.Length <= MaxArity);

            bool fArgsValid = base.CheckInvocation(args, argTypes, errors, out returnType);

            DType arg0Type = argTypes[0];
            if (arg0Type.IsTable)
                returnType = arg0Type;
            else
            {
                returnType = arg0Type.IsRecord ? arg0Type.ToTable() : DType.Error;
                fArgsValid = false;
            }

            return fArgsValid;
        }
    }
}
