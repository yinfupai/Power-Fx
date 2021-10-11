//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic;
using Microsoft.AppMagic.Authoring;
using Microsoft.AppMagic.Authoring.Texl;
using Microsoft.PowerFx.Core.App;
using Microsoft.PowerFx.Core.Entities.QueryOptions;
using Microsoft.PowerFx.Core.Glue;
using PowerApps.Language.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.PowerFx.Core.App.Controls;

namespace Microsoft.PowerFx
{
    // $$$ Everything in this file should get removed. 
    internal class Glue2DocumentBinderGlue : IBinderGlue
    {
        public bool CanControlBeUsedInComponentProperty(TexlBinding binding, IExternalControl control)
        {
            throw new NotImplementedException();
        }

        public IExternalControl GetVariableScopedControlFromTexlBinding(TexlBinding txb)
        {
            throw new NotImplementedException();
        }

        public bool IsComponentDataSource(object lookupInfoData)
        {
            throw new NotImplementedException();
        }

        public bool IsComponentScopedPropertyFunction(TexlFunction infoFunction)
        {
            return false; // $$$
        }

        public bool IsContextProperty(IExternalControlProperty externalControlProperty)
        {
            throw new NotImplementedException();
        }

        public bool IsDataComponentDefinition(object lookupInfoData)
        {
            throw new NotImplementedException();
        }

        public bool IsDataComponentInstance(object lookupInfoData)
        {
            throw new NotImplementedException();
        }

        public bool IsDynamicDataSourceInfo(object lookupInfoData)
        {
            return false; // TODO: ?
        }

        public bool IsPrimaryCommandComponentProperty(IExternalControlProperty externalControlProperty)
        {
            throw new NotImplementedException();
        }

        public bool TryGetCdsDataSourceByBind(object lhsInfoData, out IExternalControl o)
        {
            throw new NotImplementedException();
        }
    }
}
