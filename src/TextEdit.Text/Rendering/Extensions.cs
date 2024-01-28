using System.Diagnostics.CodeAnalysis;

namespace TextEdit.Text.Rendering
{
	public static class Extensions
	{
		/// <summary>
		/// Empties the <paramref name="textSelection"/>
		/// </summary>
		/// <param name="textSelection"></param>
		public static void Empty(this ITextSelection textSelection)
		{
			ThrowHelper.ThrowIfNull(textSelection);

			var endPosition = textSelection.EndPosition;

			// If selection is not already empty
			if (endPosition != textSelection.StartPosition)
			{
				var emptyRange = new TextHitRange(endPosition);

				textSelection.SetSelectedRange(emptyRange);
			}
		}

		public static bool IsEmpty(this ITextSelection textSelection)
		{
			ThrowHelper.ThrowIfNull(textSelection);

			return textSelection.StartPosition == textSelection.EndPosition;
		}

		public static bool IsNullOrEmpty([NotNullWhen(false)] this ITextSelection? textSelection)
		{
			return textSelection is null || textSelection.IsEmpty();
		}
	}
}
