//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring.Texl;
using Microsoft.PowerFx.Core;
using Microsoft.PowerFx.Core.IR;

namespace Microsoft.PowerFx.Functions
{
    internal static class CommonErrors
    {
        public static ErrorValue RuntimeTypeMismatch(IRContext irContext)
        {
            return new ErrorValue(irContext, new ExpressionError()
            {
                Message = "Runtime type mismatch",
                Span = irContext.SourceContext,
                Kind = ErrorKind.Validation
            });
        }

        public static ErrorValue ArgumentOutOfRange(IRContext irContext)
        {
            return new ErrorValue(irContext, new ExpressionError()
            {
                Message = "Argument out of range",
                Span = irContext.SourceContext,
                Kind = ErrorKind.Numeric
            });
        }

        public static FormulaValue DivByZeroError(IRContext irContext)
        {
            return new ErrorValue(irContext, new ExpressionError()
            {
                Message = "Divide by zero",
                Span = irContext.SourceContext,
                Kind = ErrorKind.Div0
            });
        }

        public static FormulaValue UnreachableCodeError(IRContext irContext)
        {
            return new ErrorValue(irContext, new ExpressionError()
            {
                Message = "Unknown error",
                Span = irContext.SourceContext,
                Kind = ErrorKind.Validation
            });
        }
    }
}
