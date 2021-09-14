//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.AppMagic.Authoring
{
    class ODataOpMetadata : OperationCapabilityMetadata
    {
        private Dictionary<DPath, DPath> _oDataReplacement;

        public ODataOpMetadata(DType schema, Dictionary<DPath, DPath> oDataReplacement)
            : base(schema)
        {
            Contracts.AssertValue(oDataReplacement);

            _oDataReplacement = oDataReplacement;
        }

        public override Dictionary<DPath, DPath> QueryPathReplacement {  get { return _oDataReplacement; } }

        public override DelegationCapability DefaultColumnCapabilities { get { return DelegationCapability.None; } }

        public override DelegationCapability TableCapabilities { get { return DefaultColumnCapabilities; } }

    }
}
