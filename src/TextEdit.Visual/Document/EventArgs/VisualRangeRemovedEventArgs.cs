namespace TextEdit.Visual
{
	public class VisualRangeRemovedEventArgs : IVisualRangeRemovedEventArgs
	{
		public int Index { get; }

		public IEnumerable<IVisual> Visuals { get; }

		public VisualRangeRemovedEventArgs(int index, IEnumerable<IVisual> visuals)
		{
			Index = index;
			Visuals = visuals;
		}
	}
}