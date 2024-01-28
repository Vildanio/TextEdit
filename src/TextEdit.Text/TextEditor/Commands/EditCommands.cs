namespace TextEdit.Text
{
	/// <summary>
	/// Collection of common text editing commands
	/// </summary>
	public static class EditCommands
	{
		private static readonly Func<ITextEditor, bool> canExecute = (editor) =>
		{
			return !editor.TextDocument.IsReadOnly && editor.Carets.Any();
		};

		public static ITextEditorCommand Backspace { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				var selection = editor.Selection;

				if (selection is not null)
				{
					selection.Paste(string.Empty);
				}
				else
				{
					var textDocument = editor.TextDocument;

					foreach (var caret in editor.Carets)
					{
						var caretPosition = caret.Position;

						if (caretPosition.CharacterIndex > 0)
						{
							textDocument.RemoveAt(caretPosition.CharacterIndex);

							caret.CharLeft();
						}
					}
				}
			}, canExecute: canExecute);

		public static ITextEditorCommand Delete { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				var selection = editor.Selection;

				if (selection is not null)
				{
					selection.Paste(string.Empty);
				}
				else
				{
					var textDocument = editor.TextDocument;

					foreach (var caret in editor.Carets)
					{
						var caretPosition = caret.Position;

						if (caretPosition.CharacterIndex > 0)
						{
							textDocument.RemoveAt(caretPosition.CharacterIndex);
						}
					}
				}
			}, canExecute: canExecute);
	}
}
