//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.PowerFx.Core.Delegation
{
    internal interface IDelegationMetadata
    {
        DType Schema { get; }
        DelegationCapability TableAttributes { get; }
        DelegationCapability TableCapabilities { get; }
        SortOpMetadata SortDelegationMetadata { get; }
        FilterOpMetadata FilterDelegationMetadata { get; }
        GroupOpMetadata GroupDelegationMetadata { get; }

        Dictionary<DPath, DPath> ODataPathReplacementMap { get; }
    }
}
