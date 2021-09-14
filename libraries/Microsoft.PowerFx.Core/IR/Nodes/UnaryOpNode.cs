//------------------------------------------------------------------------------
// <copyright file company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic;
using Microsoft.AppMagic.Authoring;
using Microsoft.AppMagic.Authoring.Texl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.PowerFx.Core.IR
{
    internal sealed class UnaryOpNode : IntermediateNode
    {
        public readonly UnaryOpKind Op;
        public readonly IntermediateNode Child;

        /// <summary>
        /// For Record->Record and Table->Table, provides coercions for individual fields
        /// Null for other ops
        /// </summary>
        public readonly Dictionary<DName, CoercionKind> AggregateFieldCoercions;
        private readonly FormulaType AggregateCoercionResultType;

        public UnaryOpNode(IRContext irContext, UnaryOpKind op, IntermediateNode child) : base(irContext)
        {
            Contracts.AssertValue(child);

            Op = op;
            Child = child;
        }

        public UnaryOpNode(IRContext irContext, UnaryOpKind op, IntermediateNode child, Dictionary<DName, CoercionKind> fieldCoercions, FormulaType resultType) : this(irContext, op, child)
        {
            Contracts.AssertValue(resultType);
            Contracts.AssertValue(fieldCoercions);

            AggregateFieldCoercions = fieldCoercions;
            AggregateCoercionResultType = resultType;
        }

        public override TResult Accept<TResult, TContext>(IRNodeVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override string ToString()
        {
            return $"UnaryOp({Op}, {Child})";
        }
    }
}
