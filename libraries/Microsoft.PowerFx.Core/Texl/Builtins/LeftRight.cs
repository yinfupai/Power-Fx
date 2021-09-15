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
    // Scalar overloads of Left and Right.
    //  Left(arg:s, count:n)
    //  Right(arg:s, count:n)
    internal sealed class LeftRightScalarFunction : BuiltinFunction
    {
        public override bool RequiresErrorContext => true;
        public override bool IsSelfContained => true;

        public LeftRightScalarFunction(bool isLeft)
            : base(isLeft ? "Left" : "Right", isLeft ? TexlStrings.AboutLeft : TexlStrings.AboutRight, FunctionCategories.Text, DType.String, 0, 2, 2, DType.String, DType.Number)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.LeftRightArg1, TexlStrings.LeftRightArg2 };
        }
    }

    // Table overloads of Left and Right:
    //  Left(arg:*[_:s], count:n)
    //  Right(arg:*[_:s], count:n)
    internal sealed class LeftRightTableScalarFunction : BuiltinFunction
    {
        public override bool RequiresErrorContext => true;
        public override bool IsSelfContained => true;

        public LeftRightTableScalarFunction(bool isLeft)
            : base(isLeft ? "Left" : "Right", isLeft ? TexlStrings.AboutLeftT : TexlStrings.AboutRightT, FunctionCategories.Table, DType.EmptyTable, 0, 2, 2, DType.EmptyTable, DType.Number)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.LeftRightTArg1, TexlStrings.LeftRightArg2 };
        }

        public override string GetUniqueTexlRuntimeName(bool isPrefetching = false)
        {
            // Disambiguate these from the scalar overloads, so we don't have to
            // do type checking in the JS (runtime) implementation of Left/Right.
            return GetUniqueTexlRuntimeName(suffix: "_TS");
        }

        public override bool CheckInvocation(TexlNode[] args, DType[] argTypes, IErrorContainer errors, out DType returnType)
        {
            Contracts.AssertValue(args);
            Contracts.AssertAllValues(args);
            Contracts.AssertValue(argTypes);
            Contracts.Assert(args.Length == argTypes.Length);
            Contracts.Assert(args.Length == 2);
            Contracts.AssertValue(errors);
            Contracts.Assert(MinArity <= args.Length && args.Length <= MaxArity);

            bool fValid = base.CheckInvocation(args, argTypes, errors, out returnType);
            Contracts.Assert(returnType.IsTable);

            // Typecheck the input table
            fValid &= CheckStringColumnType(argTypes[0], args[0], errors);

            returnType = argTypes[0];

            return fValid;
        }
    }

    // Table overloads of Left and Right:
    //  Left(arg:*[_:s], count:*[_:n])
    //  Right(arg:*[_:s], count:*[_:n])
    internal sealed class LeftRightTableTableFunction : BuiltinFunction
    {
        public override bool RequiresErrorContext => true;
        public override bool IsSelfContained => true;

        public LeftRightTableTableFunction(bool isLeft)
            : base(isLeft ? "Left" : "Right", isLeft ? TexlStrings.AboutLeftT : TexlStrings.AboutRightT, FunctionCategories.Table, DType.EmptyTable, 0, 2, 2, DType.EmptyTable, DType.EmptyTable)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.LeftRightTArg1, TexlStrings.LeftRightArg2 };
        }

        public override string GetUniqueTexlRuntimeName(bool isPrefetching = false)
        {
            // Disambiguate these from the scalar overloads, so we don't have to
            // do type checking in the JS (runtime) implementation of Left/Right.
            return GetUniqueTexlRuntimeName(suffix: "_TT");
        }

        public override bool CheckInvocation(TexlNode[] args, DType[] argTypes, IErrorContainer errors, out DType returnType)
        {
            Contracts.AssertValue(args);
            Contracts.AssertAllValues(args);
            Contracts.AssertValue(argTypes);
            Contracts.Assert(args.Length == argTypes.Length);
            Contracts.Assert(args.Length == 2);
            Contracts.AssertValue(errors);
            Contracts.Assert(MinArity <= args.Length && args.Length <= MaxArity);

            bool fValid = base.CheckInvocation(args, argTypes, errors, out returnType);
            Contracts.Assert(returnType.IsTable);

            // Typecheck the input table
            fValid &= CheckStringColumnType(argTypes[0], args[0], errors);

            // Typecheck the count table
            fValid &= CheckNumericColumnType(argTypes[1], args[1], errors);

            returnType = argTypes[0];

            return fValid;
        }
    }

    // Table overloads of Left and Right:
    //  Left(arg:s, count:*[_:n])
    //  Right(arg:s, count:*[_:n])
    internal sealed class LeftRightScalarTableFunction : BuiltinFunction
    {
        public override bool RequiresErrorContext => true;
        public override bool IsSelfContained => true;

        public LeftRightScalarTableFunction(bool isLeft)
            : base(isLeft ? "Left" : "Right", isLeft ? TexlStrings.AboutLeftT : TexlStrings.AboutRightT, FunctionCategories.Table, DType.EmptyTable, 0, 2, 2, DType.String, DType.EmptyTable)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.LeftRightArg1, TexlStrings.LeftRightArg2 };
        }

        public override string GetUniqueTexlRuntimeName(bool isPrefetching = false)
        {
            // Disambiguate these from the scalar overloads, so we don't have to
            // do type checking in the JS (runtime) implementation of Left/Right.
            return GetUniqueTexlRuntimeName(suffix: "_ST");
        }

        public override bool CheckInvocation(TexlNode[] args, DType[] argTypes, IErrorContainer errors, out DType returnType)
        {
            Contracts.AssertValue(args);
            Contracts.AssertAllValues(args);
            Contracts.AssertValue(argTypes);
            Contracts.Assert(args.Length == argTypes.Length);
            Contracts.Assert(args.Length == 2);
            Contracts.AssertValue(errors);
            Contracts.Assert(MinArity <= args.Length && args.Length <= MaxArity);

            bool fValid = base.CheckInvocation(args, argTypes, errors, out returnType);
            Contracts.Assert(returnType.IsTable);

            // Typecheck the count table
            fValid &= CheckNumericColumnType(argTypes[1], args[1], errors);

            // Synthesize a new return type
            returnType = DType.CreateTable(new TypedName(DType.String, OneColumnTableResultName));

            return fValid;
        }
    }
}
