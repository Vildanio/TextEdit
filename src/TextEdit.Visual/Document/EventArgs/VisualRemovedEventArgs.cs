namespace TextEdit.Visual
{
	public class VisualRemovedEventArgs : IVisualRemovedEventArgs
	{
		public int Index { get; }

		public IVisual Visual { get; }

		public VisualRemovedEventArgs(int index, IVisual visual)
		{
			Index = index;
			Visual = visual;
		}
	}
}