// ------------------------------------------------------------------------------
//  <copyright file="INamedLanguageSettings.cs" company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
//  </copyright>
// ------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring
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