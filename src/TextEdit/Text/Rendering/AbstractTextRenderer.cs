using Avalonia.Input;
using TextEdit.Line;

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

		#region Selections

		/// <summary>
		/// Gets selection manager.
		/// </summary>
		public abstract ITextSelectionManager SelectionManager { get; }

		#endregion
	}
}
