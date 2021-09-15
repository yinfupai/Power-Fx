//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.App;
using Microsoft.PowerFx.Core.Delegation;
using PowerApps.Language.Entities;

namespace Microsoft.AppMagic.Authoring
{
    internal interface IExternalDataSource : IExternalEntity
    {
        public DType Schema { get; }
        string Name { get; }
        bool IsSelectable { get; }
        bool IsDelegatable { get; }
        bool RequiresAsync { get; }
        IExternalDataEntityMetadataProvider DataEntityMetadataProvider { get; }
        bool IsPageable { get; }
        DataSourceKind Kind { get; }
        IExternalTableMetadata TableMetadata { get; }
        IDelegationMetadata DelegationMetadata { get; }
        string ScopeId { get; }
        bool IsComponentScoped { get; }
    }
}