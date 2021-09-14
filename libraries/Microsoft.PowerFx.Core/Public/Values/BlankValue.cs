//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring.Texl;
using Microsoft.PowerFx.Core.IR;
using System;
using System.Diagnostics;
using System.Text.Json;

namespace Microsoft.PowerFx
{
    [DebuggerDisplay("Blank() ({Type})")]
    public class BlankValue : FormulaValue
    {
        internal BlankValue(IRContext irContext): base(irContext)
        {
        }

        public override object ToObject()
        {
            return null;
        }

        public override string ToString()
        {
            return $"Blank()";
        }

        public override void Visit(IValueVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}