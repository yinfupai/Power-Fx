//------------------------------------------------------------------------------
// <copyright file="CountA.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // CountA(source:*)
    internal sealed class CountAFunction : FunctionWithTableInput
    {
        public override bool IsSelfContained => true;

        public CountAFunction()
            : base("CountA", TexlStrings.AboutCountA, FunctionCategories.Table | FunctionCategories.MathAndStat, DType.Number, 0, 1, 1, DType.EmptyTable)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.CountArg1 };
        }

        public override bool CheckInvocation(TexlNode[] args, DType[] argTypes, IErrorContainer errors, out DType returnType)
        {
            Contracts.AssertValue(args);
            Contracts.AssertAllValues(args);
            Contracts.AssertValue(argTypes);
            Contracts.Assert(args.Length == argTypes.Length);
            Contracts.AssertValue(errors);
            Contracts.Assert(MinArity <= args.Length && args.Length <= MaxArity);

            bool fValid = base.CheckInvocation(args, argTypes, errors, out returnType);

            // The argument should be a table of one column.
            DType argType = argTypes[0];
            if (!argType.IsTable)
            {
                fValid = false;
                errors.EnsureError(DocumentErrorSeverity.Severe, args[0], TexlStrings.ErrNeedTable_Func, Name);
            }
            else if (argType.GetNames(DPath.Root).Count() != 1)
            {
                fValid = false;
                errors.EnsureError(DocumentErrorSeverity.Severe, args[0], TexlStrings.ErrNeedTableCol_Func, Name);
            }

            return fValid;
        }
    }
}
