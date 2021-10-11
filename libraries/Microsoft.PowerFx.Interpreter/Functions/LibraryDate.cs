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
        public static FormulaValue Today(EvalVisitor runner, SymbolContext symbolContext, IRContext irContext, FormulaValue[] args)
        {
            // $$$ timezone?
            var date = DateTime.Today;

            return new DateValue(irContext, date);
        }

        // https://docs.microsoft.com/en-us/powerapps/maker/canvas-apps/functions/function-now-today-istoday
        public static FormulaValue IsToday(IRContext irContext, DateValue[] args)
        {
            var arg = args[0];

            var now = DateTime.Today;
            bool same = (arg.Value.Year == now.Year) && (arg.Value.Month == now.Month) && (arg.Value.Day == now.Day);
            return new BooleanValue(irContext, same);
        }

        // https://docs.microsoft.com/en-us/powerapps/maker/canvas-apps/show-text-dates-times
        // https://docs.microsoft.com/en-us/powerapps/maker/canvas-apps/functions/function-dateadd-datediff
        public static FormulaValue DateAdd(IRContext irContext, FormulaValue[] args)
        {
            var datetime = (DateValue)args[0];
            var delta = (NumberValue)args[1];
            var units = (StringValue)args[2];

            switch (units.Value.ToLower())
            {
                case "days":
                    var newDate = datetime.Value.AddDays(delta.Value);
                    return new DateValue(irContext, newDate);
                default:
                    // TODO: Task 10723372: Implement Unit Functionality in DateAdd, DateDiff Functions
                    throw new NotImplementedException("DateAdd Only supports Days for the unit field");
            }
        }

        public static FormulaValue DateDiff(IRContext irContext, FormulaValue[] args)
        {
            var start = (DateValue)args[0];
            var end = (DateValue)args[1];
            var units = (StringValue)args[2];

            TimeSpan diff = end.Value - start.Value;

            // The function DateDiff only returns a whole number of the units being subtracted, and the precision is given in the unit specified.
            switch (units.Value.ToLower())
            {
                case "days":
                    double days = Math.floor(diff.TotalDays);
                    return new NumberValue(irContext, days);
                default:
                    // TODO: Task 10723372: Implement Unit Functionality in DateAdd, DateDiff Functions
                    throw new NotImplementedException("DateDiff Only supports Days for the unit field");
            }
        }

        // https://docs.microsoft.com/en-us/powerapps/maker/canvas-apps/functions/function-datetime-parts
        public static FormulaValue Year(IRContext irContext, FormulaValue[] args)
        {
            if (args[0] is BlankValue)
            {
                // TODO: Standardize the number 0 - year 1900 logic
                return new NumberValue(irContext, 1900);
            }

            var arg0 = (DateValue)args[0];
            var x = arg0.Value.Year;
            return new NumberValue(irContext, x);
        }

        public static FormulaValue Day(IRContext irContext, FormulaValue[] args)
        {
            if (args[0] is BlankValue)
            {
                return new NumberValue(irContext, 0);
            }

            var arg0 = (DateValue)args[0];
            var x = arg0.Value.Day;
            return new NumberValue(irContext, x);
        }

        public static FormulaValue Month(IRContext irContext, FormulaValue[] args)
        {
            if (args[0] is BlankValue)
            {
                return new NumberValue(irContext, 1);
            }

            var arg0 = (DateValue)args[0];
            var x = arg0.Value.Month;
            return new NumberValue(irContext, x);
        }

        // https://docs.microsoft.com/en-us/powerapps/maker/canvas-apps/functions/function-date-time
        // Date(Year,Month,Day)
        public static FormulaValue Date(IRContext irContext, NumberValue[] args)
        {
            // $$$ fix impl
            var year = (int)args[0].Value;
            var month = (int)args[1].Value;
            var day = (int)args[2].Value;

            return new DateValue(irContext, new DateTime(year, month, day));
        }

        public static FormulaValue DateValue(IRContext irContext, StringValue[] args)
        {
            var stringInput = args[0].Value;
            var inputDateTime = Convert.ToDateTime(stringInput, System.Threading.Thread.CurrentThread.CurrentCulture);

            return new DateValue(irContext, inputDateTime);
        }
    }
}
