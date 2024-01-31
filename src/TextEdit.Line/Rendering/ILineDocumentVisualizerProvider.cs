namespace TextEdit.Line
{
	/// <summary>
	/// Provides <see cref="ILineDocumentVisualizer"/>
	/// </summary>
	public interface ILineDocumentVisualizerProvider
	{
		public ILineDocumentVisualizer GetVisualizer(ILineDocument lineDocument);
	}
}
