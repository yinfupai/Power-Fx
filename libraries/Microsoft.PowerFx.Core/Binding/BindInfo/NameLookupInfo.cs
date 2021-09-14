//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring.Texl
{
    /// <summary>
    /// Temporary name information used by the Binder<->Document lookup handshake/mechanism.
    /// </summary>
    internal struct NameLookupInfo
    {
        public readonly BindKind Kind;
        public readonly DPath Path;
        public readonly int UpCount;
        public readonly DType Type;
        public readonly DName LogicalName;

        // Optional data associated with a name. May be null.
        public readonly object Data;

        public NameLookupInfo(BindKind kind, DType type, DPath path, int upCount, object data = null, DName logicalName = default(DName))
        {
            Contracts.Assert(BindKind._Min <= kind && kind < BindKind._Lim);
            Contracts.Assert(upCount >= 0);
            Contracts.AssertValueOrNull(data);

            Kind = kind;
            Type = type;
            Path = path;
            UpCount = upCount;
            Data = data;
            LogicalName = logicalName;
        }
    }
}