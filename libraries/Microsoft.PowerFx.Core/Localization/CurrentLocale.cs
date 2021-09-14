//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Globalization;

namespace Microsoft.AppMagic.Common
{
    public sealed class CurrentLocaleInfo
    {
        public static string CurrentLocaleName { get; set; } = CultureInfo.CurrentCulture.Name;

        public static string CurrentUILanguageName { get; set; } = CultureInfo.CurrentCulture.Name;

        public static int CurrentLCID { get; set; } = CultureInfo.CurrentCulture.LCID;
    }
}
