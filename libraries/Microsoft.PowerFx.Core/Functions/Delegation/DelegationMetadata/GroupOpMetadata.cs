//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.AppMagic.Authoring
{
    // Defines group operation metadata.
    internal sealed class GroupOpMetadata : OperationCapabilityMetadata
    {
        private readonly Dictionary<DPath, DelegationCapability> _columnRestrictions;

        public GroupOpMetadata(DType schema, Dictionary<DPath, DelegationCapability> columnRestrictions)
            : base(schema)
        {
            Contracts.AssertValue(columnRestrictions);

            _columnRestrictions = columnRestrictions;
        }

        protected override Dictionary<DPath, DelegationCapability> ColumnRestrictions { get { return _columnRestrictions; } }

        public override DelegationCapability DefaultColumnCapabilities { get { return DelegationCapability.Group; } }

        public override DelegationCapability TableCapabilities { get { return DefaultColumnCapabilities; } }
    }
}
