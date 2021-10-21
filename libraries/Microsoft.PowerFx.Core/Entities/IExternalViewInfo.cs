//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Microsoft.PowerFx.Core.Entities
{
    public interface IExternalViewInfo : IDisplayMapped<Guid>
    {
        string Name { get; }
        string RelatedEntityName { get; }
    }
}