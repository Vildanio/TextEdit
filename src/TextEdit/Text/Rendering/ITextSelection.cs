
namespace TextEdit.Text
{
	public interface ITextSelection
	{
		/// <summary>
		/// Gets caret and end position of the selection
		/// </summary>
		public ITextCaret Caret { get; }

		/// <summary>
		/// Gets position where selection started
		/// </summary>
		public TextHit StartPosition { get; set; }

		/// <summary>
		/// Sets <see cref="StartPosition"/> and <see cref="ITextCaret.Position"/>
		/// </summary>
		/// <param name="range"></param>
		public void SetSelectedRange(TextHitRange range);

		/// <summary>
		/// Replaces text in selected range with the given <paramref name="text"/>
		/// </summary>
		/// <param name="text"></param>
		public void Insert(string text);
	}
}
