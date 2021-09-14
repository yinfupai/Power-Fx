//------------------------------------------------------------------------------
// <copyright file="IRuleError.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System.Collections.Generic;
using Microsoft.AppMagic.Authoring.Texl;
using Microsoft.AppMagic.Transport;

namespace Microsoft.AppMagic.Authoring
{
    [TransportType(TransportKind.ByValue)]
    public interface IRuleError : IDocumentError
    {
    }
}
