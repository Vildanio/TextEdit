namespace TextEdit.Visual
{
	public class VisualReplacedEventArgs : IVisualReplacedEventArgs
	{
		public int Index { get; }

		public IVisual OldVisual { get; }

		public IVisual NewVisual { get; }

		public VisualReplacedEventArgs(int index, IVisual oldVisual, IVisual newVisual)
		{
			Index = index;
			OldVisual = oldVisual;
			NewVisual = newVisual;
		}
	}
}