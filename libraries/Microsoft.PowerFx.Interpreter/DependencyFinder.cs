//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;
using Microsoft.AppMagic.Authoring.Texl;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace Microsoft.PowerFx
{
    // Find the variables that this formula depends on.
    // Used for recalc. 
    internal class DependencyFinder : IdentityTexlVisitor
    {
        private TexlBinding _binding;
        public HashSet<string> _vars = new HashSet<string>();

        public static HashSet<string> FindDependencies(TexlNode node, TexlBinding binding)
        {
            var v = new DependencyFinder
            {
                _binding = binding
            };
            node.Accept(v);
            return v._vars;
        }

        public override void Visit(FirstNameNode node)
        {
            FirstNameInfo info = _binding.GetInfo(node);

            var name = node.Ident.Name.Value;

            // Only include dependencies from the incoming context (Fields)
            // defined at the top level (NestDst==1)
            if ((info.NestDst == 1 && info.Kind == BindKind.LambdaField) ||
                (info.Kind == BindKind.ScopeVariable) ||
                (info.Kind == BindKind.PowerFxResolvedObject))
            {
                _vars.Add(name);
            }

            base.Visit(node);
        }
    }
}
