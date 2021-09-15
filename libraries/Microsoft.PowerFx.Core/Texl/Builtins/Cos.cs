//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Cos(number:n)
    // Equivalent Excel function: Cos
    internal sealed class CosFunction : MathOneArgFunction
    {
        public CosFunction()
            : base("Cos", TexlStrings.AboutCos, FunctionCategories.MathAndStat)
        { }
    }

    // Cos(E:*[n])
    // Table overload that computes the cosine of each item in the input table.
    internal sealed class CosTableFunction : MathOneArgTableFunction
    {
        public CosTableFunction()
            : base("Cos", TexlStrings.AboutCosT, FunctionCategories.Table)
        { }
    }
}
