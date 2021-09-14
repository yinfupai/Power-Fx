//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Transport;

namespace Microsoft.AppMagic.Authoring
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