namespace TextEdit.Line
{
	public class LineSelectionReplacedEventArgs : ILineSelectionReplacedEventArgs
	{
		public int Index { get; }

		public LineHitRange OldSelection { get; }

		public LineHitRange NewSelection { get; }

		public LineSelectionReplacedEventArgs(int index, LineHitRange oldSelection, LineHitRange newSelection)
		{
			Index = index;
			OldSelection = oldSelection;
			NewSelection = newSelection;
		}
	}
}
