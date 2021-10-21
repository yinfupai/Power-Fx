//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------


using Microsoft.PowerFx.Core.Functions.Delegation;

namespace Microsoft.PowerFx.Core.Entities.Delegation
{
    internal interface IExternalDataEntityMetadataProvider
    {
        bool TryGetEntityMetadata(string expandInfoIdentity, out IDataEntityMetadata entityMetadata);
    }
}