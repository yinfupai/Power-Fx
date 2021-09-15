//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Replace(source:s, start:n, count:n, replacement:s)
    internal sealed class ReplaceFunction : BuiltinFunction
    {
        public override bool RequiresErrorContext => true;
        public override bool IsSelfContained => true;

        public ReplaceFunction()
            : base("Replace", TexlStrings.AboutReplace, FunctionCategories.Text, DType.String, 0, 4, 4, DType.String, DType.Number, DType.Number, DType.String)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.ReplaceFuncArg1, TexlStrings.StringFuncArg2, TexlStrings.StringFuncArg3, TexlStrings.ReplaceFuncArg4 };
        }
    }

    // Replace(source:s|*[s], start:n|*[n], count:n|*[n], replacement:s|*[s])
    internal sealed class ReplaceTFunction : BuiltinFunction
    {
        public override bool RequiresErrorContext => true;
        public override bool IsSelfContained => true;

        public ReplaceTFunction()
            : base("Replace", TexlStrings.AboutReplaceT, FunctionCategories.Table, DType.EmptyTable, 0, 4, 4)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.StringTFuncArg1, TexlStrings.StringFuncArg2, TexlStrings.StringFuncArg3, TexlStrings.ReplaceFuncArg4 };
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

            DType type0 = argTypes[0];
            DType type1 = argTypes[1];
            DType type2 = argTypes[2];
            DType type3 = argTypes[3];

            // Arg0 should be either a string or a column of strings.
            // Its type dictates the function return type.
            if (type0.IsTable)
            {
                // Ensure we have a one-column table of strings
                fValid &= CheckStringColumnType(type0, args[0], errors);
                // Borrow the return type from the 1st arg
                returnType = type0;
            }
            else
            {
                returnType = DType.CreateTable(new TypedName(DType.String, OneColumnTableResultName));
                if (!DType.String.Accepts(type0))
                {
                    fValid = false;
                    errors.EnsureError(DocumentErrorSeverity.Severe, args[0], TexlStrings.ErrStringExpected);
                }
            }

            // Arg1 should be either a number or a column of numbers.
            if (type1.IsTable)
                fValid &= CheckNumericColumnType(type1, args[1], errors);
            else if (!DType.Number.Accepts(type1))
            {
                fValid = false;
                errors.EnsureError(DocumentErrorSeverity.Severe, args[1], TexlStrings.ErrNumberExpected);
            }

            // Arg2 should be either a number or a column of numbers.
            if (type2.IsTable)
                fValid &= CheckNumericColumnType(type2, args[2], errors);
            else if (!DType.Number.Accepts(type2))
            {
                fValid = false;
                errors.EnsureError(DocumentErrorSeverity.Severe, args[2], TexlStrings.ErrNumberExpected);
            }

            // Arg3 should be either a string or a column of strings.
            if (type3.IsTable)
                fValid &= CheckStringColumnType(type3, args[3], errors);
            else if (!DType.String.Accepts(type3))
            {
                fValid = false;
                errors.EnsureError(DocumentErrorSeverity.Severe, args[3], TexlStrings.ErrStringExpected);
            }

            // At least one arg has to be a table.
            if (!type0.IsTable && !type1.IsTable && !type2.IsTable && !type3.IsTable)
            {
                fValid = false;
                errors.EnsureError(DocumentErrorSeverity.Severe, args[0], TexlStrings.ErrTypeError);
                errors.EnsureError(DocumentErrorSeverity.Severe, args[1], TexlStrings.ErrTypeError);
                errors.EnsureError(DocumentErrorSeverity.Severe, args[2], TexlStrings.ErrTypeError);
                errors.EnsureError(DocumentErrorSeverity.Severe, args[3], TexlStrings.ErrTypeError);
            }

            return fValid;
        }
    }
}
