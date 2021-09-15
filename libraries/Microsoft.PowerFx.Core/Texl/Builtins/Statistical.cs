//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AppMagic.Common.Telemetry;
using Microsoft.PowerFx.Core.Logging.Trackers;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Abstract base class for all statistical functions with similar signatures that take
    // scalar arguments.
    internal abstract class StatisticalFunction : BuiltinFunction
    {
        public override bool SupportsParamCoercion => true;
        public override bool IsSelfContained => true;

        public StatisticalFunction(string name, TexlStrings.StringGetter description, FunctionCategories fc)
            : base(name, description, fc, DType.Number, 0, 1, int.MaxValue, DType.Number)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new[] { TexlStrings.StatisticalArg };
            yield return new[] { TexlStrings.StatisticalArg, TexlStrings.StatisticalArg };
            yield return new[] { TexlStrings.StatisticalArg, TexlStrings.StatisticalArg, TexlStrings.StatisticalArg };
        }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures(int arity)
        {
            if (arity > 2)
                return GetGenericSignatures(arity, TexlStrings.StatisticalArg, TexlStrings.StatisticalArg);
            return base.GetSignatures(arity);
        }

        public override bool CheckInvocation(TexlNode[] args, DType[] argTypes, IErrorContainer errors, out DType returnType, out Dictionary<TexlNode, DType> nodeToCoercedTypeMap)
        {
            Contracts.AssertValue(args);
            Contracts.AssertAllValues(args);
            Contracts.AssertValue(argTypes);
            Contracts.Assert(args.Length == argTypes.Length);
            Contracts.AssertValue(errors);
            Contracts.Assert(MinArity <= args.Length && args.Length <= MaxArity);

            bool fValid = base.CheckInvocation(args, argTypes, errors, out returnType, out nodeToCoercedTypeMap);
            Contracts.Assert(returnType == DType.Number);

            bool matchedWithCoercion;

            // Ensure that all the arguments are numeric/coercible to numeric.
            for (int i = 0; i < argTypes.Length; i++)
            {
                if (CheckType(args[i], argTypes[i], DType.Number, DefaultErrorContainer, out matchedWithCoercion))
                {
                    if (matchedWithCoercion)
                        CollectionUtils.Add(ref nodeToCoercedTypeMap, args[i], DType.Number, allowDupes: true);
                }
                else
                {
                    errors.EnsureError(DocumentErrorSeverity.Severe, args[i], TexlStrings.ErrNumberExpected);
                    fValid = false;
                }
            }

            if (!fValid)
                nodeToCoercedTypeMap = null;

            return fValid;
        }
    }
}
