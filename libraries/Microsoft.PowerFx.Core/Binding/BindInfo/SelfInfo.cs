//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.App;
using Microsoft.PowerFx.Core.App.Controls;

namespace Microsoft.AppMagic.Authoring.Texl
{
    /// <summary>
    /// Binding information for "Self".
    /// </summary>
    internal sealed class SelfInfo : ControlKeywordInfo
    {
        public override DName Name { get { return new DName(TexlLexer.KeywordSelf); } }

        public SelfInfo(SelfNode node, DPath path, IExternalControl data)
            : base(node, path, data) { }
    }
}