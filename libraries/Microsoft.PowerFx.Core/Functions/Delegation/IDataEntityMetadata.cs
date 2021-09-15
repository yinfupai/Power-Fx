//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Common;
using Microsoft.PowerFx.Core.Delegation;
using Microsoft.PowerFx.Core.Entities.Delegation;

namespace Microsoft.AppMagic.Authoring
{
    /// <summary>
    /// Metadata information about data entity types.
    /// </summary>
    internal interface IDataEntityMetadata
    {
        string EntityName { get; }
        DType Schema { get; }
        void LoadClientSemantics(bool isPrimaryTable = false);
        void SetClientSemantics(IExternalTableDefinition tableDefinition);

        BidirectionalDictionary<string, string> DisplayNameMapping { get; }
        BidirectionalDictionary<string, string> PreviousDisplayNameMapping { get; }
        bool IsConvertingDisplayNameMapping { get; set; }
        IDelegationMetadata DelegationMetadata { get; }
        IExternalTableDefinition EntityDefinition { get; }
        string DatasetName { get; }
        bool IsValid { get; }
        string OriginalDataDescriptionJson { get; }
        string InternalRepresentationJson { get; }
        void ActualizeTemplate(string datasetName);
        void ActualizeTemplate(string datasetName, string entityName);
        string ToJsonDefinition();
    }
}
