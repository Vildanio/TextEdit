namespace TextEdit.Line
{
	public interface ILineSelectionReplacedEventArgs
	{
		public int Index { get; }

		public LineHitRange OldSelection { get; }

		public LineHitRange NewSelection { get; }
	}
}
