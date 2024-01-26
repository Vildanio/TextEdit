namespace TextEdit.Visual
{
	public class VisualInsertedEventArgs : IVisualInsertedEventArgs
	{
		public int Index { get; }

		public IVisual Visual { get; }

		public VisualInsertedEventArgs(int index, IVisual visual)
		{
			Index = index;
			Visual = visual;
		}
	}
}