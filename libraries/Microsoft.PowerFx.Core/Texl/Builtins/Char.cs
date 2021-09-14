//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Char(arg:n) : s
    // Corresponding Excel function: Char
    internal sealed class CharFunction : BuiltinFunction
    {
        public override bool RequiresErrorContext => true;
        public override bool IsSelfContained => true;

        public CharFunction()
            : base("Char", TexlStrings.AboutChar, FunctionCategories.Text, DType.String, 0, 1, 1, DType.Number)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.CharArg1 };
        }
    }

    // Char(arg:*[n]) : *[s]
    internal sealed class CharTFunction : BuiltinFunction
    {
        public override bool RequiresErrorContext => true;
        public override bool IsSelfContained => true;

        public CharTFunction()
            : base("Char", TexlStrings.AboutCharT, FunctionCategories.Table, DType.EmptyTable, 0, 1, 1, DType.EmptyTable)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.CharTArg1 };
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
            Contracts.Assert(returnType.IsTable);

            // Typecheck the input table
            fValid &= CheckNumericColumnType(argTypes[0], args[0], errors);

            // Synthesize a new return type
            returnType = DType.CreateTable(new TypedName(DType.String, OneColumnTableResultName));

            return fValid;
        }
    }
}
