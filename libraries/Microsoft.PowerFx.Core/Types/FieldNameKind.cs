//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.PowerFx.Core.Types
{
    /// <summary>
    /// You can refer to a field name both by a "logical" name and a "display" name. Sometimes
    /// it's important to distinguish which kind of name you are requesting, to avoid ambiguous
    /// strings.
    /// </summary>
    public enum FieldNameKind
    {
        Display,
        Logical
    }
}