//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AppMagic.Transport;

namespace Microsoft.PowerFx.Core.Texl.Intellisense
{
    [TransportType(TransportKind.ByValue)]
    public interface IIntellisenseContext
    {
        /// <summary>
        /// The input string for intellisense.
        /// </summary>
        public string InputText { get; }

        /// <summary>
        /// Cursor position for the intellisense input string.
        /// </summary>
        public int CursorPosition { get; }
    }
}
