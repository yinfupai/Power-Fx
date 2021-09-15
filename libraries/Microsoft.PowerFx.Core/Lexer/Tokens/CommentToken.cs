//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal class CommentToken : Token
    {
        private readonly string _val;
        public bool IsOpenBlock;

        public CommentToken(string val, Span span)
            : base(TokKind.Comment, span)
        {
            Contracts.AssertValue(val);
            _val = val;
        }

        public string Value { get { return _val; } }

        public override Token Clone(Span ts)
        {
            return new CommentToken(Value, ts);
        }
    }
}