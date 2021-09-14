//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AppMagic.Authoring;
using PowerApps.Language.Entities;

namespace Microsoft.PowerFx.Core.App
{
    internal interface IExternalDocument
    {
        IExternalDocumentProperties Properties { get; }
        IExternalEntityScope GlobalScope { get; }
        IEnumerable<IDocumentError> Errors { get; }
        IExternalControl AppInfoControl { get; }
        bool TryGetControlByUniqueId(string name, out IExternalControl control);
    }
}