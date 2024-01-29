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
				var selection = editor.SelectionManager;

				if (selection.Selections.Any())
				{
					string text = selection.Cut();

					editor.Clipboard.SetTextAsync(text);
				}
			}, canExecute: (editor) => !editor.SelectionManager.IsEmpty() && !editor.TextDocument.IsReadOnly);

		public static ITextEditorCommand Copy { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				var selection = editor.SelectionManager;

				if (selection.Selections.Any())
				{
					string text = selection.Copy();

					editor.Clipboard.SetTextAsync(text);
				}
			}, canExecute: (editor) => !editor.SelectionManager.IsEmpty());

		public static ITextEditorCommand Paste { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				string? text = editor.Clipboard.GetTextAsync().Result;

				if (text is not null)
				{
					editor.SelectionManager.Paste(text);
				}
			}, canExecute: (editor) => !editor.TextDocument.IsReadOnly && !editor.SelectionManager.IsEmpty() || editor.SelectionManager.Selections.Any());
	}
}
