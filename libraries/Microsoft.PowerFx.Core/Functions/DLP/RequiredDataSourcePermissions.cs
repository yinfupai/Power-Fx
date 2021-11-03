﻿//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using Microsoft.AppMagic.Transport;

namespace Microsoft.PowerFx.Core.Functions.DLP
{
    [Flags]
    [TransportType(TransportKind.Enum)]
    public enum RequiredDataSourcePermissions
    {
        None = 0x0,
        Create = 0x1,
        Read = 0x2,
        Update = 0x4,
        Delete = 0x8
    }
}