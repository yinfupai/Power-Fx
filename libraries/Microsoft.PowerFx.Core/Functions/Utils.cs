//------------------------------------------------------------------------------
// <copyright file="Utils.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.AppMagic.Authoring;
using Microsoft.AppMagic.Authoring.Texl;
using System.Globalization;

namespace Microsoft.AppMagic
{
    // Extensions, extracted from Utils 
    internal static partial class Utils2
    {
        public static bool TestBit(this BigInteger value, int bitIndex)
        {
            Contracts.Assert(bitIndex >= 0);

            return !(value & (BigInteger.One << bitIndex)).IsZero;
        }
    }
}