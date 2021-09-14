//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------


using Microsoft.AppMagic;
using Microsoft.AppMagic.Authoring.Texl;
using Microsoft.PowerFx.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Microsoft.PowerFx.Functions
{
    // Wrappers that match JS functions -
    // these should have exact signature and casing as the TypeScript code to aide in porting JS implementations over C#. 
    internal class Math
    {
        public static double ceil(double d)
        {
            return System.Math.Ceiling(d);
        }
        public static double floor(double d)
        {
            return System.Math.Floor(d);
        }

        public static double pow(double x, double y)
        {
            return System.Math.Pow(x, y);
        }
    }

    internal static class DebugContracts
    {
        public static void assert(bool test)
        {
            Contracts.Assert(test);
        }
    }

    // Helpers 
    // Many of these help port from JScript. 
    internal static partial class Library
    {
        private interface IErrorContext { } // For porting

        private static FormulaValue GetAt(FormulaValue[] args, int idx)
        {
            return args[idx];
        }
        private static FormulaValue GetAtOrDefault(FormulaValue[] args, int idx, FormulaValue defaultValue)
        {
            if (idx >= args.Length)
            {
                return defaultValue;
            }
            return args[idx];
        }

        private static bool isTypeNumber(FormulaValue value)
        {
            return value is NumberValue;
        }

        private static bool isBlankValue(FormulaValue value)
        {
            // Jscript impl: checks against Null. 
            return value is BlankValue;
        }
        private static bool isFinite(double value)
        {
            return !double.IsInfinity(value);
        }

        public static DateTime asDateTime(FormulaValue[] args, int idx)
        {
            var arg = args[idx];
            return ((DateValue)arg).Value.DateTime;
        }

        public static int asIntegerTruncate(FormulaValue value)
        {
            var num = asNumber(value);
            return (int)num;
        }

        public static double asNumber(FormulaValue[] args, int idx)
        {
            return asNumber(args[idx]);
        }

        public static double asNumber(FormulaValue[] args, int idx, double defaultValue)
        {
            if (idx >= args.Length)
            {
                return defaultValue;
            }
            return asNumber(args[idx]);
        }

        // throw on error
        public static double asNumber(FormulaValue value)
        {
            if (value is NumberValue n)
            {
                return n.Value;
            }
            if (value is BlankValue)
            {
                return 0;
            }

            // Binder should have caught 
            throw new InvalidOperationException($"value should be a number");
        }

        public static string asString(FormulaValue value)
        {
            if (value is StringValue str)
            {
                return str.Value;
            }
            throw new InvalidOperationException($"value should be a string");
        }

        private static bool tryEquals(FormulaValue numberValue, double val)
        {
            if (numberValue is NumberValue n)
            {
                return n.Value == val;
            }
            return false;
        }

        private static Exception createInternalTypeMismatchNumberError(IErrorContext errorContext, string func, string arg)
        {
            return new InvalidOperationException($"createInternalTypeMismatchNumberError {func} {arg}");
        }

        private static Exception createInvalidNullArgumentError(IErrorContext errorContext, string arg1, int n)
        {
            return new InvalidOperationException($"createInvalidNullArgumentError {arg1} at {n}");
        }

        private static Exception createIcreateInternalTypeMismatchNumberErrornvalidNullArgumentError(IErrorContext errorContext, string arg1, int n)
        {
            return new InvalidOperationException($"createInvalidNullArgumentError {arg1} at {n}");
        }

        private static Exception createOutOfRangeArgumentError(IErrorContext errorContext, string arg1, int a, int b)
        {
            return new InvalidOperationException($"createOutOfRangeArgumentError {arg1} at {a}, {b}");
        }

    }
}