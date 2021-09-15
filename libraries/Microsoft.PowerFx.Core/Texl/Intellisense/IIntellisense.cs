//------------------------------------------------------------------------------
// <copyright file="IIntellisense.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal interface IIntellisense
    {
        /// <summary>
        /// Returns the result depending on the context.
        /// </summary>
        IIntellisenseResult Suggest(IntellisenseContext context, TexlBinding binding, Formula formula);
    }
}
