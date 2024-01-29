namespace TextEdit.Line
{
	public class LineInsertedEventArgs : ILineInsertedEventArgs
	{
		public int Index { get; }

		public string Text { get; }

		public LineInsertedEventArgs(int index, string text)
		{
			Index = index;
			Text = text;
		}
	}
}