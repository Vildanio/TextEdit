using Avalonia.Controls;
using TextEdit.Line.Document;

namespace TextEdit.Text
{
	/// <summary>
	/// <see cref="Control"/> for rendering and editing <see cref="ITextDocument"/>
	/// </summary>
	public abstract class AbstractTextEditor : Control, ITextEditor
	{
		protected abstract AbstractTextRenderer TextRenderer { get; }

		public ITextDocument TextDocument
		{
			get
			{
				return TextRenderer.TextDocument;
			}
			set
			{
				TextRenderer.TextDocument = value;
			}
		}

		public ILineMetrics LineMetrics => TextRenderer.LineMetrics;

		public bool WordWrap
		{
			get
			{
				return TextRenderer.WordWrap;
			}
			set
			{
				TextRenderer.WordWrap = value;
			}
		}

		public IEnumerable<ITextCaret> Carets => TextRenderer.Carets;

		/// <summary>
		/// Gets selections.
		/// </summary>
		public ITextSelection? Selection => TextRenderer.Selection;

		public abstract EditMode EditMode { get; set; }

		public abstract IUndoManager UndoManager { get; }

		public abstract IHotKeyManager HotKeyManager { get; }

		public abstract IClipboard Clipboard { get; }
		public abstract SelectionMode SelectionMode { get; set; }
		public abstract bool VirtualSpace { get; set; }
	}
}
