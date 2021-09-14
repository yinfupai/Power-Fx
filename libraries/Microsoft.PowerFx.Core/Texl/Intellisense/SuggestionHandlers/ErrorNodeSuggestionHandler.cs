//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring.Texl
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