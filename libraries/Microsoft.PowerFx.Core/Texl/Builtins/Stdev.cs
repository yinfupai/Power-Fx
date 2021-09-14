//------------------------------------------------------------------------------
// <copyright file="Stdev.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring.Texl
{
    // StdevP(arg1:n, arg2:n, ..., argN:n)
    // Corresponding Excel function: STDEV.P
    internal sealed class StdevPFunction : StatisticalFunction
    {
        public override bool RequiresErrorContext => true;

        public StdevPFunction()
            : base("StdevP", TexlStrings.AboutStdevP, FunctionCategories.MathAndStat)
        { }
    }

    // StdevP(source:*, projection:n)
    // Corresponding DAX function: STDEV.P
    internal sealed class StdevPTableFunction : StatisticalTableFunction
    {
        public override bool RequiresErrorContext => true;
        
        public StdevPTableFunction()
            : base("StdevP", TexlStrings.AboutStdevPT, FunctionCategories.Table)
        { }
    }
}
