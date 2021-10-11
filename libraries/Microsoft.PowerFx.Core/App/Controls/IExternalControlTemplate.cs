//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AppMagic.Authoring;
using Microsoft.PowerFx.Core.App.Components;

namespace Microsoft.PowerFx.Core.App.Controls
{
    internal interface IExternalControlTemplate
    {
        ComponentType ComponentType { get; }
        bool IncludesThisItemInSpecificProperty { get; }
        bool ReplicatesNestedControls { get; }
        IEnumerable<DName> NestedAwareTableOutputs { get; }
        bool IsComponent { get; }
        bool HasExpandoProperties { get; }
        IEnumerable<IExternalControlProperty> ExpandoProperties { get; }
        string ThisItemInputInvariantName { get; }
        IExternalControlProperty PrimaryOutputProperty { get; }
        bool IsMetaLoc { get; }
        bool IsCommandComponent { get; }
        bool TryGetProperty(string name, out IExternalControlProperty controlProperty);
        bool TryGetInputProperty(string resolverCurrentProperty, out IExternalControlProperty currentProperty);
        bool HasProperty(string currentPropertyValue, PropertyRuleCategory category);
        bool TryGetOutputProperty(string name, out IExternalControlProperty externalControlProperty);
        bool HasOutput(DName rightName);
    }
}