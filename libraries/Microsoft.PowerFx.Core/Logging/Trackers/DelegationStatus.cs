//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.PowerFx.Core.Logging.Trackers
{
    internal enum DelegationStatus
    {
        DelegationSuccessful,
        BinaryOpNoSupported,
        DataSourceNotDelegatable,
        UndelegatableFunction,
        AsyncPredicate,
        BinaryOpNotSupportedByTable,
        UnaryOpNotSupportedByTable,
        ImpureNode,
        NoDelSupportByColumn,
        UnSupportedSortArg,
        AsyncSortOrder,
        SortOrderNotSupportedByColumn,
        NotANumberArgType,
        InvalidArgType,
        UnSupportedRowScopedDottedNameNode,
        UnSupportedDistinctArg
    }
}
