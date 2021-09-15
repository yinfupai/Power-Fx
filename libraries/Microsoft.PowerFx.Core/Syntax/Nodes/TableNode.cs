//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Microsoft.AppMagic.Authoring.Texl.SourceInformation;

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal sealed class TableNode : VariadicBase
    {
        public readonly Token[] Commas;
        // BracketClose can be null.
        public readonly Token BracketClose;

        // Assumes ownership of all of the array args.
        public TableNode(ref int idNext, Token primaryToken, SourceList sourceList, TexlNode[] exprs, Token[] commas, Token bracketCloseToken)
            : base(ref idNext, primaryToken, sourceList, exprs)
        {
            Contracts.AssertValue(exprs);
            Contracts.AssertValueOrNull(commas);
            Contracts.AssertValueOrNull(bracketCloseToken);

            Commas = commas;
            BracketClose = bracketCloseToken;
        }

        public override TexlNode Clone(ref int idNext, Span ts)
        {
            var children = CloneChildren(ref idNext, ts);
            var newNodes = new Dictionary<TexlNode, TexlNode>();
            for (int i = 0; i < Children.Length; ++i)
                newNodes.Add(Children[i], children[i]);

            return new TableNode(ref idNext, Token.Clone(ts), SourceList.Clone(ts, newNodes), children, Clone(Commas, ts), BracketClose.Clone(ts));
        }

        public override void Accept(TexlVisitor visitor)
        {
            Contracts.AssertValue(visitor);
            if (visitor.PreVisit(this))
            {
                AcceptChildren(visitor);
                visitor.PostVisit(this);
            }
        }

        public override Result Accept<Result, Context>(TexlFunctionalVisitor<Result, Context> visitor, Context context)
        {
            return visitor.Visit(this, context);
        }

        public override NodeKind Kind { get { return NodeKind.Table; } }

        public override TableNode AsTable()
        {
            return this;
        }

        public override Span GetCompleteSpan()
        {
            int lim;
            if (BracketClose != null)
                lim = BracketClose.Span.Lim;
            else if (this.Children.Count() == 0)
                lim = this.Token.VerifyValue().Span.Lim;
            else
                lim = this.Children.VerifyValue().Last().VerifyValue().GetCompleteSpan().Lim;

            return new Span(this.Token.VerifyValue().Span.Min, lim);
        }

        public override Span GetTextSpan()
        {
            int lim = BracketClose == null ? Token.VerifyValue().Span.Lim : BracketClose.Span.Lim;
            return new Span(Token.VerifyValue().Span.Min, lim);
        }
    }
}