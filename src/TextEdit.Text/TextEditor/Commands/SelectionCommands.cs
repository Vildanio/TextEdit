namespace TextEdit.Text
{
	/// <summary>
	/// Collection of common selection managing commands
	/// </summary>
	public static class SelectionCommands
	{
		private static readonly Func<ITextEditor, bool> canExecute = (editor) => editor.Selection is not null;

		#region Basic navigation

		public static ITextEditorCommand SelectLogicalLineUp { get; }
			= new ActionTextEditorCommand((editor) => { editor.Selection?.LogicalLineUp(); }, canExecute: canExecute);

		public static ITextEditorCommand SelectLogicalLineDown { get; }
			= new ActionTextEditorCommand((editor) => { editor.Selection?.LogicalLineDown(); }, canExecute: canExecute);

		public static ITextEditorCommand SelectVisualLineUp { get; }
			= new ActionTextEditorCommand((editor) => { editor.Selection?.VisualLineUp(); }, canExecute: canExecute);

		public static ITextEditorCommand SelectVisualLineDown { get; }
			= new ActionTextEditorCommand((editor) => { editor.Selection?.VisualLineUp(); }, canExecute: canExecute);

		public static ITextEditorCommand SelectCharLeft { get; }
			= new ActionTextEditorCommand((editor) => { editor.Selection?.CharLeft(); }, canExecute: canExecute);

		public static ITextEditorCommand SelectCharRight { get; }
			= new ActionTextEditorCommand((editor) => { editor.Selection?.CharRight(); }, canExecute: canExecute);

		public static ITextEditorCommand SelectWordLeft { get; }
			= new ActionTextEditorCommand((editor) => { editor.Selection?.WordLeft(); }, canExecute: canExecute);

		public static ITextEditorCommand SelectWordRight { get; }
			= new ActionTextEditorCommand((editor) => { editor.Selection?.WordRight(); }, canExecute: canExecute);

		#endregion

		#region Line navigation

		public static ITextEditorCommand SelectToLogicalLineStart { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				editor.Selection?.SelectLogicalLineStart();
			}, canExecute: canExecute);

		public static ITextEditorCommand SelectToLogicalLineEnd { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				editor.Selection?.SelectLogicalLineEnd();
			}, canExecute: canExecute);


		public static ITextEditorCommand SelectToVisualLineStart { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				editor.Selection?.SelectVisualLineStart();
			}, canExecute: canExecute);

		public static ITextEditorCommand SelectToVisualLineEnd { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				editor.Selection?.SelectVisualLineEnd();
			}, canExecute: canExecute);

		#endregion

		#region Document navigation

		// There are noy ITextSelection.SelectToStart or SelectToEnd
		// because behaviour should be always the same despite the column selection mode or count of selections.

		public static ITextEditorCommand SelectToDocumentStart { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				editor.Selection?.SelectDocumentStart();
			}, canExecute: canExecute);

		public static ITextEditorCommand SelectToDocumentEnd { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				editor.Selection?.SelectDocumentEnd();
			});

		#endregion
	}
}
