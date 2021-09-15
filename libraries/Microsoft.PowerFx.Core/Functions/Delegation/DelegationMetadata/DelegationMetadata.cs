//------------------------------------------------------------------------------
// <copyright file="DelegationMetadata.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AppMagic.Authoring.Texl;
using Microsoft.PowerFx.Core.Delegation;

namespace Microsoft.AppMagic.Authoring
{
    /// <summary>
    /// This represents a delegatable operation metadata about the imported delegatable CdpDataSourceInfo.
    /// </summary>
    internal sealed partial class DelegationMetadata : IDelegationMetadata
    {
        private readonly CompositeCapabilityMetadata _compositeMetadata;

        public DelegationMetadata(DType schema, string delegationMetadataJson)
        {
            Contracts.AssertValid(schema);

            var metadataParser = new DelegationMetadataParser();
            _compositeMetadata = metadataParser.Parse(delegationMetadataJson, schema);
            Contracts.AssertValue(_compositeMetadata);

            Schema = schema;
        }
        
        public Dictionary<DPath, DPath> ODataPathReplacementMap { get { return _compositeMetadata.ODataPathReplacementMap; } }

        public SortOpMetadata SortDelegationMetadata { get { return _compositeMetadata.SortDelegationMetadata; } }
        public FilterOpMetadata FilterDelegationMetadata { get { return _compositeMetadata.FilterDelegationMetadata; } }
        public GroupOpMetadata GroupDelegationMetadata { get { return _compositeMetadata.GroupDelegationMetadata; } }
        public DType Schema { get; }
        public DelegationCapability TableAttributes { get { return _compositeMetadata.TableAttributes; } }
        public DelegationCapability TableCapabilities { get { return _compositeMetadata.TableCapabilities; } }
    }
}
