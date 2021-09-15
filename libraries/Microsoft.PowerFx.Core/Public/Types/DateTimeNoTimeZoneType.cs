//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;
using System.Diagnostics;

namespace Microsoft.PowerFx
{
    [DebuggerDisplay("{_type}:tzi")]
    public class DateTimeNoTimeZoneType : FormulaType
    {
        internal DateTimeNoTimeZoneType() : base(DType.DateTimeNoTimeZone)
        {
        }

        public override void Visit(ITypeVistor vistor)
        {
            vistor.Visit(this);
        }
    }
}
