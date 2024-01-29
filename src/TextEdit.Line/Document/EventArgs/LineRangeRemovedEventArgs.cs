namespace TextEdit.Line
{
	public class LineRangeRemovedEventArgs : ILineRangeRemovedEventArgs
	{
		public int Index { get; }

		public IEnumerable<string> Lines { get; }

		public LineRangeRemovedEventArgs(int index, IEnumerable<string> lines)
		{
			Index = index;
			Lines = lines;
		}
	}
}