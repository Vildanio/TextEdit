namespace TextEdit.Line
{
	public interface ILineRemovedEventArgs
	{
		public int Index { get; }

		public string Text { get; }
	}
}