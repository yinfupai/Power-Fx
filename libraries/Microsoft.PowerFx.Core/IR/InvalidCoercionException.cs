using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.PowerFx.Core.IR
{
    public class InvalidCoercionException : InvalidOperationException
    {
        public InvalidCoercionException(string message) : base(message) { }
    }
}
