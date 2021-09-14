//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;
using System;

namespace Microsoft.PowerFx
{
    public class DateTimeType : FormulaType
    {
        internal DateTimeType() : base(DType.DateTime)
        {
        }

        public override void Visit(ITypeVistor vistor)
        {
            vistor.Visit(this);
        }
    }

}
