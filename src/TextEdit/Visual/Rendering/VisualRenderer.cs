using Avalonia.Input;

namespace TextEdit.Visual
{
	public abstract class VisualRenderer : InputElement
	{
		/// <summary>
		/// Gets currently rendered <see cref="IVisualDocument"/>.
		/// </summary>
		public abstract IVisualDocument VisualDocument { get; set; }

		#region Selections

		/// <summary>
		/// Gets selections.
		/// </summary>
		public abstract IEnumerable<IVisualSelection> Selections { get; set; }

		#endregion
	}
}
