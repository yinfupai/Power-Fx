//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring.Texl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.PowerFx.Core.Delegation
{
    interface ICustomDelegationFunction
    {
        // This exists to push a feature gate dependence out of PowerFx.
        // Once AllowUserDelegation is cleaned up, this can be removed
        bool IsUserCallNodeDelegable();
    }
}
