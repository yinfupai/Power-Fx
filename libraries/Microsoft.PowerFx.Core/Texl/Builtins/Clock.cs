//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Microsoft.AppMagic.Authoring.Texl
{
    internal class ClockFunction : BuiltinFunction
    {
        public override bool IsSelfContained => true;

        public ClockFunction(string functionInvariantName, TexlStrings.StringGetter functionDescription)
            : base(new DPath().Append(new DName(LanguageConstants.InvariantClockNamespace)), functionInvariantName, functionDescription, FunctionCategories.DateTime, DType.CreateTable(new TypedName(DType.String, new DName("Value"))), 0, 0, 0)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new TexlStrings.StringGetter[] { };
        }
    }

    // Clock.AmPm()
    internal sealed class AmPmFunction : ClockFunction
    {
        public AmPmFunction()
            : base("AmPm", TexlStrings.AboutClock__AmPm)
        { }
    }

    // Clock.AmPmShort()
    internal sealed class AmPmShortFunction : ClockFunction
    {
        public AmPmShortFunction()
            : base("AmPmShort", TexlStrings.AboutClock__AmPmShort)
        { }
    }

    // Clock.IsClock24()
    internal sealed class IsClock24Function : BuiltinFunction
    {
        public override bool IsSelfContained => true;

        public IsClock24Function()
            : base(new DPath().Append(new DName(LanguageConstants.InvariantClockNamespace)), "IsClock24", TexlStrings.AboutClock__IsClock24, FunctionCategories.DateTime, DType.Boolean, 0, 0, 0)
        { }

        public override IEnumerable<TexlStrings.StringGetter[]> GetSignatures()
        {
            yield return new TexlStrings.StringGetter[] { };
        }
    }
}