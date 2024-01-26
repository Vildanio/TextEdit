namespace TextEdit.Line
{
	public interface ILineRangeInsertedEventArgs
	{
		public int Index { get; }

		public IEnumerable<string> Lines { get; }
	}
}