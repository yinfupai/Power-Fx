//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Linq;
using Microsoft.AppMagic.Authoring.Texl.SourceInformation;

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal abstract class NameNode : TexlNode
    {
        protected NameNode(ref int idNext, Token primaryToken, SourceList sourceList)
            : base(ref idNext, primaryToken, sourceList)
        {
        }

        public override Span GetCompleteSpan()
        {
            if (SourceList.Tokens.Count() == 0)
                return base.GetCompleteSpan();

            var start = SourceList.Tokens.First().Span.Min;
            var end = SourceList.Tokens.Last().Span.Lim;
            return new Span(start, end);
        }
    }
}