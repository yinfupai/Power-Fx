//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core;
using Microsoft.PowerFx.LanguageServerProtocol;

namespace Microsoft.PowerFx.Tests.LanguageServiceProtocol
{
    public class TestLanguageServer : LanguageServer
    {
        public TestLanguageServer(SendToClient sendToClient, IPowerFxScopeFactory scopeFactory) : base(sendToClient, scopeFactory)
        {
        }

        public int TestGetCharPosition(string expression, int position) => GetCharPosition(expression, position);

        public int TestGetPosition(string expression, int line, int character) => GetPosition(expression, line, character);

    }
}
