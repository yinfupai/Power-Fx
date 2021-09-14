//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring.Texl.SourceInformation;

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal sealed class ReplaceableNode : TexlNode
    {
        public ReplaceableNode(ref int idNext, ReplaceableToken tok)
            : base(ref idNext, tok, new SourceList(tok))
        {
            Value = tok.Value;
            Contracts.AssertValue(Value);
        }

        public string Value { get; }

        public override NodeKind Kind { get; } = NodeKind.Replaceable;

        public override TexlNode Clone(ref int idNext, Span ts)
        {
            return new ReplaceableNode(ref idNext, Token.Clone(ts).As<ReplaceableToken>());
        }

        public override ReplaceableNode AsReplaceable()
        {
            return this;
        }

        public override void Accept(TexlVisitor visitor)
        {
            Contracts.AssertValue(visitor);
            visitor.Visit(this);
        }

        public override Result Accept<Result, Context>(TexlFunctionalVisitor<Result, Context> visitor, Context context)
        {
            return visitor.Visit(this, context);
        }
    }
}