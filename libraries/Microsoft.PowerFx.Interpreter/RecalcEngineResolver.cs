//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Microsoft.AppMagic;
using Microsoft.AppMagic.Authoring;
using Microsoft.AppMagic.Authoring.Texl;

namespace Microsoft.PowerFx
{
    /// <summary>
    /// <see cref="INameResolver"/> implementation for <see cref="RecalcEngine"/>
    /// </summary>
    internal class RecalcEngineResolver : SimpleResolver
    {
        private readonly RecalcEngine _parent;
        private readonly RecordType _parameters;

        public RecalcEngineResolver(
            RecalcEngine parent,
            RecordType parameters,
            IEnumerable<EnumSymbol> enumSymbols, 
            params TexlFunction[] extraFunctions) :
            base(enumSymbols, extraFunctions)
        {
            _parameters = parameters;
            _parent = parent;
        }

         public override bool Lookup(DName name, out NameLookupInfo nameInfo, NameLookupPreferences preferences = NameLookupPreferences.None)
        {
            // Kinds of globals:
            // - global formula
            // - parameters 

            string str = name.Value;

            var parameter = _parameters.MaybeGetFieldType(str);
            if (parameter != null)
            {
                var data = new ParameterData { ParameterName = str };
                var type = parameter._type;

                nameInfo = new NameLookupInfo(
                    BindKind.PowerFxResolvedObject,
                    type,
                    DPath.Root,
                    0,
                    data
                    );
                return true;
            }

            if (_parent.Formulas.TryGetValue(str, out var fi))
            {
                var data = fi;
                var type = fi._type._type;

                nameInfo = new NameLookupInfo(
                    BindKind.PowerFxResolvedObject,
                    type,
                    DPath.Root,
                    0,
                    data
                    );
                return true;
            }

            return base.Lookup(name, out nameInfo, preferences);
        }

        public class ParameterData
        {
            public string ParameterName;
        }
    }
}
