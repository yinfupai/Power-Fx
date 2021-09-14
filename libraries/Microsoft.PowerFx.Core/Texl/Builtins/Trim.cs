//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Trim(arg:s)
    internal sealed class TrimFunction : StringOneArgFunction
    {
        public TrimFunction()
            : base("Trim", TexlStrings.AboutTrim, FunctionCategories.Text)
        { }
    }

    // Trim(arg:*[s])
    internal sealed class TrimTFunction : StringOneArgTableFunction
    {
        public TrimTFunction()
            : base("Trim", TexlStrings.AboutTrim, FunctionCategories.Table)
        { }
    }
    // TrimEnds(arg:s)
    internal sealed class TrimEndsFunction : StringOneArgFunction
    {
        public TrimEndsFunction()
            : base("TrimEnds", TexlStrings.AboutTrimEnds, FunctionCategories.Text)
        { }

        public override DelegationCapability FunctionDelegationCapability { get { return DelegationCapability.Trim; } }

        public override bool IsRowScopedServerDelegatable(CallNode callNode, TexlBinding binding, OperationCapabilityMetadata metadata)
        {
            Contracts.AssertValue(callNode);
            Contracts.AssertValue(binding);
            Contracts.AssertValue(metadata);

            return base.IsRowScopedServerDelegatable(callNode, binding, metadata);
        }
    }

    // Trim(arg:*[s])
    internal sealed class TrimEndsTFunction : StringOneArgTableFunction
    {
        public TrimEndsTFunction()
            : base("TrimEnds", TexlStrings.AboutTrimEnds, FunctionCategories.Table)
        { }
    }
}
