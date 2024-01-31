using TextEdit.Visual;

namespace TextEdit.Line
{
	/// <summary>
	/// Provides visual representation and visual metrics for a <see cref="ILineDocument"/>
	/// </summary>
	public interface ILineDocumentVisualizer : IVisualMetrics
	{
		public IVisual GetVisualLine(int index);
	}
}
