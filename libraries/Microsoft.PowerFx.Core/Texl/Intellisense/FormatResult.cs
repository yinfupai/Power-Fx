// <copyright file="FormatResult.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.PowerFx.Core.Texl.Intellisense
{
    public sealed class FormatResult
    {
        public string resultText { get; }

        public FormatResult(string text)
        {
            resultText = text;
        }
    }

}
