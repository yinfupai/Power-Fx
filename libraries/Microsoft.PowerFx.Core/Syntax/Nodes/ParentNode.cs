//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring.Texl.SourceInformation;

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal sealed class ParentNode : NameNode
    {
        public ParentNode(ref int idNext, Token tok)
            : base(ref idNext, tok, new SourceList(tok))
        {
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

        public override TexlNode Clone(ref int idNext, Span ts)
        {
            return new ParentNode(ref idNext, Token.Clone(ts));
        }

        public override NodeKind Kind { get { return NodeKind.Parent; } }

        public override ParentNode AsParent()
        {
            return this;
        }
    }
}