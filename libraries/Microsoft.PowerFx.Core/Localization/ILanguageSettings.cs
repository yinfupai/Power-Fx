//------------------------------------------------------------------------------
// <copyright file="ILanguageSettings.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System.Collections.Generic;

namespace Microsoft.AppMagic.Authoring
{
    internal interface ILanguageSettings : INamedLanguageSettings
    {
        // Empty maps are equivalent to identity maps.
        // This is relevant for Beta and Beta2 documents (which are always invariant).
        Dictionary<string, string> LocToInvariantFunctionMap { get; }
        Dictionary<string, string> LocToInvariantPunctuatorMap { get; }
        // Reverse maps
        Dictionary<string, string> InvariantToLocFunctionMap { get; }
        Dictionary<string, string> InvariantToLocPunctuatorMap { get; }
        ILanguageSettings GetIdentitySettingsForInvariantLanguage();
    }
}
