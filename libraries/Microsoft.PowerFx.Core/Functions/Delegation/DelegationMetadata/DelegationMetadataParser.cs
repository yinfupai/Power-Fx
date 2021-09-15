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
        private sealed class DelegationMetadataParser
        {
            public CompositeCapabilityMetadata Parse(string delegationMetadataJson, DType tableSchema)
            {
                Contracts.AssertValid(tableSchema);

                using var result = JsonDocument.Parse(delegationMetadataJson);

                CompositeMetaParser compositeParser = new CompositeMetaParser();
                compositeParser.AddMetaParser(new SortMetaParser());
                compositeParser.AddMetaParser(new FilterMetaParser());
                compositeParser.AddMetaParser(new GroupMetaParser());
                compositeParser.AddMetaParser(new ODataMetaParser());

                var dataServiceCapabilitiesJsonObject = result.RootElement;

                return compositeParser.Parse(dataServiceCapabilitiesJsonObject, tableSchema) as CompositeCapabilityMetadata;
            }
        }
    }
}
