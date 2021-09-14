//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Sqrt(number:n)
    // Equivalent DAX function: Sqrt
    internal sealed class SqrtFunction : MathOneArgFunction
    {
        public override bool RequiresErrorContext => true;

        public SqrtFunction()
            : base("Sqrt", TexlStrings.AboutSqrt, FunctionCategories.MathAndStat)
        { }
    }

    // Sqrt(E:*[n])
    // Table overload that computes the square root values of each item in the input table.
    internal sealed class SqrtTableFunction : MathOneArgTableFunction
    {
        public override bool RequiresErrorContext => true;

        public SqrtTableFunction()
            : base("Sqrt", TexlStrings.AboutSqrtT, FunctionCategories.Table)
        { }
    }
}
