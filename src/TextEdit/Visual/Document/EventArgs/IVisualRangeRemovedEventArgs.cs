namespace TextEdit.Visual
{
	public interface IVisualRangeRemovedEventArgs
	{
		public int Index { get; }

		public IEnumerable<IVisual> Visuals { get; }
	}
}
