//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;

namespace Microsoft.PowerFx
{
    public class StringType : FormulaType
    {
        public StringType() : base(new DType(DKind.String))
        {
        }

        public override void Visit(ITypeVistor vistor)
        {
            vistor.Visit(this);
        }
    }
}