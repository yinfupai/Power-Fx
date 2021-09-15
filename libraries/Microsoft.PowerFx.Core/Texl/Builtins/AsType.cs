//------------------------------------------------------------------------------
// <copyright file="AsType.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Microsoft.AppMagic.Common;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // AsType(record:P, table:*[]): ![]
    internal sealed class AsTypeFunction : BuiltinFunction
    {
        public const string AsTypeInvariantFunctionName = "AsType";
        public override bool RequiresErrorContext => true;
        public override bool IsAsync => true;
        public override bool CanReturnExpandInfo => true;
        public override bool IsSelfContained => true;

        public AsTypeFunction()
            : base(AsTypeInvariantFunctionName, TexlStrings.AboutAsType, FunctionCategories.Table, DType.EmptyRecord, 0, 2, 2, DType.Error /* Polymorphic type is checked in override */, DType.EmptyTable)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new[] { TexlStrings.AsTypeArg1, TexlStrings.AsTypeArg2 };
        }

        public override bool CheckInvocation(TexlBinding binding, TexlNode[] args, DType[] argTypes, IErrorContainer errors, out DType returnType)
        {
            Contracts.AssertValue(args);
            Contracts.AssertAllValues(args);
            Contracts.AssertValue(argTypes);
            Contracts.Assert(args.Length == 2);
            Contracts.Assert(argTypes.Length == 2);
            Contracts.AssertValue(errors);

            if (!base.CheckInvocation(binding, args, argTypes, errors, out returnType))
                return false;

            // Check if first argument is poly type or an activity pointer
            if (!argTypes[0].IsPolymorphic && !argTypes[0].IsActivityPointer)
            {
                errors.EnsureError(DocumentErrorSeverity.Severe, args[0], TexlStrings.ErrBadType_ExpectedType_ProvidedType, DKind.Polymorphic.ToString(), argTypes[0].GetKindString());
                return false;
            }

            // Check if table arg referrs to a connected data source.
            TexlNode tableArg = args[1];
            FirstNameInfo tableInfo;
            IExternalDataSource tableDsInfo;
            if (!binding.TryGetFirstNameInfo(tableArg.Id, out tableInfo) ||
                (tableDsInfo = (tableInfo.Data as IExternalDataSource)) == null ||
                !(tableDsInfo is IExternalTabularDataSource))
            {
                errors.EnsureError(tableArg, TexlStrings.ErrAsTypeAndIsTypeExpectConnectedDataSource);
                return false;
            }

            if (binding.Document.Properties.EnabledFeatures.IsEnhancedDelegationEnabled && (tableDsInfo is IExternalCdsDataSource) && argTypes[0].HasPolymorphicInfo)
            {
                var expandInfo = argTypes[0].PolymorphicInfo.TryGetExpandInfo(tableDsInfo.TableMetadata.Name);
                if (expandInfo != null)
                {
                    returnType = argTypes[0].ExpandPolymorphic(argTypes[1], expandInfo);
                    return true;
                }
            }

            returnType = argTypes[1].ToRecord();
            return true;
        }

        public override bool IsRowScopedServerDelegatable(CallNode callNode, TexlBinding binding, OperationCapabilityMetadata metadata)
        {
            return binding.Document.Properties.EnabledFeatures.IsEnhancedDelegationEnabled && metadata.IsDelegationSupportedByTable(DelegationCapability.AsType);
        }

        protected override bool RequiresPagedDataForParamCore(TexlNode[] args, int paramIndex, TexlBinding binding)
        {
            Contracts.AssertValue(args);
            Contracts.AssertAllValues(args);
            Contracts.Assert(0 <= paramIndex && paramIndex < args.Length);
            Contracts.AssertValue(binding);
            Contracts.Assert(binding.IsPageable(args[paramIndex].VerifyValue()));

            // For the second argument, we need only metadata. No actual data from datasource is required.
            return paramIndex != 1;
        }
    }
}
