//------------------------------------------------------------------------------
// <copyright file="Power.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.PowerFx.Core.App.ErrorContainers;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Power(number:n, number:n):n
    // Equivalent DAX function: Power
    internal sealed class PowerFunction : BuiltinFunction
    {
        public override bool SupportsParamCoercion => true;
        public override bool IsSelfContained => true;
        public override bool RequiresErrorContext => true;

        public PowerFunction()
            : base("Power", TexlStrings.AboutPower, FunctionCategories.MathAndStat, DType.Number, 0, 2, 2, DType.Number, DType.Number)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.PowerFuncArg1, TexlStrings.PowerFuncArg2 };
        }
    }

    // Power(number:n|*[n], number:n|*[n]):*[n]
    // Equivalent DAX function: Power
    internal sealed class PowerTFunction : BuiltinFunction
    {
        public override bool SupportsParamCoercion => true;
        public override bool IsSelfContained => true;
        public override bool RequiresErrorContext => true;

        public PowerTFunction()
            : base("Power", TexlStrings.AboutPowerT, FunctionCategories.MathAndStat, DType.EmptyTable, 0, 2, 2)
        { }

        public override string GetUniqueTexlRuntimeName(bool isPrefetching = false)
        {
            return GetUniqueTexlRuntimeName(suffix: "_T");
        }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.PowerTFuncArg1, TexlStrings.PowerTFuncArg2 };
        }

        public override bool CheckInvocation(TexlNode[] args, DType[] argTypes, IErrorContainer errors, out DType returnType, out Dictionary<TexlNode, DType> nodeToCoercedTypeMap)
        {
            Contracts.AssertValue(args);
            Contracts.AssertAllValues(args);
            Contracts.AssertValue(argTypes);
            Contracts.Assert(args.Length == argTypes.Length);
            Contracts.AssertValue(errors);

            bool fValid = base.CheckInvocation(args, argTypes, errors, out returnType, out nodeToCoercedTypeMap);
            fValid &= CheckAllParamsAreTypeOrSingleColumnTable(DType.Number, args, argTypes, errors, out returnType, out nodeToCoercedTypeMap);

            return fValid;
        }
    }
}
