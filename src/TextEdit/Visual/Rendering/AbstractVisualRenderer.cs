using Avalonia.Input;

namespace TextEdit.Visual
{
	public abstract class AbstractVisualRenderer : InputElement
	{
		/// <summary>
		/// Gets currently rendered <see cref="IVisualDocument"/>.
		/// </summary>
		public abstract IVisualDocument VisualDocument { get; set; }
	}
}
