// ------------------------------------------------------------------------------
//  <copyright file="INamedLanguageSettings.cs" company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
//  </copyright>
// ------------------------------------------------------------------------------

namespace Microsoft.PowerFx.Core.Localization
{
    /// <summary>
    ///     A language settings abstraction tied to a specific culture name
    /// </summary>
    public interface INamedLanguageSettings
    {
        string CultureName { get; }
        string UICultureName { get; }
    }
}