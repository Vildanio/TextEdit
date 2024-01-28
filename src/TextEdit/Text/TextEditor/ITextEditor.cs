using TextEdit.Line.Document;

namespace TextEdit.Text
{
	// Text editor should be able to support collaborative editing
	// The following intreface represents proxy to "text editing session" of ONE user
	// There are may be many "sessions" in text editor, but commands and other stuff have access to only the current session

	/// <summary>
	/// Provides access to text editor
	/// </summary>
	public interface ITextEditor
	{
		public ITextDocument TextDocument { get; set; }

		public ILineMetrics LineMetrics { get; }

		public IEnumerable<ITextCaret> Carets { get; }

		/// <summary>
		/// Gets selections.
		/// </summary>
		public ITextSelection? Selection { get; }

		/// <summary>
		/// Gets and sets selection mode
		/// </summary>
		public SelectionMode SelectionMode { get; set; }

		public EditMode EditMode { get; set; }

		public IUndoManager UndoManager { get; }

		public IHotKeyManager HotKeyManager { get; }

		public IClipboard Clipboard { get; }

		#region Options

		public bool WordWrap { get; set; }

		public bool TextDragDrop { get; set; }

		public bool ScrollBelowDocument { get; set; }

		/// <summary>
		/// Gets and sets whether selection in column mode can select as if after line endings were whitespaces
		/// </summary>
		public bool VirtualSpace { get; set; }

		#endregion
	}
}
