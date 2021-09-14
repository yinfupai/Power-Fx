//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.Delegation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Microsoft.AppMagic.Authoring
{
    internal sealed partial class DelegationMetadata : IDelegationMetadata
    {
        private sealed class SortMetaParser : MetaParser
        {
            public override OperationCapabilityMetadata Parse(JsonElement dataServiceCapabilitiesJsonObject, DType schema)
            {
                Contracts.AssertValid(schema);

                Dictionary<DPath, DelegationCapability> columnRestrictions = new Dictionary<DPath, DelegationCapability>();
                if (!dataServiceCapabilitiesJsonObject.TryGetProperty(CapabilitiesConstants.Sort_Restriction, out var sortRestrictionJsonObject))
                    return null;

                if (sortRestrictionJsonObject.TryGetProperty(CapabilitiesConstants.Sort_UnsortableProperties, out var unSortablePropertiesJsonArray))
                {
                    foreach (var prop in unSortablePropertiesJsonArray.EnumerateArray())
                    {
                        var columnName = new DName(prop.GetString());
                        var columnPath = DPath.Root.Append(columnName);

                        if (!columnRestrictions.ContainsKey(columnPath))
                            columnRestrictions.Add(columnPath, new DelegationCapability(DelegationCapability.Sort));
                    }
                }

                if (sortRestrictionJsonObject.TryGetProperty(CapabilitiesConstants.Sort_AscendingOnlyProperties, out var acendingOnlyPropertiesJsonArray))
                {
                    foreach (var prop in acendingOnlyPropertiesJsonArray.EnumerateArray())
                    {
                        var columnName = new DName(prop.GetString());
                        var columnPath = DPath.Root.Append(columnName);

                        if (!columnRestrictions.ContainsKey(columnPath))
                        {
                            columnRestrictions.Add(columnPath, new DelegationCapability(DelegationCapability.SortAscendingOnly));
                            continue;
                        }

                        var existingRestrictions = columnRestrictions[columnPath].Capabilities;
                        columnRestrictions[columnPath] = new DelegationCapability(existingRestrictions | DelegationCapability.SortAscendingOnly);
                    }
                }

                return new SortOpMetadata(schema, columnRestrictions);
            }
        }
    }
}