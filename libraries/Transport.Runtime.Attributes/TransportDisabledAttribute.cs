//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Microsoft.AppMagic.Transport
{
    /// <summary>
    /// If present, indicates that transport should ignore the property, field, or method to which this attribute is applied
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, Inherited = true)]
    public class TransportDisabledAttribute : Attribute
    {
        public TransportDisabledAttribute()
        {
        }
    }
}
