// ------------------------------------------------------------------------------
//  <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
//  </copyright>
// ------------------------------------------------------------------------------

using Microsoft.PowerFx.Core;
using System.Collections.Generic;

namespace Microsoft.PowerFx.LanguageServerProtocol.Protocol
{
    public class PublishTokensParams
    {
        /// <summary>
        /// The URI for which token information is reported.
        /// </summary>
        public string Uri { get; set; } = string.Empty;

        /// <summary>
        /// A map of token information items.
        /// </summary>
        public IReadOnlyDictionary<string, TokenResultType> Tokens { get; set; } = new Dictionary<string, TokenResultType>();
    }
}
