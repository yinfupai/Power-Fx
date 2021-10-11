//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;


// These have separate defintions as the one with a string is a pure function
namespace Microsoft.AppMagic.Authoring.Texl
{
    // GUID()
    internal sealed class GUIDNoArgFunction : BuiltinFunction
    {
        // Multiple invocations may produce different return values.
        public override bool IsStateless => false;
        public override bool IsSelfContained => true;
        public override bool SupportsParamCoercion => true;

        public GUIDNoArgFunction()
            : base("GUID", TexlStrings.AboutGUID, FunctionCategories.Text, DType.Guid, 0, 0, 0)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new TexlStrings.StringGetter[0];
        }
    }

    // GUID(GuidString:s)
    internal sealed class GUIDPureFunction : BuiltinFunction
    {
        public override bool RequiresErrorContext => true;
        public override bool IsSelfContained => true;
        public override bool SupportsParamCoercion => false;

        public GUIDPureFunction()
            : base("GUID", TexlStrings.AboutGUID, FunctionCategories.Text, DType.Guid, 0, 1, 1, DType.String)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new[] { TexlStrings.GUIDArg };
        }
    }

}
