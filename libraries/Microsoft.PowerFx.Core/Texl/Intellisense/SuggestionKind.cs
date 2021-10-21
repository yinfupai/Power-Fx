//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.PowerFx.Core.Texl.Intellisense
{
    // The kind of a suggestion.
    public enum SuggestionKind
    {
        Function,
        KeyWord,
        Global,
        Field,
        Alias,
        Enum,
        BinaryOperator,
        Local,
        ServiceFunctionOption,
        Service,
        ScopeVariable,
    }
}
