//------------------------------------------------------------------------------
// <copyright file="Concatenate.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AppMagic.Authoring.Texl
{
    // Concatenate(source1:s|*[s], source2:s|*[s], ...)
    // Corresponding DAX function: Concatenate
    // Note, this performs string/string, string/Table, table/Table concatenation.
    internal sealed class ConcatenateFunction : BuiltinFunction
    {
        public override bool IsSelfContained => true;

        public ConcatenateFunction()
            : base("Concatenate", TexlStrings.AboutConcatenate, FunctionCategories.Table | FunctionCategories.Text, DType.Unknown, 0, 2, int.MaxValue)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new [] { TexlStrings.ConcatenateArg1, TexlStrings.ConcatenateArg1 };
            yield return new [] { TexlStrings.ConcatenateArg1, TexlStrings.ConcatenateArg1, TexlStrings.ConcatenateArg1 };
            yield return new [] { TexlStrings.ConcatenateArg1, TexlStrings.ConcatenateArg1, TexlStrings.ConcatenateArg1, TexlStrings.ConcatenateArg1 };
        }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures(int arity)
        {
            if (arity > 2)
                return GetGenericSignatures(arity, TexlStrings.ConcatenateArg1, TexlStrings.ConcatenateArg1);
            return base.GetSignatures(arity);
        }

        public override bool CheckInvocation(TexlNode[] args, DType[] argTypes, IErrorContainer errors, out DType returnType)
        {
            Contracts.AssertValue(args);
            Contracts.AssertValue(argTypes);
            Contracts.Assert(args.Length == argTypes.Length);
            Contracts.Assert(args.Length >= 2);
            Contracts.AssertValue(errors);

            int count = args.Length;
            bool hasTableArg = false;
            bool fArgsValid = true;

            // Type check the args
            // If any one input argument is of table type, then the returnType will be table type.
            for (int i = 0; i < count; i++)
            {
                bool isTable;
                fArgsValid &= CheckParamIsTypeOrSingleColumnTable(DType.String, args[i], argTypes[i], errors, out isTable);
                hasTableArg |= isTable;
            }

            returnType = hasTableArg ? DType.CreateTable(new TypedName(DType.String, OneColumnTableResultName)) : DType.String;
            
            return fArgsValid;
        }
    }
}
