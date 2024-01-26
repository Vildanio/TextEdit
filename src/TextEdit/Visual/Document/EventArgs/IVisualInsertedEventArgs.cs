namespace TextEdit.Visual
{
	public interface IVisualInsertedEventArgs
	{
		public int Index { get; }

		public IVisual Visual { get; }
	}
}
