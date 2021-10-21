//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.Public;

namespace Microsoft.PowerFx.Core
{
    public interface IPowerFxScopeFactory
    {
        IPowerFxScope GetOrCreateInstance(string documentUri);
    }
}
