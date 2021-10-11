//------------------------------------------------------------------------------
// <copyright file="IsToday.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.PowerFx.Core.App.ErrorContainers;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // IsToday(date: d): b
    internal sealed class IsTodayFunction : BuiltinFunction
    {
        // Multiple invocations may result in different return values.
        public override bool IsStateless => false;
        public override bool IsSelfContained => true;
        public override bool SupportsParamCoercion => true;

        public IsTodayFunction()
            : base("IsToday", TexlStrings.AboutIsToday, FunctionCategories.Information, DType.Boolean, 0, 1, 1, DType.DateTime)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            return EnumerableUtils.Yield(new[] { TexlStrings.IsTodayFuncArg1 });
        }

        public override bool CheckInvocation(TexlBinding binding, TexlNode[] args, DType[] argTypes, IErrorContainer errors, out DType returnType, out Dictionary<TexlNode, DType> nodeToCoercedTypeMap)
        {
            Contracts.AssertValue(args);
            Contracts.AssertAllValues(args);
            Contracts.AssertValue(argTypes);
            Contracts.Assert(args.Length == argTypes.Length);
            Contracts.AssertValue(errors);
            Contracts.Assert(MinArity <= args.Length && args.Length <= MaxArity);

            bool fValid = base.CheckInvocation(args, argTypes, errors, out returnType, out nodeToCoercedTypeMap);

            DType type0 = argTypes[0];

            // Arg0 should not be a Time
            if (type0.Kind == DKind.Time)
            {
                fValid = false;
                errors.EnsureError(DocumentErrorSeverity.Severe, args[0], TexlStrings.ErrDateExpected);
            }

            returnType = ReturnType;

            return fValid;
        }
    }
}