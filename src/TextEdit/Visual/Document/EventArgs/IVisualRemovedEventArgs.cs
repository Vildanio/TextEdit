namespace TextEdit.Visual
{
	public interface IVisualRemovedEventArgs
	{
		public int Index { get; }

		public IVisual Visual { get; }
	}
}
