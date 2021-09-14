//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Acos(number:n)
    // Equivalent Excel function: acos
    internal sealed class AcosFunction : MathOneArgFunction
    {
        public AcosFunction()
            : base("Acos", TexlStrings.AboutAcos, FunctionCategories.MathAndStat)
        { }
    }

    // Acos(E:*[n])
    // Table overload that computes the arc cosine of each item in the input table.
    internal sealed class AcosTableFunction : MathOneArgTableFunction
    {
        public AcosTableFunction()
            : base("Acos", TexlStrings.AboutAcosT, FunctionCategories.Table)
        { }
    }
}
