//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.PowerFx
{
    // For migrating feature gates. 
    // The feature gates are expired and should be removed. 
    internal class OldFeatureGates
    {
        public const bool CountDistinctDelegationSupport = false;

        public const bool RelationShip1NDelegationSupport = false;

        public const bool StringOperationsSupport = false;

        public const bool AggregateFunctionSupport = true;

        public const bool AggregateDateTimeSupport = false;
    }
}
