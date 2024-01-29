using TextEdit.Line;

namespace TextEdit.Text.Rendering
{
	internal static class HitConverter
	{
		public static TextHitRange ToTextHitRange(LineHitRange lineHitRange, ILineMetrics lineMetrics)
		{
			// Get text position from line position
			var start = lineMetrics.GetOffsetByPosition(new LinePosition(lineHitRange.Start.LineIndex, lineHitRange.Start.TextHit.CharacterIndex));
			var end = lineMetrics.GetOffsetByPosition(new LinePosition(lineHitRange.End.LineIndex, lineHitRange.End.TextHit.CharacterIndex));

			// Recover the lineHitRange in text coordinate system
			var startHit = new TextHit(start, lineHitRange.Start.TextHit.TrailingLength);
			var endHit = new TextHit(end, lineHitRange.End.TextHit.TrailingLength);

			return new TextHitRange(startHit, endHit);
		}

		public static LineHitRange ToLineHitRange(TextHitRange textHitRange, ILineMetrics lineMetrics)
		{
			// Get line position from text poisition
			var start = lineMetrics.GetLinePositionByOffset(textHitRange.Start.CharacterIndex);
			var end = lineMetrics.GetLinePositionByOffset(textHitRange.End.CharacterIndex);

			// Recover the textHitRange in line coordintae system
			var startHit = new LineHit(start.LineIndex, new TextHit(start.CharacterIndex, textHitRange.Start.TrailingLength));
			var endHit = new LineHit(end.LineIndex, new TextHit(end.CharacterIndex, textHitRange.End.TrailingLength));

			return new LineHitRange(startHit, endHit);
		}
	}
}
