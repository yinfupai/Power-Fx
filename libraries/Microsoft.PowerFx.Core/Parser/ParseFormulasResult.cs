//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.Utils;
using System.Collections.Generic;

namespace Microsoft.PowerFx.Core.Parser
{
    internal class ParseFormulasResult
    {
        internal Dictionary<DName, ParseResult> NamedFormulas { get; }
        public ParseFormulasResult(Dictionary<DName, ParseResult> namedFormulas)
        {
            Contracts.AssertValue(namedFormulas);
            NamedFormulas = namedFormulas;
        }
    }
}
