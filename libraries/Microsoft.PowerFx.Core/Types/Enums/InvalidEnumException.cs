//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Microsoft.AppMagic.Authoring
{
    internal class InvalidEnumException : Exception
    {
        public InvalidEnumException(string message) : base(message)
        {
        }
    }
}