using TextEdit.Collections;

namespace TextEdit.Text
{
	public interface IText : IBuffer<char>, IReadOnlyText
	{
		public new IText Clone();

		IReadOnlyText IReadOnlyText.Clone()
		{
			return Clone();
		}

		public void InsertString(int index, string text);
	}
}