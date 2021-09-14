//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal sealed class ChainTrackerVisitor : IdentityTexlVisitor
    {
        private bool _usesChains;

        private ChainTrackerVisitor()
        {
            _usesChains = false;
        }

        public static bool Run(TexlNode node)
        {
            ChainTrackerVisitor visitor = new ChainTrackerVisitor();
            node.Accept(visitor);
            return visitor._usesChains;
        }

        public override bool PreVisit(VariadicOpNode node)
        {
            if (node.Op == VariadicOp.Chain)
            {
                _usesChains = true;
                return false;
            }

            return true;
        }
    }
}