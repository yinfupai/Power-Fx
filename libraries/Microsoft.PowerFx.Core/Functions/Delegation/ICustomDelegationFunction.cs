//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.PowerFx.Core.Functions.Delegation
{
    interface ICustomDelegationFunction
    {
        // This exists to push a feature gate dependence out of PowerFx.
        // Once AllowUserDelegation is cleaned up, this can be removed
        bool IsUserCallNodeDelegable();
    }
}
