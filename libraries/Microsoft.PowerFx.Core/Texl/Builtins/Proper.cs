//------------------------------------------------------------------------------
// <copyright file="Proper.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Proper(arg:s)
    internal sealed class ProperFunction : StringOneArgFunction
    {
        public override bool HasPreciseErrors => true;
        public ProperFunction()
            : base("Proper", TexlStrings.AboutProper, FunctionCategories.Text)
        { }

        public override bool IsRowScopedServerDelegatable(CallNode callNode, TexlBinding binding, OperationCapabilityMetadata metadata)
        {
            return false;
        }
    }

    // Proper(arg:*[s])
    internal sealed class ProperTFunction : StringOneArgTableFunction
    {
        public override bool HasPreciseErrors => true;
        public ProperTFunction()
            : base("Proper", TexlStrings.AboutProperT, FunctionCategories.Table)
        { }
    }
}
