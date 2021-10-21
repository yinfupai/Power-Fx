//------------------------------------------------------------------------------
// <copyright file="IRuleError.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Transport;

namespace Microsoft.PowerFx.Core.Errors
{
    [TransportType(TransportKind.ByValue)]
    public interface IRuleError : IDocumentError
    {
    }
}
