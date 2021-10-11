//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AppMagic.Authoring;
using Microsoft.AppMagic.Authoring.Texl;
using Microsoft.AppMagic.Common;

namespace Microsoft.PowerFx.Core.App.ErrorContainers
{
    /// <summary>
    /// Ensures that only errors under a given severity will be posted. This is
    /// useful if you're calling a function to check validity and don't want error
    /// side effects, but also want to provide warnings, for instance.
    /// </summary>
    internal class LimitedSeverityErrorContainer : IErrorContainer
    {
        private IErrorContainer errors;
        private DocumentErrorSeverity maximumSeverity;

        public DocumentErrorSeverity DefaultSeverity => errors.DefaultSeverity;

        public LimitedSeverityErrorContainer(IErrorContainer errors, DocumentErrorSeverity maximumSeverity)
        {
            this.errors = errors;
            this.maximumSeverity = maximumSeverity;
        }

        public TexlError EnsureError(TexlNode node, ErrorResourceKey errKey, params object[] args)
        {
            if (DefaultSeverity <= maximumSeverity)
            {
                return errors.EnsureError(node, errKey, args);
            }
            return null;
        }

        public TexlError EnsureError(DocumentErrorSeverity severity, TexlNode node, ErrorResourceKey errKey, params object[] args)
        {
            if (severity <= this.maximumSeverity)
            {
                return errors.EnsureError(severity, node, errKey, args);
            }
            return null;
        }

        public TexlError Error(TexlNode node, ErrorResourceKey errKey, params object[] args)
        {
            if (DefaultSeverity <= maximumSeverity)
            {
                return errors.Error(node, errKey, args);
            }
            return null;
        }

        public TexlError Error(DocumentErrorSeverity severity, TexlNode node, ErrorResourceKey errKey, params object[] args)
        {
            if (severity <= this.maximumSeverity)
            {
                return errors.Error(severity, node, errKey, args);
            }
            return null;
        }

        public void Errors(TexlNode node, DType nodeType, KeyValuePair<string, DType> schemaDifference, DType schemaDifferenceType)
        {
            if (DocumentErrorSeverity.Severe <= this.maximumSeverity)
            {
                errors.Errors(node, nodeType, schemaDifference, schemaDifferenceType);
            }
        }
    }
}
