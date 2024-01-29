namespace TextEdit.Text
{
	/// <summary>
	/// Collection of common selection managing commands
	/// </summary>
	public static class SelectionCommands
	{
		private static readonly Func<ITextEditor, bool> canExecute = (editor) => editor.SelectionManager.Selections.Any();

		#region Basic navigation

		public static ITextEditorCommand SelectLogicalLineUp { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.SelectLogicalLineUp(); }, canExecute: canExecute);

		public static ITextEditorCommand SelectLogicalLineDown { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.SelectLogicalLineDown(); }, canExecute: canExecute);

		public static ITextEditorCommand SelectVisualLineUp { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.SelectVisualLineUp(); }, canExecute: canExecute);

		public static ITextEditorCommand SelectVisualLineDown { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.SelectVisualLineUp(); }, canExecute: canExecute);

		public static ITextEditorCommand SelectCharLeft { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.SelectCharLeft(); }, canExecute: canExecute);

		public static ITextEditorCommand SelectCharRight { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.SelectCharRight(); }, canExecute: canExecute);

		public static ITextEditorCommand SelectWordLeft { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.SelectWordLeft(); }, canExecute: canExecute);

		public static ITextEditorCommand SelectWordRight { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.SelectWordRight(); }, canExecute: canExecute);

		#endregion

		#region Selection mode

		public static ITextEditorCommand SetPlainSelectionMode { get; }
			= new ActionTextEditorCommand((editor) => editor.SelectionMode = SelectionMode.Plain);

		public static ITextEditorCommand SetColumnSelectionMode { get; }
			= new ActionTextEditorCommand((editor) => editor.SelectionMode = SelectionMode.Column);

		#endregion

		#region Line navigation

		public static ITextEditorCommand SelectToLogicalLineStart { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				editor.SelectionManager.SelectLogicalLineStart();
			}, canExecute: canExecute);

		public static ITextEditorCommand SelectToLogicalLineEnd { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				editor.SelectionManager.SelectLogicalLineEnd();
			}, canExecute: canExecute);


		public static ITextEditorCommand SelectToVisualLineStart { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				editor.SelectionManager.SelectVisualLineStart();
			}, canExecute: canExecute);

		public static ITextEditorCommand SelectToVisualLineEnd { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				editor.SelectionManager.SelectVisualLineEnd();
			}, canExecute: canExecute);

		#endregion

		#region Document navigation

		public static ITextEditorCommand SelectToDocumentStart { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				editor.SelectionManager.SelectDocumentStart();
			}, canExecute: canExecute);

		public static ITextEditorCommand SelectToDocumentEnd { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				editor.SelectionManager.SelectDocumentEnd();
			});

		#endregion
	}
}
