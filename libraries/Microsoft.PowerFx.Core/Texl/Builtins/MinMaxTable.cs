//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Min(source:*, projection:n)
    // Max(source:*, projection:n)
    // Corresponding DAX functions: Min, MinA, MinX, Max, MaxA, MaxX
    internal sealed class MinMaxTableFunction : StatisticalTableFunction
    {
        private readonly DelegationCapability _delegationCapability;

        public override bool RequiresErrorContext { get { return true; } }

        public override DelegationCapability FunctionDelegationCapability { get { return _delegationCapability; } }

        public MinMaxTableFunction(bool isMin)
            : base(isMin ? "Min" : "Max", isMin ? TexlStrings.AboutMinT : TexlStrings.AboutMaxT, FunctionCategories.Table)
        {
            _delegationCapability = isMin ? DelegationCapability.Min : DelegationCapability.Max;
        }
    }
}