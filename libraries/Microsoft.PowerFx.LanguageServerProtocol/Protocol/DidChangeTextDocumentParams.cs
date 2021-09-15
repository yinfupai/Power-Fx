// ------------------------------------------------------------------------------
//  <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
//  </copyright>
// ------------------------------------------------------------------------------

namespace Microsoft.PowerFx.LanguageServerProtocol.Protocol
{
    public class DidChangeTextDocumentParams
    {
        /// <summary>
        /// The document that did change.The version number points
        /// to the version after all provided content changes have
        /// been applied.
        /// </summary>
        public VersionedTextDocumentIdentifier TextDocument { get; set; } = new VersionedTextDocumentIdentifier();

        /// <summary>
        /// The actual content changes.
        /// </summary>
        public TextDocumentContentChangeEvent[] ContentChanges { get; set; } = new TextDocumentContentChangeEvent[] { };
    }
}
