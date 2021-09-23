//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.PowerFx.Core.Tests
{

    // Base class for running a lightweght test. 
    public abstract class BaseRunner
    {
        abstract public Task<FormulaValue> RunAsync(string expr);

        // Get the friendly name of the harness. 
        public virtual string GetName()
        {
            return GetType().Name;
        }

        public virtual bool IsError(FormulaValue value)
        {
            return value is ErrorValue;
        }
    }
}
