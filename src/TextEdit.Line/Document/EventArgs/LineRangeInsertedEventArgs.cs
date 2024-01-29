namespace TextEdit.Line
{
	public class LineRangeInsertedEventArgs : ILineRangeInsertedEventArgs
	{
		public int Index { get; }

		public IEnumerable<string> Lines { get; }

		public LineRangeInsertedEventArgs(int index, IEnumerable<string> lines)
		{
			Index = index;
			Lines = lines;
		}
	}
}