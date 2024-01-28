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
		#region TextDocument

		public ITextDocument TextDocument { get; set; }

		#endregion

		#region LineMetrics

		public ILineMetrics LineMetrics { get; }

		#endregion

		#region WordWrap

		public bool WordWrap { get; set; }

		#endregion

		#region Caret

		public IEnumerable<ITextCaret> Carets { get; }

		#endregion

		#region Selection

		/// <summary>
		/// Gets selections.
		/// </summary>
		public ITextSelection? Selection { get; }

		/// <summary>
		/// Gets and sets selection mode
		/// </summary>
		public SelectionMode SelectionMode { get; set; }

		#endregion

		#region VirtualSpace

		/// <summary>
		/// Gets and sets whether selection in column mode can select as if after line endings were whitespaces
		/// </summary>
		public bool VirtualSpace { get; set; }

		#endregion

		#region EditMode

		public EditMode EditMode { get; set; }

		#endregion

		#region Undo

		public IUndoManager UndoManager { get; }

		#endregion

		#region HotKey

		public IHotKeyManager HotKeyManager { get; }

		#endregion

		#region Clipboard

		public IClipboard Clipboard { get; }

		#endregion
	}
}
