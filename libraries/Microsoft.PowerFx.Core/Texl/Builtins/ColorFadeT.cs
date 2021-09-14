//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // ColorFade(color:c|*[c], fadeDelta:n|*[n])
    internal sealed class ColorFadeTFunction : BuiltinFunction
    {
        private static readonly string TableKindString = DType.EmptyTable.GetKindString();
        public override bool IsSelfContained => true;

        public ColorFadeTFunction()
            : base("ColorFade", TexlStrings.AboutColorFadeT, FunctionCategories.Table, DType.EmptyTable, 0, 2, 2)
        { }

        public override bool IsTrackedInTelemetry => false;

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.ColorFadeTArg1, TexlStrings.ColorFadeTArg2 };
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

            DType otherType = DType.Invalid;
            TexlNode otherArg = null;

            // At least one of the arguments has to be a table.
            if (type0.IsTable)
            {
                // Ensure we have a one-column table of colors.
                fValid &= CheckColorColumnType(type0, args[0], errors);
                // Borrow the return type from the 1st arg.
                returnType = type0;
                // Check arg1 below.
                otherArg = args[1];
                otherType = type1;

                fValid &= CheckOtherType(otherType, otherArg, DType.Number, errors);

                Contracts.Assert(returnType.IsTable);
                Contracts.Assert(!fValid || returnType.IsColumn);
            }
            else if (type1.IsTable)
            {
                // Ensure we have a one-column table of numerics.
                fValid &= CheckNumericColumnType(type1, args[1], errors);
                // Since the 1st arg is not a table, make a new table return type *[Result:c]
                returnType = DType.CreateTable(new TypedName(DType.Color, OneColumnTableResultName));
                // Check arg0 below.
                otherArg = args[0];
                otherType = type0;

                fValid &= CheckOtherType(otherType, otherArg, DType.Color, errors);

                Contracts.Assert(returnType.IsTable);
                Contracts.Assert(!fValid || returnType.IsColumn);
            }
            else
            {
                Contracts.Assert(returnType.IsTable);

                errors.EnsureError(DocumentErrorSeverity.Severe, args[0], TexlStrings.ErrTypeError_Ex1_Ex2_Found, TableKindString, DType.Color.GetKindString(), type0.GetKindString());
                errors.EnsureError(DocumentErrorSeverity.Severe, args[1], TexlStrings.ErrTypeError_Ex1_Ex2_Found, TableKindString, DType.Number.GetKindString(), type1.GetKindString());
                // Both args are invalid. No need to continue.
                return false;
            }

            return fValid;
        }

        private bool CheckOtherType(DType otherType, TexlNode otherArg, DType expectedType, IErrorContainer errors)
        {
            Contracts.Assert(otherType.IsValid);
            Contracts.AssertValue(otherArg);
            Contracts.Assert(expectedType == DType.Color || expectedType == DType.Number);
            Contracts.AssertValue(errors);

            if (otherType.IsTable)
            {
                // Ensure we have a one-column table of numerics/color values based on expected type.
                return expectedType == DType.Number ? CheckNumericColumnType(otherType, otherArg, errors) : CheckColorColumnType(otherType, otherArg, errors);
            }

            if (expectedType.Accepts(otherType))
                return true;

            errors.EnsureError(DocumentErrorSeverity.Severe, otherArg, TexlStrings.ErrTypeError_Ex1_Ex2_Found, TableKindString, expectedType.GetKindString(), otherType.GetKindString());
            return false;
        }
    }
}