//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Transport;

namespace Microsoft.PowerFx.Core.Errors
{
    [TransportType(TransportKind.Enum)]
    public enum DocumentErrorKind
    {
        AXL,
        ClipBoard,
        Intellisense,
        Importer,
        Persistence,
        Publish,
        Rule,
        Entity,
        Migration,
        UnsupportedDocumentTypeOnImport,
        DeletedComponent
    }
}