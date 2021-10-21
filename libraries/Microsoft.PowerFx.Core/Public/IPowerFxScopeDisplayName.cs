//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.PowerFx.Core.Public
{
    /// <summary>
    /// Provide display name translation
    /// </summary>
    public interface IPowerFxScopeDisplayName
    {
        /// <summary>
        /// Translate entity logical name to display name
        /// </summary>
        string TranslateToDisplayName(string expression);
    }
}
