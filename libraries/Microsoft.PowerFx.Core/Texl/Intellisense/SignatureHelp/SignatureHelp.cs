//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.PowerFx.Core.Texl.Intellisense.SignatureHelp
{
    public class SignatureHelp
    {
        public SignatureInformation[] Signatures { get; set; }
        public uint ActiveSignature { get; set; }
        public uint ActiveParameter { get; set; }
    }
}