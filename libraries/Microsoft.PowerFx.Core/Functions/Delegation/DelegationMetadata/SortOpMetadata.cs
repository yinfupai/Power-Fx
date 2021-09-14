//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.AppMagic.Authoring
{
    // Defines sort operation metadata.
    internal sealed class SortOpMetadata : OperationCapabilityMetadata
    {
        private readonly Dictionary<DPath, DelegationCapability> _columnRestrictions;

        public SortOpMetadata(DType schema, Dictionary<DPath, DelegationCapability> columnRestrictions)
            : base(schema)
        {
            Contracts.AssertValue(columnRestrictions);

            _columnRestrictions = columnRestrictions;
        }

        protected override Dictionary<DPath, DelegationCapability> ColumnRestrictions { get { return _columnRestrictions; } }

        public override DelegationCapability DefaultColumnCapabilities { get { return DelegationCapability.Sort; } }

        public override DelegationCapability TableCapabilities { get { return DefaultColumnCapabilities; } }

        // Returns true if column is marked as AscendingOnly.
        public bool IsColumnAscendingOnly(DPath columnPath)
        {
            Contracts.AssertValid(columnPath);

            DelegationCapability columnRestrictions;
            if (!TryGetColumnRestrictions(columnPath, out columnRestrictions))
                return false;

            return columnRestrictions.HasCapability(DelegationCapability.SortAscendingOnly);
        }
    }
}
