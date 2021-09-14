//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Radians(number:n)
    // Equivalent Excel function: Radians
    internal sealed class RadiansFunction : MathOneArgFunction
    {
        public RadiansFunction()
            : base("Radians", TexlStrings.AboutRadians, FunctionCategories.MathAndStat)
        { }
    }

    // Radians(E:*[n])
    // Table overload that computes the radians values of each item in the input table.
    internal sealed class RadiansTableFunction : MathOneArgTableFunction
    {
        public RadiansTableFunction()
            : base("Radians", TexlStrings.AboutRadiansT, FunctionCategories.Table)
        { }
    }
}
