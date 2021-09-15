// ------------------------------------------------------------------------------
//  <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
//  </copyright>
// ------------------------------------------------------------------------------

namespace Microsoft.PowerFx.LanguageServerProtocol.Protocol
{
    public class InitialFixupParams
    {
        /// <summary>
        /// The document that was opened.
        /// </summary>
        public TextDocumentItem TextDocument { get; set; }
    }
}
