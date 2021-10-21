//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.Binding;
using Microsoft.PowerFx.Core.Syntax.Nodes;
using Microsoft.PowerFx.Core.Utils;

namespace Microsoft.PowerFx.Core.Functions.Delegation.DelegationStrategies
{
    internal interface IOpDelegationStrategy
    {
        bool IsOpSupportedByColumn(OperationCapabilityMetadata metadata, TexlNode column, DPath columnPath, TexlBinding binder);
        bool IsOpSupportedByTable(OperationCapabilityMetadata metadata, TexlNode node, TexlBinding binder);
        bool IsSupportedOpNode(TexlNode node, OperationCapabilityMetadata metadata, TexlBinding binding);
    }
}
