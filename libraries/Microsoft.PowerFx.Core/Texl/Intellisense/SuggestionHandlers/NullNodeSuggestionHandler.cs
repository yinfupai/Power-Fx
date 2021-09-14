//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal partial class Intellisense
    {
        internal sealed class NullNodeSuggestionHandler : ISuggestionHandler
        {
            public NullNodeSuggestionHandler() { }

            /// <summary>
            /// Adds suggestions as appropriate to the internal Suggestions and SubstringSuggestions lists of intellisenseData.
            /// Returns true if intellisenseData is handled and no more suggestions are to be found and false otherwise.
            /// </summary>
            public bool Run(IntellisenseData intellisenseData)
            {
                Contracts.AssertValue(intellisenseData);

                if (intellisenseData.CurNode != null)
                    return false;

                return IntellisenseHelper.AddSuggestionsForValuePossibilities(intellisenseData, null);
            }
        }
    }
}