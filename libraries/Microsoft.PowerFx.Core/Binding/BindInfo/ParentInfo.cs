//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.App;

namespace Microsoft.AppMagic.Authoring.Texl
{
    /// <summary>
    /// Binding information for "Parent" names.
    /// </summary>
    internal sealed class ParentInfo : ControlKeywordInfo
    {
        public override DName Name { get { return new DName(TexlLexer.KeywordParent); } }

        public ParentInfo(ParentNode node, DPath path, IExternalControl data)
            : base(node, path, data) { }
    }
}