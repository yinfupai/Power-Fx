//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Degrees(number:n)
    // Equivalent Excel function: Degrees
    internal sealed class DegreesFunction : MathOneArgFunction
    {
        public DegreesFunction()
            : base("Degrees", TexlStrings.AboutDegrees, FunctionCategories.MathAndStat)
        { }
    }

    // Degrees(E:*[n])
    // Table overload that computes the degrees values of each item in the input table.
    internal sealed class DegreesTableFunction : MathOneArgTableFunction
    {
        public DegreesTableFunction()
            : base("Degrees", TexlStrings.AboutDegreesT, FunctionCategories.Table)
        { }
    }
}
