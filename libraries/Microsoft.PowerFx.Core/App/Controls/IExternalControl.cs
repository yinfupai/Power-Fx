//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;
using Microsoft.PowerFx.Core.Types;
using PowerApps.Language.Entities;

namespace Microsoft.PowerFx.Core.App
{
    internal interface IExternalControl : IExternalEntity
    {
        bool IsDataComponentDefinition { get; }
        bool IsDataComponentInstance { get; }
        IExternalControlTemplate Template { get; }
        bool IsComponentControl { get; }
        IExternalControl TopParentOrSelf { get; }
        string DisplayName { get; }
        bool IsReplicable { get; }
        bool IsAppInfoControl { get; }
        DType ThisItemType { get; }
        bool IsAppGlobalControl { get; }
        string UniqueId { get; }
        bool IsComponentInstance { get; }
        bool IsComponentDefinition { get; }
        bool IsCommandComponentInstance { get; }
        IExternalControlType GetControlDType();
        IExternalControlType GetControlDType(bool calculateAugmentedExpandoType, bool isDataLimited);
        bool IsDescendentOf(IExternalControl controlInfo);
        IExternalRule GetRule(string propertyInvariantName);
        bool TryGetRule(string dName, out IExternalRule rule);
    }
}