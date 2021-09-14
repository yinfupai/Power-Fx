//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring.Texl;
using Microsoft.PowerFx.Core.IR;
using System;

namespace Microsoft.PowerFx.Functions
{
    partial class Library
    {
        #region Standard Error Handling Wrappers for Coercion Functions
        public static FunctionPtr OperatorUnaryNumberToText = StandardErrorHandling<NumberValue>(
            expandArguments: NoArgExpansion,
            replaceBlankValues: DoNotReplaceBlank,
            checkRuntimeTypes: ExactValueTypeOrBlank<NumberValue>,
            checkRuntimeValues: FiniteChecker,
            returnBehavior: ReturnBehavior.ReturnBlankIfAnyArgIsBlank,
            targetFunction: NumberToText
        );

        public static FunctionPtr OperatorUnaryNumberToBoolean = StandardErrorHandling<NumberValue>(
            expandArguments: NoArgExpansion,
            replaceBlankValues: DoNotReplaceBlank,
            checkRuntimeTypes: ExactValueTypeOrBlank<NumberValue>,
            checkRuntimeValues: FiniteChecker,
            returnBehavior: ReturnBehavior.ReturnBlankIfAnyArgIsBlank,
            targetFunction: NumberToBoolean
        );

        public static FunctionPtr OperatorUnaryBooleanToText = StandardErrorHandling<BooleanValue>(
            expandArguments: NoArgExpansion,
            replaceBlankValues: DoNotReplaceBlank,
            checkRuntimeTypes: ExactValueTypeOrBlank<BooleanValue>,
            checkRuntimeValues: DeferRuntimeValueChecking,
            returnBehavior: ReturnBehavior.ReturnBlankIfAnyArgIsBlank,
            targetFunction: BooleanToText
        );

        public static FunctionPtr OperatorUnaryBooleanToNumber = StandardErrorHandling<BooleanValue>(
            expandArguments: NoArgExpansion,
            replaceBlankValues: DoNotReplaceBlank,
            checkRuntimeTypes: ExactValueTypeOrBlank<BooleanValue>,
            checkRuntimeValues: DeferRuntimeValueChecking,
            returnBehavior: ReturnBehavior.ReturnBlankIfAnyArgIsBlank,
            targetFunction: BooleanToNumber
        );

        public static FunctionPtr OperatorUnaryTextToBoolean = StandardErrorHandling<StringValue>(
            expandArguments: NoArgExpansion,
            replaceBlankValues: DoNotReplaceBlank,
            checkRuntimeTypes: ExactValueTypeOrBlank<BooleanValue>,
            checkRuntimeValues: DeferRuntimeValueChecking,
            returnBehavior: ReturnBehavior.ReturnBlankIfAnyArgIsBlank,
            targetFunction: TextToBoolean
        );
        #endregion

        #region Coercion Functions
        public static FormulaValue NumberToText(IRContext irContext, NumberValue[] args)
        {
            return Text(irContext, args);
        }

        public static FormulaValue NumberToBoolean(IRContext irContext, NumberValue[] args)
        {
            var n = args[0].Value;
            return new BooleanValue(irContext, n != 0.0);
        }

        public static FormulaValue BooleanToText(IRContext irContext, BooleanValue[] args)
        {
            var b = args[0].Value;
            return new StringValue(irContext, b ? "true" : "false");
        }

        public static FormulaValue BooleanToNumber(IRContext irContext, BooleanValue[] args)
        {
            var b = args[0].Value;
            return new NumberValue(irContext, b ? 1.0 : 0.0);
        }

        public static FormulaValue TextToBoolean(IRContext irContext, StringValue[] args)
        {
            var s = args[0].Value;
            return new BooleanValue(irContext, s == "true");
        }
        #endregion
    }
}