using TextEdit.Text.Document.Unicode;

namespace TextEdit.Text
{
	internal sealed class OverstrikeEditMode : AbstractEditMode
	{
		public override void Insert(TextEditor editor, char character)
		{
			var textDocument = editor.TextDocument;

			foreach (var caret in editor.Carets)
			{
				int offset = caret.Position.GetLastCharacterIndex();

				// When caret before line ending or at the end of text
				if (offset == textDocument.Count || IsBeforeLineEnding(textDocument, offset))
				{
					textDocument.Insert(offset, character);
				}
				else
				{
					// Replace current character
					textDocument[offset] = character;

					// Move caret to the next character
					caret.CharRight();
				}
			}
		}

		private static bool IsBeforeLineEnding(ITextDocument textDocument, int offset)
		{
			return textDocument[offset].IsLineTerminator();
		}
	}
}
