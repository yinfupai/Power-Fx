//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AppMagic.Common;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Default "no-op" error container that does not post document errors.
    // See the TexlFunction.DefaultErrorContainer property and its uses for more info.
    internal sealed class DefaultNoOpErrorContainer : IErrorContainer
    {
        public DocumentErrorSeverity DefaultSeverity => DocumentErrorSeverity._Min;

        public TexlError EnsureError(TexlNode node, StringResources.ErrorResourceKey errKey, params object[] args)
        {
            return null;
        }

        public TexlError Error(TexlNode node, StringResources.ErrorResourceKey errKey, params object[] args)
        {
            return null;
        }

        public TexlError EnsureError(DocumentErrorSeverity severity, TexlNode node, StringResources.ErrorResourceKey errKey, params object[] args)
        {
            return null;
        }

        public TexlError Error(DocumentErrorSeverity severity, TexlNode node, StringResources.ErrorResourceKey errKey, params object[] args)
        {
            return null;
        }

        public void Errors(TexlNode node, DType nodeType, KeyValuePair<string, DType> schemaDifference, DType schemaDifferenceType)
        {
            // Do nothing.
        }
    }
}