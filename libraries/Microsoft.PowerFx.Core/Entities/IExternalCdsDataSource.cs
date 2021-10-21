//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.App;
using Microsoft.PowerFx.Core.Entities.Delegation;

namespace Microsoft.PowerFx.Core.Entities
{
    internal interface IExternalCdsDataSource : IExternalTabularDataSource
    {
        string PrimaryNameField { get; }
        string DatasetName { get; }
        IExternalDocument Document { get; }
        IExternalTableDefinition TableDefinition { get; }

        bool TryGetRelatedColumn(string selectColumnName, out string additionalColumnName, IExternalTableDefinition expandsTableDefinition = null);
    }
}