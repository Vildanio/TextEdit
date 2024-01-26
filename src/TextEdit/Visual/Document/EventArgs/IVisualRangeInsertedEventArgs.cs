namespace TextEdit.Visual
{
	public interface IVisualRangeInsertedEventArgs
	{
		public int Index { get; }

		public IEnumerable<IVisual> Visuals { get; }
	}
}
