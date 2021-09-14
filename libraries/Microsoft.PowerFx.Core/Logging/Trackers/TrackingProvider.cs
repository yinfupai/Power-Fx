//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring.Texl;
using Microsoft.PowerFx.Core.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.PowerFx.Core.Logging.Trackers
{
    internal class TrackingProvider
    {
        public readonly static TrackingProvider Instance = new TrackingProvider();
        internal event EventHandler<IAddSuggestionMessageEventArgs> AddSuggestionEvent;
        internal event EventHandler<IDelegationTrackerEventArgs> DelegationTrackerEvent;

        internal void AddSuggestionMessage(string message, TexlNode node, TexlBinding binding)
        {
            AddSuggestionEvent?.Invoke(this, new AddSuggestionMessageEventArgs(message, node, binding));
        }

        internal void SetDelegationTrackerStatus(DelegationStatus status, TexlNode node, 
            TexlBinding binding, TexlFunction func, DelegationTelemetryInfo logInfo = null)
        {
            DelegationTrackerEvent?.Invoke(this, new DelegationTrackerEventArgs(status, node, binding, func, logInfo));
        }
    }
}
