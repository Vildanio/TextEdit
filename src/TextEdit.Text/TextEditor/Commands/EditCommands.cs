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
			return !editor.TextDocument.IsReadOnly && editor.SelectionManager.Selections.Any();
		};

		#region Char

		public static ITextEditorCommand CharDelete { get; }
			= new ActionTextEditorCommand(DoCharDelete, canExecute: canExecute);

		public static ITextEditorCommand CharBackspace { get; }
			= new ActionTextEditorCommand(DoCharBackspace, canExecute: canExecute);

		public static ITextEditorCommand WordDelete { get; }
			= new ActionTextEditorCommand(DoWordDelete, canExecute: canExecute);

		public static ITextEditorCommand WordBackspace { get; }
			= new ActionTextEditorCommand(DoWordBackspace, canExecute: canExecute);

		public static void DoCharDelete(ITextEditor textEditor)
		{
			var selection = textEditor.SelectionManager;

			if (selection.Selections.Any())
			{
				selection.Paste(string.Empty);
			}
			else
			{
				var textDocument = textEditor.TextDocument;

				foreach (var caret in selection.EnumerateCarets())
				{
					var caretPosition = caret;

					if (caretPosition.CharacterIndex > 0)
					{
						textDocument.RemoveAt(caretPosition.CharacterIndex);
					}
				}
			}
		}

		public static void DoWordDelete(ITextEditor textEditor)
		{
			var selection = textEditor.SelectionManager;

			if (selection.Selections.Any())
			{
				selection.Paste(string.Empty);
			}
			else
			{
				var textDocument = textEditor.TextDocument;

				foreach (var caret in selection.EnumerateCarets())
				{
					var caretIndex = caret.CharacterIndex;

					if (caretIndex > 0)
					{
						int newCaretPosition = WordBoundsUtils.GetWordRight(textDocument, caretIndex);

						textDocument.RemoveRange(newCaretPosition, newCaretPosition - caretIndex);
					}
				}

				// ### NEEDS_TEST
				// Maybe moving should be performed before removing
				selection.WordLeft();
			}
		}

		public static void DoCharBackspace(ITextEditor textEditor)
		{
			var selection = textEditor.SelectionManager;

			if (selection.Selections.Any())
			{
				selection.Paste(string.Empty);
			}
			else
			{
				var textDocument = textEditor.TextDocument;

				foreach (var caret in selection.EnumerateCarets())
				{
					var caretPosition = caret;

					if (caretPosition.CharacterIndex > 0)
					{
						textDocument.RemoveAt(caretPosition.CharacterIndex);
					}
				}

				// ### NEEDS_TEST
				// Maybe moving should be performed before removing
				selection.CharLeft();
			}
		}

		public static void DoWordBackspace(ITextEditor textEditor)
		{
			var selection = textEditor.SelectionManager;

			if (selection.Selections.Any())
			{
				selection.Paste(string.Empty);
			}
			else
			{
				var textDocument = textEditor.TextDocument;

				foreach (var caret in selection.EnumerateCarets())
				{
					var caretIndex = caret.CharacterIndex;

					if (caretIndex > 0)
					{
						int newCaretPosition = WordBoundsUtils.GetWordLeft(textDocument, caretIndex);

						textDocument.RemoveRange(newCaretPosition, caretIndex - newCaretPosition);
					}
				}

				// ### NEEDS_TEST
				// Maybe moving should be performed before removing
			}
		}

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
