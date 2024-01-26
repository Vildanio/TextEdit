namespace TextEdit.Line
{
	public interface ILineRangeRemovedEventArgs
	{
		public int Index { get; }

		public IEnumerable<string> Lines { get; }
	}
}