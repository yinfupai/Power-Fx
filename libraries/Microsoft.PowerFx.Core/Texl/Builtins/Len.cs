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
    // Len(arg:s)
    // Corresponding DAX function: Len
    internal sealed class LenFunction : StringOneArgFunction
    {
        public override bool HasPreciseErrors => true;

        public LenFunction()
            : base("Len", TexlStrings.AboutLen, FunctionCategories.Text, DType.Number)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.LenArg1 };
        }

        public override DelegationCapability FunctionDelegationCapability { get { return DelegationCapability.Length; } }

        public override bool IsRowScopedServerDelegatable(CallNode callNode, TexlBinding binding, OperationCapabilityMetadata metadata)
        {
            return base.IsRowScopedServerDelegatable(callNode, binding, metadata);
        }
    }

    // Len(arg:*[s])
    internal sealed class LenTFunction : BuiltinFunction
    {
        public override bool IsSelfContained => true;

        public LenTFunction()
            : base("Len", TexlStrings.AboutLenT, FunctionCategories.Table, DType.EmptyTable, 0, 1, 1, DType.EmptyTable)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.LenTArg1 };
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
            fValid &= CheckStringColumnType(argTypes[0], args[0], errors);

            // Synthesize a new return type
            returnType = DType.CreateTable(new TypedName(DType.Number, OneColumnTableResultName));

            return fValid;
        }
    }

}
