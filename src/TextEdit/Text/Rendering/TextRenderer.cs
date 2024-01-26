using Avalonia.Input;

namespace TextEdit.Text
{
	public abstract class TextRenderer : InputElement
	{
		public abstract ITextDocument TextDocument { get; set; }

		#region WordWrap

		public bool WordWrapEnabled { get; set; }

		#endregion

		#region Selections

		/// <summary>
		/// Gets selections.
		/// </summary>
		public abstract IEnumerable<ITextSelection> Selections { get; set; }

		#endregion
	}
}
