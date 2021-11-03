﻿//------------------------------------------------------------------------------
// <copyright file="Utils.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Numerics;
using Microsoft.PowerFx.Core.Utils;

namespace Microsoft.PowerFx.Core.Functions
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