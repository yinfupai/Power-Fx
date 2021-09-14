//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal class StrLitToken : Token
    {
        private readonly string _val;

        public StrLitToken(string val, Span span)
            : base(TokKind.StrLit, span)
        {
            Contracts.AssertValue(val);
            _val = val;
        }

        /// <summary>
        /// Copy Ctor for StrLitToken used by Clone
        /// </summary>
        /// <param name="tok">The token to be copied</param>
        /// <param name="newSpan">The new span</param>
        private StrLitToken(StrLitToken tok, Span newSpan)
            : this(tok.Value, newSpan)
        {
        }

        public override string ToString()
        {
            return _val;
        }

        public string Value { get { return _val; } }

        public override Token Clone(Span ts)
        {
            return new StrLitToken(this, ts);
        }

        public override bool Equals(Token that)
        {
            Contracts.AssertValue(that);

            if (!(that is StrLitToken))
                return false;
            return Value == that.As<StrLitToken>().Value && base.Equals(that);
        }
    }
}