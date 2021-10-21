using System;

namespace Microsoft.PowerFx.Core.IR
{
    public class InvalidCoercionException : InvalidOperationException
    {
        public InvalidCoercionException(string message) : base(message) { }
    }
}
