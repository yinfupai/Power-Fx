//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.AppMagic.Authoring
{
    public class IntellisenseContext: IIntellisenseContext
    {
        /// <summary>
        /// The input string for intellisense.
        /// </summary>
        public string InputText { get; private set; }

        /// <summary>
        /// Cursor position for the intellisense input string.
        /// </summary>
        public int CursorPosition { get; private set; }


        public IntellisenseContext(string inputText, int cursorPosition)
        {
            Contracts.CheckValue(inputText, "inputText");
            Contracts.CheckParam(cursorPosition >= 0 && cursorPosition <= inputText.Length, "cursorPosition");

            InputText = inputText;
            CursorPosition = cursorPosition;
        }

        public void InsertTextAtCursorPos(string insertedText)
        {
            Contracts.AssertValue(insertedText);

            InputText = InputText.Substring(0, CursorPosition) + insertedText + InputText.Substring(CursorPosition);
            CursorPosition += insertedText.Length;
        }
    }
}
