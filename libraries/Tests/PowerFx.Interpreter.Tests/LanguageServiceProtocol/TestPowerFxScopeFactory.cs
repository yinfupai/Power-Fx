//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core;

namespace Microsoft.PowerFx.Tests.LanguageServiceProtocol
{
    public class TestPowerFxScopeFactory : IPowerFxScopeFactory
    {
        public delegate IPowerFxScope GetOrCreateInstanceDelegate(string documentUri);

        private GetOrCreateInstanceDelegate _getOrCreateInstance;

        public TestPowerFxScopeFactory(GetOrCreateInstanceDelegate getOrCreateInstance)
        {
            _getOrCreateInstance = getOrCreateInstance;
        }

        public IPowerFxScope GetOrCreateInstance(string documentUri)
        {
            return _getOrCreateInstance(documentUri);
        }
    }
}
