namespace TextEdit.Text
{
	public static class UndoCommands
	{
		public static ITextEditorCommand Undo { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				editor.UndoManager.Undo();
			}, canExecute: (editor) => editor.UndoManager.CanUndo());

		public static ITextEditorCommand Redo { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				editor.UndoManager.Undo();
			}, canExecute: (editor) => editor.UndoManager.CanRedo());
	}
}
