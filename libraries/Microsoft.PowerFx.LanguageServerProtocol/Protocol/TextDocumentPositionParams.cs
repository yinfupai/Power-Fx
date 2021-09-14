// ------------------------------------------------------------------------------
//  <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
//  </copyright>
// ------------------------------------------------------------------------------

namespace Microsoft.PowerFx.LanguageServerProtocol.Protocol
{
    public class TextDocumentPositionParams
    {
        public TextDocumentPositionParams()
        {
            TextDocument = new TextDocumentIdentifier();
            Position = new Position();
        }

        /// <summary>
        /// The text document.
        /// </summary>
        public TextDocumentIdentifier TextDocument { get; set; }

        /// <summary>
        /// The position inside the text document.
        /// </summary>
        public Position Position { get; set; }
    }
}
