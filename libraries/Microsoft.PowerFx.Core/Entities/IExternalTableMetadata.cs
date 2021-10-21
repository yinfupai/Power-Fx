//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.PowerFx.Core.Entities
{
    internal interface IExternalTableMetadata
    {
        bool TryGetColumn(string nameRhs, out ColumnMetadata columnMetadata);
        string DisplayName { get; }
        string Name { get; }
    }
}