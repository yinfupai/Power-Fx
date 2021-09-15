//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AppMagic.Authoring;
using Microsoft.PowerFx.Core.App;

namespace PowerApps.Language.Entities
{
    internal interface IExternalEntity
    {
        DName EntityName { get; }
        string InvariantName { get; }
        bool IsControl { get; }
        IExternalEntityScope EntityScope { get; }
        IEnumerable<IDocumentError> Errors { get; }
        bool TryGetRule(DName propertyName, out IExternalRule rule);
    }
}