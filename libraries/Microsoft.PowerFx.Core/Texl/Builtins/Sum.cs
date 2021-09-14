//------------------------------------------------------------------------------
// <copyright file="Sum.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Sum(arg1:n, arg2:n, ..., argN:n)
    internal sealed class SumFunction : StatisticalFunction
    {
        public override bool RequiresErrorContext => true;

        public SumFunction()
            : base("Sum", TexlStrings.AboutSum, FunctionCategories.MathAndStat)
        { }
    }

    // Sum(source:*, projection:n)
    // Corresponding DAX functions: Sum, SumX
    internal sealed class SumTableFunction : StatisticalTableFunction
    {
        public override bool RequiresErrorContext => true;

        public override DelegationCapability FunctionDelegationCapability { get { return DelegationCapability.Sum; } }

        public SumTableFunction()
            : base("Sum", TexlStrings.AboutSumT, FunctionCategories.Table)
        { }
    }
}
