//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Blank()
    internal sealed class BlankFunction : BuiltinFunction
    {
        public override bool IsSelfContained => true;

        public BlankFunction() : base("Blank",
                  TexlStrings.AboutBlank,
                  FunctionCategories.Text,
                  DType.ObjNull, // return type
                  0,            // no lambdas
                  0,            // min arity of 0
                  0)            // max arity of 0
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new TexlStrings.StringGetter[] { };
        }
    }
}
