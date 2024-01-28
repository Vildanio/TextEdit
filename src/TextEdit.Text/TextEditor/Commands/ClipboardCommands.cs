using TextEdit.Text.Rendering;

namespace TextEdit.Text
{
	/// <summary>
	/// Collection of clipboard related text editor commands
	/// </summary>
	public static class ClipboardCommands
	{
		public static ITextEditorCommand Cut { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				var selection = editor.Selection;

				if (selection is not null)
				{
					string text = selection.Cut();

					editor.Clipboard.SetTextAsync(text);
				}
			}, canExecute: (editor) => !editor.Selection.IsNullOrEmpty() && !editor.TextDocument.IsReadOnly);

		public static ITextEditorCommand Copy { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				var selection = editor.Selection;

				if (selection is not null)
				{
					string text = selection.Copy();

					editor.Clipboard.SetTextAsync(text);
				}
			}, canExecute: (editor) => !editor.Selection.IsNullOrEmpty());

		public static ITextEditorCommand Paste { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				string? text = editor.Clipboard.GetTextAsync().Result;

				if (text is not null)
				{
					var selection = editor.Selection;

					if (selection is not null)
					{
						selection.Paste(text);
					}
					else
					{
						foreach (var caret in editor.Carets)
						{
							caret.Paste(text);
						}
					}
				}
			}, canExecute: (editor) => !editor.TextDocument.IsReadOnly && !editor.Selection.IsNullOrEmpty() || editor.Carets.Any());
	}
}
