//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.PowerFx.Core.Texl.Intellisense.SignatureHelp
{
    public class SignatureInformation
    {
        public string Label { get; set; }
        public string Documentation { get; set; }
        public ParameterInformation[] Parameters { get; set; }
    }
}