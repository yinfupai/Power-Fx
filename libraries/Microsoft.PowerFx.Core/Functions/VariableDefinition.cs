//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring.Texl
{
    // This class captures the information about a single variable definition
    internal sealed class VariableDefinition
    {
        public DName Name { get; private set; }
        public bool IsGlobal { get; private set; }
        public TexlNode Node { get; private set; }

        public VariableDefinition(DName name, bool isGlobal, TexlNode node)
        {
            Contracts.AssertValid(name);
            Contracts.AssertValue(node);
            
            Name = name;
            IsGlobal = isGlobal;
            Node = node;
        }
    }
}
