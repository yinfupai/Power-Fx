//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.IR;

namespace Microsoft.PowerFx
{
    /// <summary>
    /// All values except BlankValue and ErrorValue should inherit from this base class.
    /// BlankValue and ErrorValue inherit directly from FormulaValue. The type parameter
    /// T in DValue is constrained to ValidFormulaValue, meaning that BlankValue
    /// and ErrorValue can never be substituted for T.
    /// </summary>
    public abstract class ValidFormulaValue : FormulaValue
    {
        internal ValidFormulaValue(IRContext irContext) : base(irContext)
        {

        }
    }
}
