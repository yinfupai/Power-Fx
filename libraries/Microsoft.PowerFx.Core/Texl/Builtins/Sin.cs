//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Sin(number:n)
    // Equivalent Excel function: Sin
    internal sealed class SinFunction : MathOneArgFunction
    {
        public SinFunction()
            : base("Sin", TexlStrings.AboutSin, FunctionCategories.MathAndStat)
        { }
    }

    // Sin(E:*[n])
    // Table overload that computes the sine values of each item in the input table.
    internal sealed class SinTableFunction : MathOneArgTableFunction
    {
        public SinTableFunction()
            : base("Sin", TexlStrings.AboutSinT, FunctionCategories.Table)
        { }
    }
}