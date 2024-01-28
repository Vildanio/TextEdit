using Avalonia.Input;
using TextEdit.Line.Document;

namespace TextEdit.Text
{
	public abstract class AbstractTextRenderer : InputElement
	{
		public abstract ITextDocument TextDocument { get; set; }

		/// <summary>
		/// Gets <see cref="ILineMetrics"/> used for text rendering
		/// </summary>
		public abstract ILineMetrics LineMetrics { get; }

		#region WordWrap

		public bool WordWrap { get; set; }

		#endregion

		#region Carets

		/// <summary>
		/// Gets carets.
		/// </summary>
		public abstract IEnumerable<ITextCaret> Carets { get; }

		#endregion

		#region Selections

		/// <summary>
		/// Gets selection.
		/// </summary>
		public abstract ITextSelection? Selection { get; }

		#endregion
	}
}
