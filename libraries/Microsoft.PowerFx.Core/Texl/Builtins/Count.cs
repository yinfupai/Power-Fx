//------------------------------------------------------------------------------
// <copyright file="Count.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Count(source:*)
    internal sealed class CountFunction : FunctionWithTableInput
    {
        public override bool IsSelfContained => true;

        public CountFunction()
            : base("Count", TexlStrings.AboutCount, FunctionCategories.Table | FunctionCategories.MathAndStat, DType.Number, 0, 1, 1, DType.EmptyTable)
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
            IEnumerable<TypedName> columns;
            if (!argType.IsTable)
            {
                fValid = false;
                errors.EnsureError(DocumentErrorSeverity.Severe, args[0], TexlStrings.ErrNeedTable_Func, Name);
            }
            else if ((columns = argType.GetNames(DPath.Root)).Count() != 1)
            {
                fValid = false;
                errors.EnsureError(DocumentErrorSeverity.Severe, args[0], TexlStrings.ErrNeedTableCol_Func, Name);
            }
            else if (!DType.Number.Accepts(columns.Single().Type))
            {
                fValid = false;
                errors.EnsureError(DocumentErrorSeverity.Warning, args[0], TexlStrings.ErrInvalidSchemaNeedNumCol_Col, columns.Single().Name);
            }

            return fValid;
        }
    }
}
