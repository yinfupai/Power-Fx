//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;
using Microsoft.AppMagic.Authoring.Texl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.PowerFx.Core.Delegation.DelegationStrategies
{
    internal interface IOpDelegationStrategy
    {
        bool IsOpSupportedByColumn(OperationCapabilityMetadata metadata, TexlNode column, DPath columnPath, TexlBinding binder);
        bool IsOpSupportedByTable(OperationCapabilityMetadata metadata, TexlNode node, TexlBinding binder);
        bool IsSupportedOpNode(TexlNode node, OperationCapabilityMetadata metadata, TexlBinding binding);
    }
}
