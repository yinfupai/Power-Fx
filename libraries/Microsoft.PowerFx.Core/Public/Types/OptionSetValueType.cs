//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;

namespace Microsoft.PowerFx
{
    public class OptionSetValueType : FormulaType
    {
        internal OptionSetValueType() : base(new DType(DKind.OptionSetValue))
        {
        }
        public override void Visit(ITypeVistor vistor)
        {
            vistor.Visit(this);
        }
    }
}
