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
        private sealed class CompositeMetaParser : MetaParser
        {
            private readonly List<MetaParser> _metaParsers;

            public CompositeMetaParser()
            {
                _metaParsers = new List<MetaParser>();
            }

            public override OperationCapabilityMetadata Parse(JsonElement dataServiceCapabilitiesJsonObject, DType schema)
            {
                Contracts.AssertValid(schema);

                List<OperationCapabilityMetadata> capabilities = new List<OperationCapabilityMetadata>();
                foreach (var parser in _metaParsers)
                {
                    var capabilityMetadata = parser.Parse(dataServiceCapabilitiesJsonObject, schema);
                    if (capabilityMetadata != null)
                        capabilities.Add(capabilityMetadata);
                }

                return new CompositeCapabilityMetadata(schema, capabilities);
            }

            public void AddMetaParser(MetaParser metaParser)
            {
                Contracts.AssertValue(metaParser);

                _metaParsers.Add(metaParser);
            }
        }
    }
}