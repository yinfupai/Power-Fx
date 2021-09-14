//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring.DataToControls;

namespace Microsoft.PowerFx.Core.App
{
    internal interface IExternalColumnMetadata
    {
        DataFormat? DataFormat { get; }
    }
}