//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring;

namespace Microsoft.PowerFx
{
    // Useful for representing fields in an aggregate.  
    public class NamedFormulaType
    {
        internal readonly TypedName _typedName;

        public NamedFormulaType(string name, FormulaType type)
        {
            _typedName = new TypedName(type._type, new DName(name));
        }

        internal NamedFormulaType(TypedName typedName)
        {
            _typedName = typedName;
        }

        public string Name => _typedName.Name;

        public FormulaType Type => FormulaType.Build(_typedName.Type);
    }
}
