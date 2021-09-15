// ------------------------------------------------------------------------------
//  <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
//  </copyright>
// ------------------------------------------------------------------------------

namespace Microsoft.PowerFx.LanguageServerProtocol.Protocol
{
    /// <summary>
    /// Custom LSP protocol starts with '$/', not all client supports
    /// </summary>
    public static class CustomProtocolNames
    {
        public const string PublishTokens = "$/publishTokens";
        public const string InitialFixup = "$/initialFixup";
    }
}
