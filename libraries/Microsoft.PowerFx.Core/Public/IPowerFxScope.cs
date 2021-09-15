//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring.Texl;

namespace Microsoft.PowerFx.Core
{
    /// <summary>
    /// Provide intellisense (Design-time) support. 
    /// </summary>
    public interface IPowerFxScope
    {
        /// <summary>
        /// Check for errors in the given expression. 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        CheckResult Check(string expression);

        /// <summary>
        /// Provide intellisense for expression
        /// </summary>
        IIntellisenseResult Suggest(string expression, int cursorPosition);
    }
}
