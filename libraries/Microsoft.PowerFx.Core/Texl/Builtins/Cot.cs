//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Cot(number:n)
    // Equivalent Excel function: Cot
    internal sealed class CotFunction : MathOneArgFunction
    {
        public CotFunction()
            : base("Cot", TexlStrings.AboutCot, FunctionCategories.MathAndStat)
        { }
    }

    // Cot(E:*[n])
    // Table overload that computes the cotangent of each item in the input table.
    internal sealed class CotTableFunction : MathOneArgTableFunction
    {
        public CotTableFunction()
            : base("Cot", TexlStrings.AboutCotT, FunctionCategories.Table)
        { }
    }
}
