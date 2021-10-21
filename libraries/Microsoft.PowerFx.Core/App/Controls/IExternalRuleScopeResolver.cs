//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.Binding.BindInfo;
using Microsoft.PowerFx.Core.Utils;

namespace Microsoft.PowerFx.Core.App.Controls
{
    internal interface IExternalRuleScopeResolver
    {
        bool Lookup(DName identName, out ScopedNameLookupInfo scopedNameLookupInfo);
    }
}