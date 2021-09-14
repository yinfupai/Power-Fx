//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------


namespace Microsoft.AppMagic.Authoring
{
    internal interface IExternalDataEntityMetadataProvider
    {
        bool TryGetEntityMetadata(string expandInfoIdentity, out IDataEntityMetadata entityMetadata);
    }
}