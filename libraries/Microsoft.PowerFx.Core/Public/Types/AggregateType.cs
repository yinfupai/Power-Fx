//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Microsoft.PowerFx.Core.Types;
using Microsoft.PowerFx.Core.Utils;

namespace Microsoft.PowerFx.Core.Public.Types
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
