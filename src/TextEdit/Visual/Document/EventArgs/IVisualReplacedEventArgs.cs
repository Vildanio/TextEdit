namespace TextEdit.Visual
{
	public interface IVisualReplacedEventArgs
	{
		public int Index { get; }

		public IVisual OldVisual { get; }

		public IVisual NewVisual { get; }
	}
}
