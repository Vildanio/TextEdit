namespace TextEdit.Text
{
	public static class TextSelectionExtensions
	{
		public static TextHit? GetPrimaryCaret(this ITextSelectionManager textSelectionManager)
		{
			var primarySelection = textSelectionManager.GetPrimarySelection();

			if (primarySelection is not null)
			{
				return primarySelection.End;
			}

			return null;
		}

		public static TextHitRange? GetPrimarySelection(this ITextSelectionManager textSelectionManager)
		{
			ThrowHelper.ThrowIfNull(textSelectionManager);

			if (textSelectionManager.Selections.Count > 0)
			{
				return textSelectionManager.Selections[0];
			}

			return null;
		}

		public static IEnumerable<TextHit> EnumerateCarets(this ITextSelectionManager textSelectionManager)
		{
			ThrowHelper.ThrowIfNull(textSelectionManager);

			foreach (var selectedRange in textSelectionManager.Selections)
			{
				yield return selectedRange.End;
			}
		}
	}
}
