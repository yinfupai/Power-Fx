//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.PowerFx.Core.App
{
    internal interface IExternalDocumentProperties
    {
        IExternalEnabledFeatures EnabledFeatures { get; }
        bool SupportsImplicitThisItem { get; }
        Dictionary<string, int> DisallowedFunctions { get; }
    }
}