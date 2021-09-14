//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal sealed class DottedNameInfo
    {
        public readonly DottedNameNode Node;

        // Optional data associated with a DottedNameNode. May be null.
        public readonly object Data;

        public DottedNameInfo(DottedNameNode node, object data = null)
        {
            Contracts.AssertValue(node);
            Contracts.AssertValueOrNull(data);

            Node = node;
            Data = data;
        }
    }
}