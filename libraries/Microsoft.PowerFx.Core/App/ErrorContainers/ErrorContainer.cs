//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Common;
using System.Collections.Generic;

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal class ErrorContainer : IErrorContainer
    {
        private List<TexlError> _errors;

        public DocumentErrorSeverity DefaultSeverity => DocumentErrorSeverity.Critical;

        public bool HasErrors()
        {
            return CollectionUtils.Size(_errors) > 0;
        }

        public bool HasErrors(TexlNode node, DocumentErrorSeverity severity = DocumentErrorSeverity.Suggestion)
        {
            Contracts.AssertValue(node);

            if (CollectionUtils.Size(_errors) == 0)
                return false;

            foreach (var err in _errors)
            {
                if (err.Node == node && err.Severity >= severity)
                    return true;
            }

            return false;
        }

        public bool HasErrorsInTree(TexlNode rootNode, DocumentErrorSeverity severity = DocumentErrorSeverity.Suggestion)
        {
            Contracts.AssertValue(rootNode);

            if (CollectionUtils.Size(_errors) == 0)
                return false;

            foreach (var err in _errors)
            {
                if (err.Node.InTree(rootNode) && err.Severity >= severity)
                    return true;
            }

            return false;
        }

        public bool GetErrors(ref List<TexlError> rgerr)
        {
            if (CollectionUtils.Size(_errors) == 0)
                return false;
            CollectionUtils.Add(ref rgerr, _errors);
            return true;
        }

        public IEnumerable<TexlError> GetErrors()
        {
            if (_errors != null)
            {
                foreach (var err in _errors)
                    yield return err;
            }
        }

        public TexlError EnsureError(TexlNode node, StringResources.ErrorResourceKey errKey, params object[] args)
        {
            return EnsureError(DefaultSeverity, node, errKey, args);
        }

        public TexlError EnsureError(DocumentErrorSeverity severity, TexlNode node, StringResources.ErrorResourceKey errKey, params object[] args)
        {
            Contracts.AssertValue(node);
            Contracts.AssertValue(args);

            if (!HasErrors(node, severity))
                return Error(severity, node, errKey, args);

            return null;
        }

        public TexlError Error(TexlNode node, StringResources.ErrorResourceKey errKey, params object[] args)
        {
            return Error(DefaultSeverity, node, errKey, args);
        }

        public TexlError Error(DocumentErrorSeverity severity, TexlNode node, StringResources.ErrorResourceKey errKey, params object[] args)
        {
            Contracts.AssertValue(node);
            Contracts.AssertValue(args);

            TexlError err = new TexlError(node, severity, errKey, args);
            CollectionUtils.Add(ref _errors, err);
            return err;
        }

        public void Errors(TexlNode node, DType nodeType, KeyValuePair<string, DType> schemaDifference, DType schemaDifferenceType)
        {
            Contracts.AssertValue(node);
            Contracts.AssertValid(nodeType);

            Error(DocumentErrorSeverity.Severe, node, TexlStrings.ErrBadSchema_ExpectedType, nodeType.GetKindString());

            // If there's no schema difference, this was just an invalid type.
            if (string.IsNullOrEmpty(schemaDifference.Key))
                return;

            if (schemaDifferenceType.IsValid)
            {
                Error(
                    DocumentErrorSeverity.Severe,
                    node,
                    TexlStrings.ErrColumnTypeMismatch_ColName_ExpectedType_ActualType,
                    schemaDifference.Key,
                    schemaDifference.Value.GetKindString(),
                    schemaDifferenceType.GetKindString());
            }
            else
            {
                Error(
                    DocumentErrorSeverity.Severe,
                    node,
                    TexlStrings.ErrColumnMissing_ColName_ExpectedType,
                    schemaDifference.Key,
                    schemaDifference.Value.GetKindString());
            }
        }
    }
}
