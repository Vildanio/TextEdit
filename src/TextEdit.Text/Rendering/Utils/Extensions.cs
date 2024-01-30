namespace TextEdit.Text
{
	public static class Extensions
	{
		/// <summary>
		/// Gets value indicating whether the <paramref name="textSelection"/> has not-empty selections or not
		/// </summary>
		/// <param name="textSelection"></param>
		/// <returns></returns>
		public static bool IsEmpty(this ITextSelectionManager textSelection)
		{
			ThrowHelper.ThrowIfNull(textSelection);

			return textSelection.Selections.All(x => !x.IsEmpty);
		}
	}
}
