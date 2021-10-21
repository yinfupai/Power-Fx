//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.PowerFx.Core.Texl.Intellisense{
    internal interface ISpecialCaseHandler
    {
        /// <summary>
        /// Handles special cases as needed by fixing replacementStartIndex and matchingLength.
        /// Additionally, filters suggestion list if needed
        /// </summary>
        bool Run(IIntellisenseContext context, IntellisenseData.IntellisenseData intellisenseData, List<IntellisenseSuggestion> suggestions);
    }
}