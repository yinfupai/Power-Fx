//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic;
using Microsoft.AppMagic.Authoring;
using Microsoft.AppMagic.Authoring.Texl;
using Microsoft.PowerFx.Core.App;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.PowerFx.Core.App.Controls;

namespace Microsoft.PowerFx.Core.Logging.Trackers
{
    // Helps migrate from DelegationTracker in DocumentServer.Core
    internal class DelegationTrackerCore
    {
        // Marks telemetry in the rule. Needed in canvas, not needed in PowerFx.Core
        // DelegationTracker.SetDelegationTrackerStatus(DelegationStatus.NotANumberArgType, callNode, binding, this, DelegationTelemetryInfo.CreateEmptyDelegationTelemetryInfo());
        public static void SetDelegationTrackerStatus(DelegationStatus status, TexlNode node, TexlBinding binding, TexlFunction func, DelegationTelemetryInfo logInfo = null)
        {
            Contracts.AssertValue(node);
            Contracts.AssertValue(binding);
            Contracts.AssertValue(func);
            Contracts.AssertValueOrNull(logInfo);

            IExternalRule rule;
            // The rule need not exist on ControlInfo yet. This happens when we are attempting
            // to create a namemap in which case we try to bind the rule before adding it to the control.
            if (!TryGetCurrentRule(binding, out rule))
                return;

            rule.SetDelegationTrackerStatus(node, status, logInfo ?? DelegationTelemetryInfo.CreateEmptyDelegationTelemetryInfo(), func);
        }

        internal static bool TryGetCurrentRule(TexlBinding binding, out IExternalRule rule)
        {
            Contracts.AssertValue(binding);

            rule = null;

            var entity = binding.NameResolver?.CurrentEntity;
            DName? currentProperty = binding.NameResolver?.CurrentProperty;
            if (entity == null || !currentProperty.HasValue || !currentProperty.Value.IsValid)
                return false;

            return entity.TryGetRule(currentProperty.Value, out rule);
        }
    }
}
