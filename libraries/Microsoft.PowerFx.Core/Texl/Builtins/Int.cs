//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Int(number:n)
    // Truncate by rounding toward negative infinity.
    internal sealed class IntFunction : MathOneArgFunction
    {
        public IntFunction()
            : base("Int", TexlStrings.AboutInt, FunctionCategories.MathAndStat)
        { }
    }

    // Int(E:*[n])
    // Table overload that applies Int to each item in the input table.
    internal sealed class IntTableFunction : MathOneArgTableFunction
    {
        public IntTableFunction()
            : base("Int", TexlStrings.AboutIntT, FunctionCategories.Table)
        { }
    }
}
