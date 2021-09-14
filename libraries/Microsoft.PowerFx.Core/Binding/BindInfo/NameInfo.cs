//------------------------------------------------------------------------------
// <copyright file="NameInfo.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring.Texl
{
    /// <summary>
    /// Binding information associated with a name.
    /// </summary>
    internal abstract class NameInfo
    {
        public readonly BindKind Kind;
        public readonly NameNode Node;

        public abstract DName Name { get; }

        protected NameInfo(BindKind kind, NameNode node)
        {
            Contracts.Assert(BindKind._Min <= kind && kind < BindKind._Lim);
            Contracts.AssertValue(node);

            Kind = kind;
            Node = node;
        }

        /// <summary>
        /// Asserts that the object is in fact of type T before casting.
        /// </summary>
        public T As<T>() where T : NameInfo
        {
            Contracts.Assert(this is T);

            return (T)this;
        }
    }
}
