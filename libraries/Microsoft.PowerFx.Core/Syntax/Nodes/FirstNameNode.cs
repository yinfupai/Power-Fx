//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring.Texl.SourceInformation;

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal sealed class FirstNameNode : NameNode
    {
        public readonly Identifier Ident;

        public bool IsLhs { get { return Parent != null && Parent.AsDottedName() != null; } }

        public FirstNameNode(ref int idNext, Token tok, SourceList sourceList, Identifier ident)
            : base(ref idNext, tok, sourceList)
        {
            Contracts.AssertValue(ident);
            Contracts.Assert(ident.Namespace.IsRoot);

            Ident = ident;
        }
        public FirstNameNode(ref int idNext, Token tok, Identifier ident)
            : this(ref idNext, tok, new SourceList(tok), ident)
        {
        }

        public override TexlNode Clone(ref int idNext, Span ts)
        {
            return new FirstNameNode(ref idNext, Token.Clone(ts), Ident.Clone(ts));
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

        public override NodeKind Kind { get { return NodeKind.FirstName; } }

        public override FirstNameNode CastFirstName()
        {
            return this;
        }

        public override FirstNameNode AsFirstName()
        {
            return this;
        }
    }
}