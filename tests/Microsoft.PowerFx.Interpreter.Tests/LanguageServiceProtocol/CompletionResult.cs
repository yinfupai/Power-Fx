//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.LanguageServerProtocol.Protocol;

namespace Microsoft.PowerFx.Tests.LanguageServiceProtocol
{
    public class CompletionResult
    {
        public bool IsIncomplete { get; set; }

        public CompletionItem[] Items { get; set; } = new CompletionItem[] { };
    }
}
