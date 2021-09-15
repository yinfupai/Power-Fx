//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Common.Telemetry;

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal interface IArgValidator<T>
    {
        bool TryGetValidValue(TexlNode argNode, TexlBinding binding, out T value);
    }
}
