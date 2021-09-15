//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.PowerFx.Core.App;
using Microsoft.PowerFx.Core.Entities.QueryOptions;
using PowerApps.Language.Entities;

namespace Microsoft.AppMagic.Authoring
{
    internal interface IExternalTabularDataSource : IExternalDataSource, IDisplayMapped<string>
    {
        TabularDataQueryOptions QueryOptions { get; }
        IReadOnlyList<string> GetKeyColumns();
        IEnumerable<string> GetKeyColumns(IExpandInfo expandInfo);
        bool CanIncludeSelect(string selectColumnName);
        bool CanIncludeSelect(IExpandInfo expandInfo, string selectColumnName);
        bool CanIncludeExpand(IExpandInfo expandToAdd);
        bool CanIncludeExpand(IExpandInfo parentExpandInfo, IExpandInfo expandToAdd);
    }
}