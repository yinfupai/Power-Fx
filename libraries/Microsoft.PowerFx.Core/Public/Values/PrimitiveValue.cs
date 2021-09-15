//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core.IR;
using System.Diagnostics.Contracts;
using System.Text.Json;

namespace Microsoft.PowerFx
{ 
    /// <summary>
    /// Helper for non-aggregate values that are represented as a single .net object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PrimitiveValue<T> : ValidFormulaValue
    {
        protected readonly T _value;

        public T Value => _value;

        internal PrimitiveValue(IRContext irContext, T value) : base(irContext)
        {
            Contract.Assert(value != null);

            this._value = value;
        }

        public override object ToObject()
        {
            return _value;
        }
    }
}