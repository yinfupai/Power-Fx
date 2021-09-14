//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring.Texl.SourceInformation;

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal sealed class BlankNode : TexlNode
    {
        public override NodeKind Kind { get { return NodeKind.Blank; } }

        public BlankNode(ref int idNext, Token primaryToken)
            : base(ref idNext, primaryToken, new SourceList(primaryToken))
        {
        }

        public override TexlNode Clone(ref int idNext, Span ts)
        {
            return new BlankNode(ref idNext, Token.Clone(ts));
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

        public override BlankNode AsBlank()
        {
            return this;
        }
    }
}