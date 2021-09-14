//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Tan(number:n)
    // Equivalent Excel function: Tan
    internal sealed class TanFunction : MathOneArgFunction
    {
        public TanFunction()
            : base("Tan", TexlStrings.AboutTan, FunctionCategories.MathAndStat)
        { }
    }

    // Tan(E:*[n])
    // Table overload that computes the tangent of each item in the input table.
    internal sealed class TanTableFunction : MathOneArgTableFunction
    {
        public TanTableFunction()
            : base("Tan", TexlStrings.AboutTanT, FunctionCategories.Table)
        { }
    }
}
