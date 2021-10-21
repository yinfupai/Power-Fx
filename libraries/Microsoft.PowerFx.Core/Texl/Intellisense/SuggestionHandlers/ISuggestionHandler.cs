//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.PowerFx.Core.Texl.Intellisense{
    internal interface ISuggestionHandler
    {
        /// <summary>
        /// Adds suggestions as appropriate to the internal Suggestions and SubstringSuggestions lists of intellisenseData.
        /// Returns true if suggestions are found and false otherwise.
        /// </summary>
        bool Run(IntellisenseData.IntellisenseData intellisenseData);
    }
}
