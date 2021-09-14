//------------------------------------------------------------------------------
// <copyright file="Coalesce.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Coalesce(expression:E)
    // Equivalent T-SQL Function: COALESCE
    internal sealed class CoalesceFunction : BuiltinFunction
    {
        public override bool IsSelfContained => true;

        public CoalesceFunction()
            : base("Coalesce", TexlStrings.AboutCoalesce, FunctionCategories.Information, DType.Unknown, 0, 1, int.MaxValue)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new[] { TexlStrings.CoalesceArg1 };
            yield return new[] { TexlStrings.CoalesceArg1, TexlStrings.CoalesceArg1 };
            yield return new[] { TexlStrings.CoalesceArg1, TexlStrings.CoalesceArg1, TexlStrings.CoalesceArg1 };
        }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures(int arity)
        {
            if (arity > 2)
                return GetGenericSignatures(arity, TexlStrings.CoalesceArg1);
            return base.GetSignatures(arity);
        }

        public override bool CheckInvocation(TexlNode[] args, DType[] argTypes, IErrorContainer errors, out DType returnType)
        {
            Contracts.AssertValue(args);
            Contracts.AssertValue(argTypes);
            Contracts.Assert(args.Length == argTypes.Length);
            Contracts.Assert(args.Length >= 1);
            Contracts.AssertValue(errors);

            int count = args.Length;
            bool fArgsValid = true;
            bool fArgsNonNull = false;

            returnType = DType.Unknown;

            for (int i = 0; i < count; ++i)
            {
                if (argTypes[i].Kind == DKind.ObjNull)
                    continue;
                fArgsNonNull = true;
                if (returnType.Accepts(argTypes[i]))
                    continue;
                else if (argTypes[i].Accepts(returnType))
                    returnType = argTypes[i];
                else
                    fArgsValid &= false;
            }

            if (!fArgsNonNull)
                returnType = DType.ObjNull;

            return fArgsValid;
        }
    }
}
