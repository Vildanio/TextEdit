using TextEdit.Text.Utils;

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

		#region Char

		public static ITextEditorCommand CharBackspace { get; }
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

		public static ITextEditorCommand CharDelete { get; }
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

		public static ITextEditorCommand WordBackspace { get; }
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
						var caretIndex = caret.Position.CharacterIndex;

						if (caretIndex > 0)
						{
							int newCaretPosition = WordBoundsUtils.GetWordLeft(textDocument, caretIndex);

							textDocument.RemoveRange(newCaretPosition, caretIndex - newCaretPosition);

							caret.Position = new TextHit(newCaretPosition);
						}
					}
				}
			}, canExecute: canExecute);

		public static ITextEditorCommand WordDelete { get; }
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
						var caretIndex = caret.Position.CharacterIndex;

						if (caretIndex > 0)
						{
							int newCaretPosition = WordBoundsUtils.GetWordRight(textDocument, caretIndex);

							textDocument.RemoveRange(newCaretPosition, newCaretPosition - caretIndex);

							caret.Position = new TextHit(newCaretPosition);
						}
					}
				}
			}, canExecute: canExecute);

		#endregion

		#region Edit mode

		public static ITextEditorCommand SwitchEditMode { get; }
			= new ActionTextEditorCommand((editor) => editor.EditMode = editor.EditMode == EditMode.Insert ? EditMode.Overstrike : EditMode.Insert);

		public static ITextEditorCommand SetInsertMode { get; }
			= new ActionTextEditorCommand((editor) => editor.EditMode = EditMode.Insert);

		public static ITextEditorCommand SetOverstrikeMode { get; }
			= new ActionTextEditorCommand((editor) => editor.EditMode = EditMode.Overstrike);

		#endregion
	}
}
