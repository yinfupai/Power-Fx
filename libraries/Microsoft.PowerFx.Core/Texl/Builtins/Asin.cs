//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Asin(number:n)
    // Equivalent Excel function: Asin
    internal sealed class AsinFunction : MathOneArgFunction
    {
        public AsinFunction()
            : base("Asin", TexlStrings.AboutAsin, FunctionCategories.MathAndStat)
        { }
    }

    // Asin(E:*[n])
    // Table overload that computes the arc sine values of each item in the input table.
    internal sealed class AsinTableFunction : MathOneArgTableFunction
    {
        public AsinTableFunction()
            : base("Asin", TexlStrings.AboutAsinT, FunctionCategories.Table)
        { }
    }
}