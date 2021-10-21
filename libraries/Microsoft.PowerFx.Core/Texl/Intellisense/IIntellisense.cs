//------------------------------------------------------------------------------
// <copyright file="IIntellisense.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.Binding;
using Microsoft.PowerFx.Core.Syntax;

namespace Microsoft.PowerFx.Core.Texl.Intellisense
{
    internal interface IIntellisense
    {
        /// <summary>
        /// Returns the result depending on the context.
        /// </summary>
        IIntellisenseResult Suggest(IntellisenseContext context, TexlBinding binding, Formula formula);
    }
}
