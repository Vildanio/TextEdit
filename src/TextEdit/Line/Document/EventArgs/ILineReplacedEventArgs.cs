namespace TextEdit.Line
{
	public interface ILineReplacedEventArgs
	{
		public int Index { get; }

		public string OldText { get; }

		public string NewText { get; }
	}
}