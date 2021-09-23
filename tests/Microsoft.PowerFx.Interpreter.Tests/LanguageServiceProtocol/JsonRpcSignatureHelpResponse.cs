//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.LanguageServerProtocol.Protocol;

namespace Microsoft.PowerFx.Tests.LanguageServiceProtocol
{
    public class JsonRpcSignatureHelpResponse
    {
        public string Jsonrpc { get; set; } = string.Empty;

        public string Id { get; set; } = string.Empty;

        public SignatureHelp Result { get; set; } = new SignatureHelp();
    }
}
