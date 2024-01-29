namespace TextEdit.Text
{
	/// <summary>
	/// Collection of common caret navigation commands
	/// </summary>
	public static class CaretCommands
	{
		private static readonly Func<ITextEditor, bool> canExecute = (editor) => editor.SelectionManager.Selections.Any();

		#region Basic navigation

		public static ITextEditorCommand CaretLogicalLineUp { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.LogicalLineUp(); }, canExecute: canExecute);

		public static ITextEditorCommand CaretLogicalLineDown { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.LogicalLineDown(); }, canExecute: canExecute);

		public static ITextEditorCommand CaretVisualLineUp { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.VisualLineUp(); }, canExecute: canExecute);

		public static ITextEditorCommand CaretVisualLineDown { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.VisualLineUp(); }, canExecute: canExecute);

		public static ITextEditorCommand CaretCharLeft { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.CharLeft(); }, canExecute: canExecute);

		public static ITextEditorCommand CaretCharRight { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.CharRight(); }, canExecute: canExecute);

		public static ITextEditorCommand CaretWordLeft { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.WordLeft(); }, canExecute: canExecute);

		public static ITextEditorCommand CaretWordRight { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.WordRight(); }, canExecute: canExecute);

		#endregion

		#region Line navigation

		public static ITextEditorCommand CaretToLogicalLineStart { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.LogicalLineStart(); }, canExecute: canExecute);

		public static ITextEditorCommand CaretToLogicalLineEnd { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.LogicalLineEnd(); }, canExecute: canExecute);

		public static ITextEditorCommand CaretToVisualLineStart { get; }
		   = new ActionTextEditorCommand((editor) => { editor.SelectionManager.VisualLineStart(); }, canExecute: canExecute);

		public static ITextEditorCommand CaretToVisualLineEnd { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.VisualLineEnd(); }, canExecute: canExecute);

		#endregion

		#region Document navigation

		public static ITextEditorCommand CaretToDocumentStart { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.DocumentStart(); }, canExecute: canExecute);

		public static ITextEditorCommand CaretToDocumentEnd { get; }
			= new ActionTextEditorCommand((editor) => { editor.SelectionManager.DocumentEnd(); }, canExecute: canExecute);

		#endregion
	}
}
