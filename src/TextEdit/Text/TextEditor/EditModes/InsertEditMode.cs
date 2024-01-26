namespace TextEdit.Text
{
	internal sealed class InsertEditMode : AbstractEditMode
	{
		public override void Insert(TextEditor editor, char character)
		{
			var textDocument = editor.TextDocument;

			foreach (var caret in editor.Carets)
			{
				int offset = caret.Position.GetLastCharacterIndex();

				textDocument.Insert(offset, character);
			}
		}
	}
}
