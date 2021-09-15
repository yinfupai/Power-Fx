//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Authoring.Texl;
using Microsoft.PowerFx.Core.IR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.PowerFx.Functions
{
    internal static partial class Library
    {
        // Char is used for PA string escaping 
        public static FormulaValue Char(IRContext irContext, NumberValue[] args)
        {
            var arg0 = args[0];
            var str = new string((char)arg0.Value, 1);
            return new StringValue(irContext, str);
        }

        public static FormulaValue Concat(EvalVisitor runner, SymbolContext symbolContext, IRContext irContext, FormulaValue[] args)
        {// Streaming 
            var arg0 = (TableValue)args[0];
            var arg1 = (LambdaFormulaValue)args[1];

            StringBuilder sb = new StringBuilder();

            foreach (var row in arg0.Rows)
            {
                if (row.IsValue)
                {
                    var childContext = symbolContext.WithScopeValues(row.Value);

                    // Filter evals to a boolean 
                    var result = arg1.Eval(runner, childContext);

                    var str = (StringValue)result;
                    sb.Append(str.Value);
                }
            }

            return new StringValue(irContext, sb.ToString());
        }

        // Scalar 
        // Operator & maps to this function call. 
        public static FormulaValue Concatenate(IRContext irContext, StringValue[] args)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var arg in args)
            {
                sb.Append(arg.Value);
            }

            return new StringValue(irContext, sb.ToString());
        }

        // https://docs.microsoft.com/en-us/powerapps/maker/canvas-apps/functions/function-value
        // Convert string to number
        public static FormulaValue Value(IRContext irContext, FormulaValue[] args)
        {
            var arg0 = args[0];

            if (arg0 is NumberValue n)
            {
                return n;
            }

            var str = ((StringValue)arg0).Value;

            if (string.IsNullOrEmpty(str))
            {
                return new BlankValue(irContext);
            }

            double div = 1;
            if (str[str.Length - 1] == '%')
            {
                str = str.Substring(0, str.Length - 1);
                div = 100;
            }

            if (!double.TryParse(str, NumberStyles.Any, new CultureInfo("en-US"), out var val))
            {
                return new BlankValue(irContext);
            }

            val /= div;

            return new NumberValue(irContext, val);
        }

        // https://docs.microsoft.com/en-us/powerapps/maker/canvas-apps/functions/function-text
        public static FormulaValue Text(IRContext irContext, FormulaValue[] args)
        {
            if (args.Length > 1)
            {
                throw new NotImplementedException("Text() doesn't support format args");
            }

            // $$$ combine with a ToString()? 
            // $$$ Should these be handled by coercion?
            string str = null;
            var arg0 = args[0];
            if (arg0 is NumberValue num)
            {
                str = num.Value.ToString();
            }
            else if (arg0 is StringValue s)
            {
                str = s.Value;
            }
            else if (arg0 is DateValue d)
            {
                // $$$ Use real format string
                str = d.Value.ToString("M/d/yyyy");
            }

            if (str != null)
            {
                return new StringValue(irContext, str);
            }

            throw new NotImplementedException($"Text format for {arg0.GetType().Name}");
        }

        // https://docs.microsoft.com/en-us/powerapps/maker/canvas-apps/functions/function-isblank-isempty
        // Take first non-blank value. 
        // 
        public static FormulaValue Coalesce(EvalVisitor runner, SymbolContext symbolContext, IRContext irContext, FormulaValue[] args)
        {
            var errors = new List<ErrorValue>();

            foreach (var arg in args)
            {
                var res = runner.EvalArg<ValidFormulaValue>(arg, symbolContext, arg.IRContext);

                if (res.IsValue)
                {
                    var val = res.Value;
                    if (!(val is StringValue str && str.Value == ""))
                    {
                        if (errors.Count == 0)
                            return res.ToFormulaValue();
                        else
                            return ErrorValue.Combine(irContext, errors);
                    }
                }
                if (res.IsError)
                {
                    errors.Add(res.Error);
                }
            }
            if (errors.Count == 0)
                return new BlankValue(irContext);
            else
                return ErrorValue.Combine(irContext, errors);
        }

        public static FormulaValue Lower(IRContext irContext, StringValue[] args)
        {
            return new StringValue(irContext, args[0].Value.ToLower());
        }

        public static FormulaValue Upper(IRContext irContext, StringValue[] args)
        {
            return new StringValue(irContext, args[0].Value.ToUpper());
        }

        // https://docs.microsoft.com/en-us/powerapps/maker/canvas-apps/functions/function-len
        public static FormulaValue Len(IRContext irContext, StringValue[] args)
        {
            return new NumberValue(irContext, args[0].Value.Length);
        }

        // https://docs.microsoft.com/en-us/powerapps/maker/canvas-apps/functions/function-left-mid-right
        public static FormulaValue Mid(IRContext irContext, FormulaValue[] args)
        {
            var errors = new List<ErrorValue>();
            NumberValue start = (NumberValue)args[1];
            if (double.IsNaN(start.Value) || double.IsInfinity(start.Value) || start.Value <= 0)
            {
                errors.Add(CommonErrors.ArgumentOutOfRange(start.IRContext));
            }

            NumberValue count = (NumberValue)args[2];
            if (double.IsNaN(count.Value) || double.IsInfinity(count.Value) || count.Value < 0)
            {
                errors.Add(CommonErrors.ArgumentOutOfRange(count.IRContext));
            }

            if (errors.Count != 0)
            {
                return ErrorValue.Combine(irContext, errors);
            }

            StringValue source = (StringValue)args[0];
            if (source.Value == "")
            {
                return new StringValue(irContext, "");
            }

            var start0Based = (int)(start.Value - 1);
            var minCount = System.Math.Min((int)count.Value, source.Value.Length - start0Based);
            var result = source.Value.Substring(start0Based, minCount);

            return new StringValue(irContext, result);
        }
    }
}
