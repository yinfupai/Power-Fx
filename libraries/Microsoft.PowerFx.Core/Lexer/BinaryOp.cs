//------------------------------------------------------------------------------
// <copyright file="Operators.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.PowerFx.Core.Lexer
{
    internal enum BinaryOp
    {
        Or,
        And,
        Concat,
        Add,
        Mul,
        Div,
        Power,
        Equal,
        NotEqual,
        Less,
        LessEqual,
        Greater,
        GreaterEqual,
        In,
        Exactin,
        Error
    }
}
