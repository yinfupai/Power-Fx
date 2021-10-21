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
        internal sealed class BlankNodeSuggestionHandler : ErrorNodeSuggestionHandlerBase
        {
            public BlankNodeSuggestionHandler()
                : base(NodeKind.Blank)
            { }
        }
    }
}