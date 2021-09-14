//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;

namespace Microsoft.AppMagic.Common
{
    public static class StringResources
    {
        // Key Type of string resources related to errors. Used by BaseError in DocError.cs to ensure
        // that it is passed a key as opposed to a generic string, such as the contents of the error
        // message.
        //
        // Existing keys for error messages are split between here (for general document errors) and
        // Strings.cs (for Texl errors).
        public struct ErrorResourceKey
        {
            public string Key { get; }

            public ErrorResourceKey(string key)
            {
                Contracts.AssertNonEmpty(key);

                Key = key;
            }
        }

        // ErrorResourceKey for creating an error from an arbitrary string message. The key resolves to "{0}", meaning
        // that a single string arg can be supplied representing the entire text of the error.
        public static ErrorResourceKey ErrGeneralError = new ErrorResourceKey("ErrGeneralError");

        // Other error keys.
        public static ErrorResourceKey ErrOpeningDocument_UnknownError = new ErrorResourceKey("ErrOpeningDocument_UnknownError");
        public static ErrorResourceKey ErrOpeningDocument_UnsupportedPreviousVersion = new ErrorResourceKey("ErrOpeningDocument_UnsupportedPreviousVersion");
        public static ErrorResourceKey ErrOpeningDocument_TooNewVersion = new ErrorResourceKey("ErrOpeningDocument_TooNewVersion");
        public static ErrorResourceKey ErrOpeningDocument_CorruptFile = new ErrorResourceKey("ErrOpeningDocument_CorruptFile");
        public static ErrorResourceKey ErrOpeningDocument_NotSupported = new ErrorResourceKey("ErrOpeningDocument_NotSupported");
        public static ErrorResourceKey ErrOpeningDocument_AccessDenied = new ErrorResourceKey("ErrOpeningDocument_AccessDenied");
        public static ErrorResourceKey ErrOpeningDocument_CorruptOnImport = new ErrorResourceKey("ErrOpeningDocument_CorruptOnImport");
        public static ErrorResourceKey ErrImport_UnableToReadTable_TableName = new ErrorResourceKey("ErrImport_UnableToReadTable_TableName");
        public static ErrorResourceKey ErrImport_UnableToReadSomeTables = new ErrorResourceKey("ErrImport_UnableToReadSomeTables");
        public static ErrorResourceKey ErrImport_UnhandledException = new ErrorResourceKey("ErrImport_UnhandledException");
        public static ErrorResourceKey ErrImport_EntityExists = new ErrorResourceKey("ErrImport_EntityExists");
        public static ErrorResourceKey ErrImport_EntitiesRemoved = new ErrorResourceKey("ErrImport_EntitiesRemoved");
        public static ErrorResourceKey PlayerPublishWarnExternalFiles = new ErrorResourceKey("PlayerPublishWarnExternalFiles");
        public static ErrorResourceKey ErrImport_UnsupportedDocumentType = new ErrorResourceKey("ErrImport_UnsupportedDocumentType");

        public class ErrorResource
        {
            public const string XmlType = "errorResource";

            // The default error message.
            public const string ShortMessageTag = "shortMessage";
            // Optional: A longer explanation of the error. There is currently no UI (or DocError) support for this.
            public const string LongMessageTag = "longMessage";
            // Optional: A series of messages explaining how to fix the error.
            public const string HowToFixTag = "howToFixMessage";
            // Optional: A series of messages explaining why to fix the error. Used primarily for accessibility errors.
            public const string WhyToFixTag = "whyToFixMessage";
            // Optional: A series of links to help documents. There is currently no UI (or DocError) support for this.
            public const string LinkTag = "link";
            public const string LinkTagDisplayTextTag = "value";
            public const string LinkTagUrlTag = "url";

            private Dictionary<string, IList<string>> TagToValues;
            public IList<IErrorHelpLink> HelpLinks { get; }

            private ErrorResource()
            {
                TagToValues = new Dictionary<string, IList<string>>();
                HelpLinks = new List<IErrorHelpLink>();
            }

            public static ErrorResource Parse(XElement errorXml)
            {
                Contracts.AssertValue(errorXml);

                var errorResource = new ErrorResource();

                // Parse each sub-element into the TagToValues dictionary.
                foreach (var tag in errorXml.Elements())
                {
                    string tagName = tag.Name.LocalName;

                    // Links are specialized because they are a two-part resource.
                    if (tagName == LinkTag)
                    {
                        errorResource.AddHelpLink(tag);
                    }
                    else
                    {
                        if (!errorResource.TagToValues.ContainsKey(tagName))
                            errorResource.TagToValues[tagName] = new List<string>();

                        errorResource.TagToValues[tagName].Add(tag.Element("value").Value);
                    }
                }

                return errorResource;
            }

            private void AddHelpLink(XElement linkTag)
            {
                Contracts.AssertValue(linkTag);

                HelpLinks.Add(new ErrorHelpLink(linkTag.Element(LinkTagDisplayTextTag).Value, linkTag.Element(LinkTagUrlTag).Value));
            }

            public string GetSingleValue(string tag)
            {
                Contracts.AssertValue(tag);

                if (!TagToValues.ContainsKey(tag))
                    return null;

                Contracts.Assert(TagToValues[tag].Count == 1);

                return TagToValues[tag][0];
            }

            public IList<string> GetValues(string tag) {
                if (!TagToValues.ContainsKey(tag))
                    return null;
                return TagToValues[tag];
            }
        }

        private const string FallbackLocale = "en-US";

        public static ErrorResource GetErrorResource(ErrorResourceKey resourceKey, string locale = null)
        {
            Contracts.CheckValue(resourceKey.Key, "action");
            Contracts.CheckValueOrNull(locale, "locale");

            ErrorResource resourceValue;

            // As foreign languages can lag behind en-US while being localized, if we can't find it then always look in the en-US locale
            if (!TryGetErrorResource(resourceKey, out resourceValue, locale) && !TryGetErrorResource(resourceKey, out resourceValue, FallbackLocale))
            {
                Debug.WriteLine(string.Format("ERROR error resource {0} not found", resourceKey));
                throw new System.IO.FileNotFoundException(resourceKey.Key);
            }

            return resourceValue;
        }

        public static string Get(ErrorResourceKey resourceKey, string locale = null)
        {
            return Get(resourceKey.Key, locale);
        }

        public static string Get(string resourceKey, string locale = null)
        {
            Contracts.CheckValue(resourceKey, "action");
            Contracts.CheckValueOrNull(locale, "locale");

            string resourceValue;

            // As foreign languages can lag behind en-US while being localized, if we can't find it then always look in the en-US locale
            if (!TryGet(resourceKey, out resourceValue, locale) && !TryGet(resourceKey, out resourceValue, FallbackLocale))
            {
                // Prior to ErrorResources, error messages were fetched like other string resources.
                // The resource associated with the key corresponds to the ShortMessage of the new
                // ErrorResource objects. For backwards compatibility with tests/telemetry that fetched
                // the error message manually (as opposed to going through the DocError class), we check
                // if there is an error resource associated with this key if we did not find it normally.
                ErrorResource potentialErrorResource;
                if (TryGetErrorResource(new ErrorResourceKey(resourceKey), out potentialErrorResource))
                    return potentialErrorResource.GetSingleValue(ErrorResource.ShortMessageTag);

                Debug.WriteLine(string.Format("ERROR resource string {0} not found", resourceKey));
                throw new System.IO.FileNotFoundException(resourceKey);
            }

            return resourceValue;
        }

        // One resource dictionary per locale
        private static Dictionary<string, Dictionary<string, string>> Strings = new Dictionary<string, Dictionary<string, string>>();
        private static Dictionary<string, Dictionary<string, ErrorResource>> ErrorResources = new Dictionary<string, Dictionary<string, ErrorResource>>();

        private class TypeFromThisAssembly
        { }

        public static bool TryGetErrorResource(ErrorResourceKey resourceKey, out ErrorResource resourceValue, string locale = null)
        {
            Contracts.CheckValue(resourceKey.Key, "action");
            Contracts.CheckValueOrNull(locale, "locale");

            if (locale == null)
            {
                locale = CurrentLocaleInfo.CurrentUILanguageName;
                Contracts.CheckNonEmpty(locale, "currentLocale");
            }

            Dictionary<string, ErrorResource> errorResources;

            if (!ErrorResources.TryGetValue(locale, out errorResources))
            {
                Dictionary<string, string> strings;
                LoadFromResource(locale, out strings, out errorResources);
                Strings[locale] = strings;
                ErrorResources[locale] = errorResources;
            }

            return errorResources.TryGetValue(resourceKey.Key.ToLowerInvariant(), out resourceValue);
        }

        public static bool TryGet(string resourceKey, out string resourceValue, string locale = null)
        {
            Contracts.CheckValue(resourceKey, "action");
            Contracts.CheckValueOrNull(locale, "locale");

            if (locale == null)
            {
                locale = CurrentLocaleInfo.CurrentUILanguageName;
                Contracts.CheckNonEmpty(locale, "currentLocale");
            }

            Dictionary<string, string> strings;

            if (!Strings.TryGetValue(locale, out strings))
            {
                Dictionary<string, ErrorResource> errorResources;
                LoadFromResource(locale, out strings, out errorResources);
                Strings[locale] = strings;
                ErrorResources[locale] = errorResources;
            }

            return strings.TryGetValue(resourceKey.ToLowerInvariant(), out resourceValue);
        }

        private static void LoadFromResource(string locale, out Dictionary<string, string> strings, out Dictionary<string, ErrorResource> errorResources)
        {
            var assembly = new TypeFromThisAssembly().GetType().Assembly;

            // This is being done because the filename of the manifest is case sensitive e.g. given zh-CN it was returning English
            if (locale.Equals("zh-CN"))
            {
                locale = "zh-cn";
            }
            else if (locale.Equals("zh-TW"))
            {
                locale = "zh-tw";
            }
            else if (locale.Equals("ko-KR"))
            {
                locale = "ko-kr";
            }

            using (var res = assembly.GetManifestResourceStream("Microsoft.PowerFx.Core.strings." + locale.Replace("-", "_") + ".Resources.pares"))
            {
                if (res == null)
                {
                    if (Strings.TryGetValue(FallbackLocale, out strings) && ErrorResources.TryGetValue(FallbackLocale, out errorResources))
                        return;                             // use the already-loaded en-US strings

                    if (locale == FallbackLocale)
                    {
                        throw new InvalidProgramException(string.Format("[StringResources] Resources not found for locale '{0}' and failed to find fallback", locale));
                    }

                    LoadFromResource(FallbackLocale, out strings, out errorResources);               // load the default ones (recursive, but not infinite due to check above)
                }
                else
                {
                    var loadedStrings = XDocument.Load(res).Descendants(XName.Get("data"));
                    strings = new Dictionary<string, string>();
                    errorResources = new Dictionary<string, ErrorResource>();
                    foreach (var item in loadedStrings)
                    {
                        string type;
                        if (item.TryGetNonEmptyAttributeValue("type", out type) && type == ErrorResource.XmlType)
                            errorResources[item.Attribute("name").Value.ToLowerInvariant()] = ErrorResource.Parse(item);
                        else
                            strings[item.Attribute("name").Value.ToLowerInvariant()] = item.Element("value").Value;
                    }
                }
            }
        }
    }
}
