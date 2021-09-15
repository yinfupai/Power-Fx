//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AppMagic;
using Microsoft.AppMagic.Authoring;
using Microsoft.AppMagic.Authoring.Texl;
using PowerApps.Language.Entities;

namespace Microsoft.PowerFx.Core.App
{
    internal interface IQualifiedValuesInfo : IExternalEntity
    {
        bool IsAsyncAccess { get; }
        string Kind { get; }
        DType Schema { get; }
        IReadOnlyDictionary<string, string> Values { get; }
        DType ValueType { get; }
    }
}