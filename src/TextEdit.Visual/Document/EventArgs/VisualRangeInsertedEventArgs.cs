namespace TextEdit.Visual
{
	public class VisualRangeInsertedEventArgs : IVisualRangeInsertedEventArgs
	{
		public int Index { get; }

		public IEnumerable<IVisual> Visuals { get; }

		public VisualRangeInsertedEventArgs(int index, IEnumerable<IVisual> visuals)
		{
			Index = index;
			Visuals = visuals;
		}
	}
}