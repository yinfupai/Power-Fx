//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Transport;

namespace Microsoft.PowerFx.Core.App.Components
{
    [TransportType(TransportKind.Enum)]
    public enum ComponentType
    {
        CanvasComponent = 0,
        DataComponent = 1,
        FunctionComponent = 2,
        CommandComponent = 3,
    }
}