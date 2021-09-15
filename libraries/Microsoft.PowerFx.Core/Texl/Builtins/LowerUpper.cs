//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Lower(arg:s)
    // Upper(arg:s)
    // Corresponding DAX functions: Lower, Upper
    internal sealed class LowerUpperFunction : StringOneArgFunction
    {
        private bool _isLower;
        public LowerUpperFunction(bool isLower)
            : base(isLower ? "Lower" : "Upper", isLower ? TexlStrings.AboutLower : TexlStrings.AboutUpper, FunctionCategories.Text)
        {
            _isLower = isLower;
        }

        public override DelegationCapability FunctionDelegationCapability { get { return _isLower ? DelegationCapability.Lower : DelegationCapability.Upper; } }

        public override bool IsRowScopedServerDelegatable(CallNode callNode, TexlBinding binding, OperationCapabilityMetadata metadata)
        {
            Contracts.AssertValue(callNode);
            Contracts.AssertValue(binding);
            Contracts.AssertValue(metadata);

            if (!OldFeatureGates.StringOperationsSupport)
                return false;

            return base.IsRowScopedServerDelegatable(callNode, binding, metadata);
        }
    }

    // Lower(arg:*[s])
    // Upper(arg:*[s])
    internal sealed class LowerUpperTFunction : StringOneArgTableFunction
    {
        public LowerUpperTFunction(bool isLower)
            : base(isLower ? "Lower" : "Upper", isLower ? TexlStrings.AboutLowerT : TexlStrings.AboutUpperT, FunctionCategories.Table)
        { }
    }
}
