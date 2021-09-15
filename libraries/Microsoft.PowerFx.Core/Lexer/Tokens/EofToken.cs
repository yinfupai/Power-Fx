//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal class EofToken : Token
    {
        public EofToken(Span span)
            : base(TokKind.Eof, span)
        { }

        /// <summary>
        /// Copy Ctor for EofToken used by Clone
        /// </summary>
        /// <param name="tok">The token to be copied</param>
        /// <param name="newSpan">The new span</param>
        private EofToken(EofToken tok, Span newSpan)
            : this(newSpan)
        {
        }

        public override Token Clone(Span ts)
        {
            return new EofToken(this, ts);
        }
    }
}