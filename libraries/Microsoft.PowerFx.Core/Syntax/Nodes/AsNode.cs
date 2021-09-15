//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AppMagic.Authoring.Texl.SourceInformation;

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal sealed class AsNode : TexlNode
    {
        public readonly TexlNode Left;
        public readonly Identifier Right;

        public AsNode(ref int idNext, Token primaryToken, SourceList sourceList, TexlNode left, Identifier right)
            : base(ref idNext, primaryToken, sourceList)
        {
            Contracts.AssertValue(left);
            Contracts.AssertValue(right);
            Left = left;
            Left.Parent = this;
            Right = right;
            _depth = 1 + left.Depth;

            MinChildID = left.MinChildID;
        }

        public override TexlNode Clone(ref int idNext, Span ts)
        {
            var left = Left.Clone(ref idNext, ts);
            var newNodes = new Dictionary<TexlNode, TexlNode>
            {
                { Left, left },
            };

            return new AsNode(
                ref idNext,
                Token.Clone(ts),
                SourceList.Clone(ts, newNodes),
                left,
                Right);
        }

        public override void Accept(TexlVisitor visitor)
        {
            Contracts.AssertValue(visitor);
            if (visitor.PreVisit(this))
            {
                Left.Accept(visitor);
                visitor.PostVisit(this);
            }
        }

        public override Result Accept<Result, Context>(TexlFunctionalVisitor<Result, Context> visitor, Context context)
        {
            return visitor.Visit(this, context);
        }

        public override NodeKind Kind { get { return NodeKind.As; } }

        public override AsNode AsAsNode()
        {
            return this;
        }

        public override Span GetCompleteSpan()
        {
            return new Span(Left.GetCompleteSpan().Min, Right.Token.Span.Lim);
        }
    }
}