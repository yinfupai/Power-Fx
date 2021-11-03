﻿//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.Errors;
using Microsoft.PowerFx.Core.Localization;

namespace Microsoft.PowerFx.Core.Public
{
    public class ExpressionError
    {
        public string Message { get; set; }
        public Span Span { get; set; }
        public ErrorKind Kind { get; set; }
        public DocumentErrorSeverity Severity { get; set; }

        public override string ToString()
        {
            return $"Error {Span.Min}-{Span.Lim}: {Message}";
        }
    }
}