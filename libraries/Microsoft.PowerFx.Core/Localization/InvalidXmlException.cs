//------------------------------------------------------------------------------
// <copyright file="InvalidXmlException.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.AppMagic.Common
{
    /// <summary>
    /// Represents an exception that occurs while reading and validating an xml document.
    /// It's intended to be safe to display the message back to the user.
    /// </summary>
    internal sealed class InvalidXmlException : XmlException
    {

        /// <param name="reason">The user-friendly reason why the xml document is invalid.</param>
        public InvalidXmlException(string reason)
            : base(reason)
        {
            Contracts.AssertNonEmpty(reason);
        }

        public InvalidXmlException(string reason, XAttribute invalidAttr)
            : base(reason, null, GetLineNumber(invalidAttr), GetLinePosition(invalidAttr))
        {
            Contracts.AssertNonEmpty(reason);
            Contracts.AssertValue(invalidAttr);
        }

        public InvalidXmlException(string reason, XElement invalidElem)
            : base(reason, null, GetLineNumber(invalidElem), GetLinePosition(invalidElem))
        {
            Contracts.AssertNonEmpty(reason);
            Contracts.AssertValue(invalidElem);
        }

        public InvalidXmlException(string reason, XObject invalidXObject)
            : base(reason, null, GetLineNumber(invalidXObject), GetLinePosition(invalidXObject))
        {
            Contracts.AssertNonEmpty(reason);
            Contracts.AssertValue(invalidXObject);
        }

        private static int GetLineNumber(XObject xobj)
        {
            Contracts.AssertValue(xobj, "The ctor chosen requires the invalid XObject argument to be non-null.");
            return ((IXmlLineInfo)xobj).LineNumber;
        }

        private static int GetLinePosition(XObject xobj)
        {
            Contracts.AssertValue(xobj, "The ctor chosen requires the invalid XObject argument to be non-null.");
            return ((IXmlLineInfo)xobj).LinePosition;
        }

    }
}
