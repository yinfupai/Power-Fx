//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Microsoft.PowerFx.Core.Types.Enums
{
    internal class InvalidEnumException : Exception
    {
        public InvalidEnumException(string message) : base(message)
        {
        }
    }
}