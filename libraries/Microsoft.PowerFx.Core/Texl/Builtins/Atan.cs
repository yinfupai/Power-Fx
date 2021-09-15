//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Atan(number:n)
    // Equivalent Excel function: Atan
    internal sealed class AtanFunction : MathOneArgFunction
    {
        public AtanFunction()
            : base("Atan", TexlStrings.AboutAtan, FunctionCategories.MathAndStat)
        { }
    }

    // Atan(E:*[n])
    // Table overload that computes the arc tangent of each item in the input table.
    internal sealed class AtanTableFunction : MathOneArgTableFunction
    {
        public AtanTableFunction()
            : base("Atan", TexlStrings.AboutAtanT, FunctionCategories.Table)
        { }
    }
}
