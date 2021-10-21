//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.Syntax;

namespace Microsoft.PowerFx.Core.Texl.Intellisense
{
    internal partial class Intellisense
    {
        internal sealed class ErrorNodeSuggestionHandler : ErrorNodeSuggestionHandlerBase
        {
            public ErrorNodeSuggestionHandler()
                : base(NodeKind.Error)
            { }
        }
    }
}