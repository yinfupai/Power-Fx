//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.IR;
using System;
using System.Linq;
using Microsoft.PowerFx.Core.Public.Values;

namespace Microsoft.PowerFx.Functions
{
    internal static partial class Library
    {
        #region Operator Standard Error Handling Wrappers
        public static FunctionPtr OperatorBinaryAdd = StandardErrorHandling<NumberValue>(
            expandArguments: NoArgExpansion,
            replaceBlankValues: ReplaceBlankWithZero,
            checkRuntimeTypes: ExactValueType<NumberValue>,
            checkRuntimeValues: FiniteChecker,
            returnBehavior: ReturnBehavior.AlwaysEvaluateAndReturnResult,
            targetFunction: NumericAdd
        );

        public static FunctionPtr OperatorBinaryMul = StandardErrorHandling<NumberValue>(
            expandArguments: NoArgExpansion,
            replaceBlankValues: ReplaceBlankWithZero,
            checkRuntimeTypes: ExactValueType<NumberValue>,
            checkRuntimeValues: FiniteChecker,
            returnBehavior: ReturnBehavior.AlwaysEvaluateAndReturnResult,
            targetFunction: NumericMul
        );

        public static FunctionPtr OperatorBinaryDiv = StandardErrorHandling<NumberValue>(
            expandArguments: NoArgExpansion,
            replaceBlankValues: ReplaceBlankWithZero,
            checkRuntimeTypes: ExactValueType<NumberValue>,
            checkRuntimeValues: DivideByZeroChecker,
            returnBehavior: ReturnBehavior.AlwaysEvaluateAndReturnResult,
            targetFunction: NumericDiv
        );

        public static FunctionPtr OperatorBinaryGt = StandardErrorHandling<NumberValue>(
            expandArguments: NoArgExpansion,
            replaceBlankValues: ReplaceBlankWithZero,
            checkRuntimeTypes: ExactValueType<NumberValue>,
            checkRuntimeValues: FiniteChecker,
            returnBehavior: ReturnBehavior.AlwaysEvaluateAndReturnResult,
            targetFunction: NumericGt
        );

        public static FunctionPtr OperatorBinaryGeq = StandardErrorHandling<NumberValue>(
            expandArguments: NoArgExpansion,
            replaceBlankValues: ReplaceBlankWithZero,
            checkRuntimeTypes: ExactValueType<NumberValue>,
            checkRuntimeValues: FiniteChecker,
            returnBehavior: ReturnBehavior.AlwaysEvaluateAndReturnResult,
            targetFunction: NumericGeq
        );

        public static FunctionPtr OperatorBinaryLt = StandardErrorHandling<NumberValue>(
            expandArguments: NoArgExpansion,
            replaceBlankValues: ReplaceBlankWithZero,
            checkRuntimeTypes: ExactValueType<NumberValue>,
            checkRuntimeValues: FiniteChecker,
            returnBehavior: ReturnBehavior.AlwaysEvaluateAndReturnResult,
            targetFunction: NumericLt
        );

        public static FunctionPtr OperatorBinaryLeq = StandardErrorHandling<NumberValue>(
            expandArguments: NoArgExpansion,
            replaceBlankValues: ReplaceBlankWithZero,
            checkRuntimeTypes: ExactValueType<NumberValue>,
            checkRuntimeValues: FiniteChecker,
            returnBehavior: ReturnBehavior.AlwaysEvaluateAndReturnResult,
            targetFunction: NumericLeq
        );

        public static FunctionPtr OperatorBinaryEq = StandardErrorHandling<FormulaValue>(
            expandArguments: NoArgExpansion,
            replaceBlankValues: DoNotReplaceBlank,
            checkRuntimeTypes: DeferRuntimeTypeChecking,
            checkRuntimeValues: DeferRuntimeValueChecking,
            returnBehavior: ReturnBehavior.AlwaysEvaluateAndReturnResult,
            targetFunction: AreEqual
        );

        public static FunctionPtr OperatorBinaryNeq = StandardErrorHandling<FormulaValue>(
            expandArguments: NoArgExpansion,
            replaceBlankValues: DoNotReplaceBlank,
            checkRuntimeTypes: DeferRuntimeTypeChecking,
            checkRuntimeValues: DeferRuntimeValueChecking,
            returnBehavior: ReturnBehavior.AlwaysEvaluateAndReturnResult,
            targetFunction: NotEqual
        );

        public static FunctionPtr OperatorTextIn = StandardErrorHandling(
            expandArguments: NoArgExpansion,
            replaceBlankValues: DoNotReplaceBlank,
            checkRuntimeTypes: DeferRuntimeTypeChecking,
            checkRuntimeValues: DeferRuntimeValueChecking,
            returnBehavior: ReturnBehavior.AlwaysEvaluateAndReturnResult,
            targetFunction: StringInOperator(false)
        );

        public static FunctionPtr OperatorTextInExact = StandardErrorHandling(
            expandArguments: NoArgExpansion,
            replaceBlankValues: DoNotReplaceBlank,
            checkRuntimeTypes: DeferRuntimeTypeChecking,
            checkRuntimeValues: DeferRuntimeValueChecking,
            returnBehavior: ReturnBehavior.AlwaysEvaluateAndReturnResult,
            targetFunction: StringInOperator(true)
        );

        public static FunctionPtr OperatorScalarTableIn = StandardErrorHandling(
            expandArguments: NoArgExpansion,
            replaceBlankValues: DoNotReplaceBlank,
            checkRuntimeTypes: DeferRuntimeTypeChecking,
            checkRuntimeValues: DeferRuntimeValueChecking,
            returnBehavior: ReturnBehavior.AlwaysEvaluateAndReturnResult,
            targetFunction: InScalarTableOperator(false)
        );

        public static FunctionPtr OperatorScalarTableInExact = StandardErrorHandling(
            expandArguments: NoArgExpansion,
            replaceBlankValues: DoNotReplaceBlank,
            checkRuntimeTypes: DeferRuntimeTypeChecking,
            checkRuntimeValues: DeferRuntimeValueChecking,
            returnBehavior: ReturnBehavior.AlwaysEvaluateAndReturnResult,
            targetFunction: InScalarTableOperator(true)
        );
        #endregion

        private static NumberValue NumericAdd(IRContext irContext, NumberValue[] args)
        {
            var result = args[0].Value + args[1].Value;
            return new NumberValue(irContext, result);
        }

        private static NumberValue NumericMul(IRContext irContext, NumberValue[] args)
        {
            var result = args[0].Value * args[1].Value;
            return new NumberValue(irContext, result);
        }

        private static NumberValue NumericDiv(IRContext irContext, NumberValue[] args)
        {
            var result = args[0].Value / args[1].Value;
            return new NumberValue(irContext, result);
        }

        private static BooleanValue NumericGt(IRContext irContext, NumberValue[] args)
        {
            var result = args[0].Value > args[1].Value;
            return new BooleanValue(irContext, result);
        }

        private static BooleanValue NumericGeq(IRContext irContext, NumberValue[] args)
        {
            var result = args[0].Value >= args[1].Value;
            return new BooleanValue(irContext, result);
        }

        private static BooleanValue NumericLt(IRContext irContext, NumberValue[] args)
        {
            var result = args[0].Value < args[1].Value;
            return new BooleanValue(irContext, result);
        }

        private static BooleanValue NumericLeq(IRContext irContext, NumberValue[] args)
        {
            var result = args[0].Value <= args[1].Value;
            return new BooleanValue(irContext, result);
        }

        private static BooleanValue AreEqual(IRContext irContext, FormulaValue[] args)
        {
            FormulaValue arg1 = args[0];
            FormulaValue arg2 = args[1];
            return new BooleanValue(irContext, RuntimeHelpers.AreEqual(arg1, arg2));
        }

        private static BooleanValue NotEqual(IRContext irContext, FormulaValue[] args)
        {
            FormulaValue arg1 = args[0];
            FormulaValue arg2 = args[1];
            return new BooleanValue(irContext, !RuntimeHelpers.AreEqual(arg1, arg2));
        }

        // See in_SS in JScript membershipReplacementFunctions
        public static Func<IRContext, FormulaValue[], FormulaValue> StringInOperator(bool exact)
        {
            return (irContext, args) =>
            {
                var left = args[0];
                var right = args[1];
                if (left is BlankValue)
                {
                    return new BooleanValue(irContext, right is BlankValue);
                }
                if (right is BlankValue)
                {
                    return new BooleanValue(irContext, false);
                }
                var leftStr = (StringValue)left;
                var rightStr = (StringValue)right;

                if (exact)
                {
                    return new BooleanValue(irContext, rightStr.Value.IndexOf(leftStr.Value) >= 0);
                }
                return new BooleanValue(irContext, rightStr.Value.ToLowerInvariant().IndexOf(leftStr.Value.ToLowerInvariant()) >= 0);
            };
        }

        // Left is a scalar. Right is a single-column table.
        // See in_ST()
        public static Func<IRContext, FormulaValue[], FormulaValue> InScalarTableOperator(bool exact)
        {
            return (irContext, args) =>
            {
                var left = args[0];
                var right = args[1];

                if (!exact && left is StringValue strLhs)
                {
                    left = strLhs.ToLower();
                }

                TableValue source = (TableValue)right;

                foreach (var row in source.Rows)
                {
                    if (row.IsValue)
                    {
                        var rhs = row.Value.Fields.First().Value;

                        if (!exact && rhs is StringValue strRhs)
                        {
                            right = strRhs.ToLower();
                        }

                        if (RuntimeHelpers.AreEqual(left, rhs))
                        {
                            return new BooleanValue(irContext, true);
                        }
                    }
                }
                return new BooleanValue(irContext, false);
            };
        }
    }
}