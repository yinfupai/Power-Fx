//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.Binding;
using Microsoft.PowerFx.Core.Syntax.Nodes;

namespace Microsoft.PowerFx.Core.Functions.FunctionArgValidators
{
    internal interface IArgValidator<T>
    {
        bool TryGetValidValue(TexlNode argNode, TexlBinding binding, out T value);
    }
}
