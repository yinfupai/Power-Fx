//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.App;

namespace Microsoft.PowerFx.Core.Entities
{
    internal interface IExternalColumnMetadata
    {
        DataFormat? DataFormat { get; }
    }
}