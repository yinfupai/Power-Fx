//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal sealed class AsInfo
    {
        public readonly AsNode Node;
        public readonly DName AsIdentifier;

        public AsInfo(AsNode node, DName identifier)
        {
            Contracts.AssertValue(node);
            Contracts.AssertValid(identifier);

            AsIdentifier = identifier;
            Node = node;
        }
    }
}