//------------------------------------------------------------------------------
// <copyright file company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.AppMagic;

namespace Microsoft.PowerFx.Core.IR.Symbols
{
    internal class ScopeAccessSymbol : IScopeSymbol
    {
        public ScopeSymbol Parent { get; }
        public int Index { get; }

        public DName Name => Parent.AccessedFields.ElementAtOrDefault(Index);


        public ScopeAccessSymbol(ScopeSymbol parent, int index)
        {
            Contracts.AssertValue(parent);
            Contracts.AssertIndex(index, parent.AccessedFields.Count);

            Parent = parent;
            Index = index;
        }

        public override string ToString()
        {
            return $"{Parent}, {Name}";
        }
    }
}
