// ------------------------------------------------------------------------------
//  <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
//  </copyright>
// ------------------------------------------------------------------------------

using Microsoft.AppMagic.Transport;

namespace Microsoft.PowerFx.Core.Functions.TransportSchemas
{
    [TransportType(TransportKind.ByValue)]
    public sealed class FunctionInfo
    {
        public string Label;
        public string Detail;
        public string Documentation;
        public FunctionSignature[] Signatures;
    }
}