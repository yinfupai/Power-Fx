//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Acot(number:n)
    // Equivalent Excel function: Acot
    internal sealed class AcotFunction : MathOneArgFunction
    {
        public AcotFunction()
            : base("Acot", TexlStrings.AboutAcot, FunctionCategories.MathAndStat)
        { }
    }

    // Acot(E:*[n])
    // Table overload that computes the arc cotangent of each item in the input table.
    internal sealed class AcotTableFunction : MathOneArgTableFunction
    {
        public AcotTableFunction()
            : base("Acot", TexlStrings.AboutAcotT, FunctionCategories.Table)
        { }
    }
}
