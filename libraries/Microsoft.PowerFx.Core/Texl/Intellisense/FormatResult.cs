// <copyright file="FormatResult.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using Microsoft.AppMagic.Transport;

namespace Microsoft.AppMagic.Authoring
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
