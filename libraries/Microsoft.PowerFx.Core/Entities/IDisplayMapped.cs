//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.UtilityDataStructures;

namespace Microsoft.PowerFx.Core.Entities
{
    public interface IDisplayMapped<T>
    {
        bool IsConvertingDisplayNameMapping { get; }

        /// <summary>
        /// Maps logical names to display names.
        /// </summary>
        public BidirectionalDictionary<T, string> DisplayNameMapping { get; }

        /// <summary>
        /// Display Mapped objects occasionally change their display names, in which case we need
        /// both the new and old display names to correctly rewrite them in rules.
        /// </summary>
        public BidirectionalDictionary<T, string> PreviousDisplayNameMapping { get; }
    }
}