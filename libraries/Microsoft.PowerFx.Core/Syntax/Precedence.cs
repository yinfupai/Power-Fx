//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.PowerFx.Core.Syntax
{
    // Operator precedence.
    internal enum Precedence : byte
    {
        None,
        SingleExpr,
        Or,
        And,
        In,
        Compare,
        Concat,
        Add,
        Mul,
        Error,
        As,
        PrefixUnary,
        Power,
        PostfixUnary,
        Primary,
        Atomic,
    }
}
