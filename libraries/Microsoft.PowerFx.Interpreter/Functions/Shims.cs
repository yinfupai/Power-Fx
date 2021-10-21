//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------


using System;
using Microsoft.PowerFx.Core.Public.Values;
using Microsoft.PowerFx.Core.Utils;

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

        private static bool isBlankValue(FormulaValue value)
        {
            // Jscript impl: checks against Null. 
            return value is BlankValue;
        }
        private static bool isFinite(double value)
        {
            return !double.IsInfinity(value);
        }
    }
}