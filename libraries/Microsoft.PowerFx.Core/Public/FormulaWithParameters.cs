//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.PowerFx
{
    /// <summary>
    /// Encapsulate a formula and the parameters it has available. 
    /// </summary>
    public class FormulaWithParameters
    {
        readonly internal string _expression; // Formula
        readonly internal FormulaType _schema; // context formula can access.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression">The text version of the expression</param>
        /// <param name="parameterTypes">The static type of parameters (context) available to this formula. 
        /// If omited, this formula doesn't have any additional parameters. 
        /// </param>
        public FormulaWithParameters(string expression, FormulaType parameterTypes = null)
        {
            _expression = expression;
            _schema = parameterTypes ?? new RecordType();
        }
    }
}
