//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;

namespace Microsoft.PowerFx
{
    public class NumberType : FormulaType
    {
        internal NumberType() : base(new DType(DKind.Number))
        {
        }
        public override void Visit(ITypeVistor vistor)
        {
            vistor.Visit(this);
        }
    }
}