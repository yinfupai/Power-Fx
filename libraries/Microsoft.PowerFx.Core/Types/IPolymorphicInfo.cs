//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.AppMagic.Authoring
{
    /// <summary>
    /// Information about polymorphic entity type, generates/stores ExpandInfo for each of its target casts.
    /// </summary>
    internal interface IPolymorphicInfo
    {
        string[] TargetTables { get; }
        string[] TargetFields { get; }
        bool IsTable { get; }
        string Name { get; }
        IExternalDataSource ParentDataSource { get; }
        public IEnumerable<IExpandInfo> Expands { get; }
        public IExpandInfo TryGetExpandInfo(string targetTable);
        IPolymorphicInfo Clone();
        string ToDebugString();
    }
}