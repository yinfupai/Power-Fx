//------------------------------------------------------------------------------
// <copyright file="IsToday.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // IsToday(date: d): b
    internal sealed class IsTodayFunction : BuiltinFunction
    {
        // Multiple invocations may result in different return values.
        public override bool IsStateless => false;
        public override bool IsSelfContained => true;

        public IsTodayFunction()
            : base("IsToday", TexlStrings.AboutIsToday, FunctionCategories.Information, DType.Boolean, 0, 1, 1, DType.DateTime)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            return EnumerableUtils.Yield(new[] { TexlStrings.IsTodayFuncArg1 });
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

            // Arg0 should be either a DateTime or Date.
            if (!(type0.Kind == DKind.Date || type0.Kind == DKind.DateTime))
            {
                fValid = false;
                errors.EnsureError(DocumentErrorSeverity.Severe, args[0], TexlStrings.ErrDateExpected);
            }

            returnType = ReturnType;

            return fValid;
        }
    }
}