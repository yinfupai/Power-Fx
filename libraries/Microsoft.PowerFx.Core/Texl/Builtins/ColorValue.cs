//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // ColorValue(colorstring:s)
    // Returns the color value for the given color string
    internal sealed class ColorValueFunction : BuiltinFunction
    {
        public override bool RequiresErrorContext => true;
        public override bool IsSelfContained => true;

        public ColorValueFunction()
            : base("ColorValue", TexlStrings.AboutColorValue, FunctionCategories.Color, DType.Color, 0, 1, 1, DType.String)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new[] { TexlStrings.ColorValueArg1 };
        }
    }
}