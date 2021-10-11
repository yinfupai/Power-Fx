//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.PowerFx.Core.App.ErrorContainers;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Split(text:s, separator:s)
    internal class SplitFunction : StringTwoArgFunction
    {
        public SplitFunction()
            : base("Split", TexlStrings.AboutSplit, DType.EmptyTable)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new[] { TexlStrings.SplitArg1, TexlStrings.SplitArg2 };
        }

        public override bool CheckInvocation(TexlBinding binding, TexlNode[] args, DType[] argTypes, IErrorContainer errors, out DType returnType, out Dictionary<TexlNode, DType> nodeToCoercedTypeMap)
        {
            Contracts.AssertValue(args);
            Contracts.AssertAllValues(args);
            Contracts.AssertValue(argTypes);
            Contracts.Assert(args.Length == argTypes.Length);
            Contracts.Assert(args.Length == 2);
            Contracts.AssertValue(errors);

            bool fValid = base.CheckInvocation(args, argTypes, errors, out returnType, out nodeToCoercedTypeMap);
            Contracts.Assert(returnType.IsTable);

            returnType = DType.CreateTable(new TypedName(DType.String, OneColumnTableResultName));
            return fValid;
        }
    }
}
