//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal sealed class DefaultBinaryOpDelegationStrategy : BinaryOpDelegationStrategy
    {
        public DefaultBinaryOpDelegationStrategy(BinaryOp op, TexlFunction function) : base(op, function)
        { }
    }

    internal sealed class DefaultUnaryOpDelegationStrategy : UnaryOpDelegationStrategy
    {
        public DefaultUnaryOpDelegationStrategy(UnaryOp op, TexlFunction function) : base(op, function)
        { }
    }
}
