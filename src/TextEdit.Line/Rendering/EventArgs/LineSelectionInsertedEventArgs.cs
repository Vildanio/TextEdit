namespace TextEdit.Line
{
	public class LineSelectionInsertedEventArgs : ILineSelectionInsertedEventArgs
	{
		public int Index { get; }

		public LineHitRange Selection { get; }

		public LineSelectionInsertedEventArgs(int index, LineHitRange selection)
		{
			Index = index;
			Selection = selection;
		}
	}
}
