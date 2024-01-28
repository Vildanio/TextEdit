using TextEdit.Utils;

namespace TextEdit.Text
{
	/// <summary>
	/// Collection of common caret navigation commands
	/// </summary>
	public static class CaretCommands
	{
		private static readonly Func<ITextEditor, bool> canExecute = (editor) => editor.Carets.Any();

		#region Basic navigation

		public static ITextEditorCommand CaretLogicalLineUp { get; }
			= new ActionTextEditorCommand((editor) => { editor.Carets.ForEach(x => x.LogicalLineUp()); }, canExecute: canExecute);

		public static ITextEditorCommand CaretLogicalLineDown { get; }
			= new ActionTextEditorCommand((editor) => { editor.Carets.ForEach(x => x.LogicalLineDown()); }, canExecute: canExecute);

		public static ITextEditorCommand CaretVisualLineUp { get; }
			= new ActionTextEditorCommand((editor) => { editor.Carets.ForEach(x => x.VisualLineUp()); }, canExecute: canExecute);

		public static ITextEditorCommand CaretVisualLineDown { get; }
			= new ActionTextEditorCommand((editor) => { editor.Carets.ForEach(x => x.VisualLineUp()); }, canExecute: canExecute);

		public static ITextEditorCommand CaretCharLeft { get; }
			= new ActionTextEditorCommand((editor) => { editor.Carets.ForEach(x => x.CharLeft()); }, canExecute: canExecute);

		public static ITextEditorCommand CaretCharRight { get; }
			= new ActionTextEditorCommand((editor) => { editor.Carets.ForEach(x => x.CharRight()); }, canExecute: canExecute);

		public static ITextEditorCommand CaretWordLeft { get; }
			= new ActionTextEditorCommand((editor) => { editor.Carets.ForEach(x => x.WordLeft()); }, canExecute: canExecute);

		public static ITextEditorCommand CaretWordRight { get; }
			= new ActionTextEditorCommand((editor) => { editor.Carets.ForEach(x => x.WordRight()); }, canExecute: canExecute);

		#endregion

		#region Line navigation

		public static ITextEditorCommand CaretToLogicalLineStart { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				foreach (var caret in editor.Carets)
				{
					caret.LogicalLineStart();
				}
			}, canExecute: canExecute);

		public static ITextEditorCommand CaretToLogicalLineEnd { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				foreach (var caret in editor.Carets)
				{
					caret.LogicalLineEnd();
				}
			}, canExecute: canExecute);

		public static ITextEditorCommand CaretToVisualLineStart { get; }
		   = new ActionTextEditorCommand((editor) =>
		   {
			   foreach (var caret in editor.Carets)
			   {
				   caret.VisualLineStart();
			   }
		   }, canExecute: canExecute);

		public static ITextEditorCommand CaretToVisualLineEnd { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				foreach (var caret in editor.Carets)
				{
					caret.VisualLineEnd();
				}
			}, canExecute: canExecute);

		#endregion

		#region Document navigation

		public static ITextEditorCommand CaretToDocumentStart { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				foreach (var caret in editor.Carets)
				{
					caret.DocumentStart();
				}
			}, canExecute: canExecute);

		public static ITextEditorCommand CaretToDocumentEnd { get; }
			= new ActionTextEditorCommand((editor) =>
			{
				foreach (var caret in editor.Carets)
				{
					caret.DocumentEnd();
				}
			}, canExecute: canExecute);

		#endregion
	}
}
