namespace TextEdit.Text
{
	internal sealed class OverstrikeEditMode : AbstractEditMode
	{
		public override void Insert(AbstractTextEditor editor, char character)
		{
			var textDocument = editor.TextDocument;
			var selection = editor.SelectionManager;

			foreach (var caretPosition in selection.EnumerateCarets())
			{
				int offset = caretPosition.CharacterIndex;

				// When caret before line ending or at the end of text
				if (offset == textDocument.Count || IsBeforeLineEnding(textDocument, offset))
				{
					textDocument.Insert(offset, character);
				}
				else
				{
					// Replace current character
					textDocument[offset] = character;
				}
			}

			// ### NEEDS_CHECK
			selection.CharRight();
		}

		private static bool IsBeforeLineEnding(ITextDocument textDocument, int offset)
		{
			return textDocument[offset].IsLineTerminator();
		}
	}
}
