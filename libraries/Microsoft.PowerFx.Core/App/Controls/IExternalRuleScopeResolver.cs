//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;
using Microsoft.AppMagic.Authoring.Texl;

namespace Microsoft.PowerFx.Core.App
{
    internal interface IExternalRuleScopeResolver
    {
        bool Lookup(DName identName, out ScopedNameLookupInfo scopedNameLookupInfo);
    }
}