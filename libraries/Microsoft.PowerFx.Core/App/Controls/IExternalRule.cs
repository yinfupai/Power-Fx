//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AppMagic.Authoring.Texl;
using Microsoft.PowerFx.Core.Entities.QueryOptions;
using Microsoft.PowerFx.Core.Logging.Trackers;

namespace Microsoft.PowerFx.Core.App.Controls
{
    internal interface IExternalRule
    {
        Dictionary<int, DataSourceToQueryOptionsMap> TexlNodeQueryOptions { get; }
        IExternalDocument Document { get; }
        TexlBinding Binding { get; }
        bool HasErrors { get; }
        bool HasControlPropertyDependency(string referencedControlUniqueId);

        void SetDelegationTrackerStatus(TexlNode node, DelegationStatus status, DelegationTelemetryInfo logInfo, TexlFunction func);        
    }
}