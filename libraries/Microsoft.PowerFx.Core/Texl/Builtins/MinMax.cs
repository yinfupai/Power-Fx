//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Min(arg1:n, arg2:n, ..., argN:n)
    // Max(arg1:n, arg2:n, ..., argN:n)
    // Corresponding Excel functions: Min, Max
    internal sealed class MinMaxFunction : StatisticalFunction
    {
        public MinMaxFunction(bool isMin)
            : base(isMin ? "Min" : "Max", isMin ? TexlStrings.AboutMin : TexlStrings.AboutMax, FunctionCategories.MathAndStat)
        { }
    }
}
