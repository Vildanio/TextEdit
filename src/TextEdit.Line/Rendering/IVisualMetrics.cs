using Avalonia;

namespace TextEdit.Line
{
	public interface IVisualMetrics
	{
		public int GetVisual(double y, out double lineTop);
		
		#region GetLineExtent

		public double GetWidth(int index);

		public double GetHeight(int index);

		public Size GetExtent(int index);

		#endregion

		#region GetLinesExtent

		// The renderer should be able to get extent of not-rendered text, to support
		// virtualization and simultaneously correct scrolling extent.
		// We need extent of the text in pixels, because other scroll units are unreliable.
		// For example: line indexes — incorrect when word wrap enabled.
		// Unability to create a text with "not-determninstic" extent greatly simplify code
		// and allows to get rid of need to create two sub-renderers:
		// 1. Virtual — which render only visible part of text and relies on a visual builder
		// for calculating scroll extent.
		// 2. Full — renders full text, because can't get extent from visual builder

		public double GetWidth(int start, int count);

		public double GetHeight(int start, int count);

		public Size GetExtent(int start, int count);

		#endregion
	}
}
