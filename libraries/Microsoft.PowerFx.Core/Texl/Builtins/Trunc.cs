//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Trunc(number:n, digits:n)
    // Truncate by rounding toward zero.
    internal sealed class TruncFunction : BuiltinFunction
    {
        public override bool IsSelfContained => true;

        public TruncFunction()
            : base("Trunc", TexlStrings.AboutTrunc, FunctionCategories.MathAndStat, DType.Number, 0, 1, 2, DType.Number, DType.Number)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new[] { TexlStrings.TruncArg1 };
            yield return new[] { TexlStrings.TruncArg1, TexlStrings.TruncArg2 };
        }
    }
    internal sealed class TruncTableFunction : BuiltinFunction
    {
        public override bool IsSelfContained => true;

        public TruncTableFunction()
            : base("Trunc", TexlStrings.AboutTruncT, FunctionCategories.Table, DType.EmptyTable, 0, 1, 2)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new[] { TexlStrings.TruncTArg1 };
            yield return new[] { TexlStrings.TruncTArg1, TexlStrings.TruncTArg2 };
        }

        public override string GetUniqueTexlRuntimeName(bool isPrefetching = false)
        {
            return GetUniqueTexlRuntimeName(suffix: "_T");
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

            if (argTypes.Length == 2)
            {
                DType type0 = argTypes[0];
                DType type1 = argTypes[1];

                DType otherType = DType.Invalid;
                TexlNode otherArg = null;

                // At least one of the arguments has to be a table.
                if (type0.IsTable)
                {
                    // Ensure we have a one-column table of numerics
                    fValid &= CheckNumericColumnType(type0, args[0], errors);
                    // Borrow the return type from the 1st arg
                    returnType = type0;
                    // Check arg1 below.
                    otherArg = args[1];
                    otherType = type1;
                }
                else if (type1.IsTable)
                {
                    // Ensure we have a one-column table of numerics
                    fValid &= CheckNumericColumnType(type1, args[1], errors);
                    // Since the 1st arg is not a table, make a new table return type *[Result:n]
                    returnType = DType.CreateTable(new TypedName(DType.Number, OneColumnTableResultName));
                    // Check arg0 below.
                    otherArg = args[0];
                    otherType = type0;
                }
                else
                {
                    Contracts.Assert(returnType.IsTable);
                    errors.EnsureError(DocumentErrorSeverity.Severe, args[0], TexlStrings.ErrTypeError);
                    errors.EnsureError(DocumentErrorSeverity.Severe, args[1], TexlStrings.ErrTypeError);
                    // Both args are invalid. No need to continue.
                    return false;
                }

                Contracts.Assert(otherType.IsValid);
                Contracts.AssertValue(otherArg);
                Contracts.Assert(returnType.IsTable);
                Contracts.Assert(!fValid || returnType.IsColumn);

                if (otherType.IsTable)
                {
                    // Ensure we have a one-column table of numerics
                    fValid &= CheckNumericColumnType(otherType, otherArg, errors);
                }
                else if (!DType.Number.Accepts(otherType))
                {
                    fValid = false;
                    errors.EnsureError(DocumentErrorSeverity.Severe, otherArg, TexlStrings.ErrTypeError);
                }
            }
            else
            {
                DType type0 = argTypes[0];

                if (type0.IsTable)
                {
                    // Ensure we have a one-column table of numerics
                    fValid &= CheckNumericColumnType(type0, args[0], errors);
                    // Borrow the return type from the 1st arg
                    returnType = type0;
                }
                else
                {
                    Contracts.Assert(returnType.IsTable);
                    errors.EnsureError(DocumentErrorSeverity.Severe, args[0], TexlStrings.ErrTypeError);
                    return false;
                }
            }

            return fValid;
        }
    }
}
