namespace TextEdit.Line
{
	public class LineReplacedEventArgs : ILineReplacedEventArgs
	{
		public int Index { get; }

		public string OldText { get; }

		public string NewText { get; }

		public LineReplacedEventArgs(int index, string oldText, string newText)
		{
			Index = index;
			OldText = oldText;
			NewText = newText;
		}
	}
}