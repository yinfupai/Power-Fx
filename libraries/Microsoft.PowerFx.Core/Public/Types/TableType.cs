//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;
using System.Diagnostics.Contracts;

namespace Microsoft.PowerFx
{
    public class TableType : AggregateType
    {
        internal TableType(DType type) : base(type)
        {
            Contract.Assert(type.IsTable);
        }

        public TableType() : base(DType.EmptyTable)
        {
        }

        internal static TableType FromRecord(RecordType type)
        {
            var tableType = type._type.ToTable();
            return new TableType(tableType);
        }


        public override void Visit(ITypeVistor vistor)
        {
            vistor.Visit(this);
        }

        public TableType Add(NamedFormulaType field)
        {
            var newType = _type.Add(field._typedName);
            return new TableType(newType);
        }

        public RecordType ToRecord()
        {
            return new RecordType(this._type.ToRecord());
        }
    }
}