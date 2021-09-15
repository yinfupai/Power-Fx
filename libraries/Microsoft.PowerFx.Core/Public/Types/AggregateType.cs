//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.PowerFx
{
    public abstract class AggregateType : FormulaType
    {
        internal AggregateType(DType type) : base(type)
        {
        }

        // Enumerate fields
        public IEnumerable<NamedFormulaType> GetNames()
        {
            IEnumerable<TypedName> names = this._type.GetAllNames(DPath.Root);
            return from name in names select new NamedFormulaType(name);
        }
    }
}
