// ------------------------------------------------------------------------------
//  <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
//  </copyright>
// ------------------------------------------------------------------------------

namespace Microsoft.PowerFx.LanguageServerProtocol.Protocol
{
    public class TextDocumentIdentifier
    {
        public TextDocumentIdentifier()
        {
            Uri = string.Empty;
        }

        /// <summary>
        /// The text document's URI.
        /// </summary>
        public string Uri { get; set; }
    }
}
