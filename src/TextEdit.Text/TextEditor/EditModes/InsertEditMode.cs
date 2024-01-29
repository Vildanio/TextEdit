namespace TextEdit.Text
{
	internal sealed class InsertEditMode : AbstractEditMode
	{
		public override void Insert(AbstractTextEditor editor, char character)
		{
			var textDocument = editor.TextDocument;

			foreach (var caretPosition in editor.SelectionManager.EnumerateCarets())
			{
				textDocument.Insert(caretPosition.CharacterIndex, character);
			}
		}
	}
}
