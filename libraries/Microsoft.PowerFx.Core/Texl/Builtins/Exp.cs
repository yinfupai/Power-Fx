//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Exp(number:n)
    // Equivalent Excel function: Exp
    internal sealed class ExpFunction : MathOneArgFunction
    {
        public override bool HasPreciseErrors => true;

        public ExpFunction()
            : base("Exp", TexlStrings.AboutExp, FunctionCategories.MathAndStat)
        { }
    }

    // Exp(E:*[n])
    // Table overload that computes the E raised to the respective values of each item in the input table.
    internal sealed class ExpTableFunction : MathOneArgTableFunction
    {
        public override bool HasPreciseErrors => true;

        public ExpTableFunction()
            : base("Exp", TexlStrings.AboutExpT, FunctionCategories.Table)
        { }
    }
}
