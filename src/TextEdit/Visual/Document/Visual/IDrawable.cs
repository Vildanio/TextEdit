using Avalonia;
using Avalonia.Media;

namespace TextEdit.Visual
{
	/// <summary>
	/// Provides <see cref="Draw(DrawingContext, Point)"/> method
	/// </summary>
	public interface IDrawable
	{
		/// <summary>
		/// Draws something to the specified drawing context
		/// </summary>
		/// <param name="drawingContext"></param>
		public void Draw(DrawingContext drawingContext, Point point);
	}
}
