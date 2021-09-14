//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.PowerFx.Core
{
    public interface IPowerFxScopeFactory
    {
        IPowerFxScope GetOrCreateInstance(string documentUri);
    }
}
