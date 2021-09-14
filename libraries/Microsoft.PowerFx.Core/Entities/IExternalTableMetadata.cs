//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;
using Microsoft.AppMagic.Authoring.DataToControls;

namespace Microsoft.PowerFx.Core.App
{
    internal interface IExternalTableMetadata
    {
        bool TryGetColumn(string nameRhs, out ColumnMetadata columnMetadata);
        string DisplayName { get; }
        string Name { get; }
    }
}