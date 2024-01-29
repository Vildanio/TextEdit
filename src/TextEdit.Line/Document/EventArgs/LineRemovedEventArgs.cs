namespace TextEdit.Line
{
	public class LineRemovedEventArgs : ILineRemovedEventArgs
	{
		public int Index { get; }

		public string Text { get; }

		public LineRemovedEventArgs(int index, string text)
		{
			Index = index;
			Text = text;
		}
	}
}