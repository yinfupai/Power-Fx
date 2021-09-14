//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Value(arg:s|n [,language:s])
    // Corresponding Excel and DAX function: Value
    internal sealed class ValueFunction : BuiltinFunction
    {
        public const string ValueInvariantFunctionName = "Value";
        public override bool RequiresErrorContext => true;
        public override bool IsSelfContained => true;

        public ValueFunction()
            : base(ValueInvariantFunctionName, TexlStrings.AboutValue, FunctionCategories.Text, DType.Number, 0, 1, 2, DType.String, DType.String)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.ValueArg1 };
            yield return new [] { TexlStrings.ValueArg1, TexlStrings.ValueArg2 };
        }

        public override bool CheckInvocation(TexlNode[] args, DType[] argTypes, IErrorContainer errors, out DType returnType)
        {
            Contracts.AssertValue(args);
            Contracts.AssertAllValues(args);
            Contracts.AssertValue(argTypes);
            Contracts.AssertAllValid(argTypes);
            Contracts.Assert(args.Length == argTypes.Length);
            Contracts.AssertValue(errors);
            Contracts.Assert(MinArity <= args.Length && args.Length <= MaxArity);

            bool isValid = true;

            DType argType = argTypes[0];
            if (!DType.Number.Accepts(argType) && !DType.String.Accepts(argType))
            {
                errors.EnsureError(DocumentErrorSeverity.Severe, args[0], TexlStrings.ErrNumberOrStringExpected);
                isValid = false;
            }

            if (args.Length > 1)
            {
                argType = argTypes[1];
                if (!DType.String.Accepts(argType))
                {
                    errors.EnsureError(DocumentErrorSeverity.Severe, args[1], TexlStrings.ErrStringExpected);
                    isValid = false;
                }
            }

            returnType = DType.Number;
            return isValid;
        }

        public override bool HasSuggestionsForParam(int index)
        {
            return index == 1;
        }
    }
}
