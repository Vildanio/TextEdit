namespace TextEdit.Text
{
	public class CharRangeRemovedEventArgs : ICharRangeRemovedEventArgs
	{
		public int Index { get; }

		public string Chars { get; }

		public CharRangeRemovedEventArgs(int index, string chars)
		{
			Index = index;
			Chars = chars;
		}
	}
}