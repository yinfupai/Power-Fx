//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AppMagic.Common;
using Microsoft.PowerFx.Core.Delegation;

namespace Microsoft.AppMagic.Authoring
{
    internal sealed partial class DelegationMetadata : IDelegationMetadata
    {
        private abstract class MetaParser
        {
            public abstract OperationCapabilityMetadata Parse(JsonElement dataServiceCapabilitiesJsonObject, DType Schema);

            protected DelegationCapability ParseColumnCapability(JsonElement columnCapabilityJsonObj, string capabilityKey)
            {
                Contracts.AssertNonEmpty(capabilityKey);

                // Retrieve the entry for the column using column name as key.
                if (!columnCapabilityJsonObj.TryGetProperty(capabilityKey, out var functionsJsonArray))
                    return DelegationCapability.None;

                DelegationCapability columnCapability = DelegationCapability.None;
                foreach (var op in functionsJsonArray.EnumerateArray())
                {
                    var operatorStr = op.GetString();
                    Contracts.AssertNonEmpty(operatorStr);

                    // If we don't support the operator then don't look at this capability.
                    if (!DelegationCapability.OperatorToDelegationCapabilityMap.ContainsKey(operatorStr))
                        continue;

                    columnCapability |= DelegationCapability.OperatorToDelegationCapabilityMap[operatorStr];
                }

                return columnCapability;
            }
        }
    }
}