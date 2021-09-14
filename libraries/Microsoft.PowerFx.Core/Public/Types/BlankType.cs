//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;

namespace Microsoft.PowerFx
{
    /// <summary>
    /// Represents a Blank (similar to Null) value. BlankType is compatible with other types. 
    /// </summary>
    public class BlankType : FormulaType
    {
        internal BlankType() : base(new DType(DKind.ObjNull))
        {
        }
        public override void Visit(ITypeVistor vistor)
        {
            vistor.Visit(this);
        }
    }
}
