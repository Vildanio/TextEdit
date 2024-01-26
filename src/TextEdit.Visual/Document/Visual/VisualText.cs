using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media.TextFormatting;
using TextEdit.Text;

namespace TextEdit.Visual.Document.Visual
{
	/// <summary>
	/// <see cref="IVisual"/> which all columns are selectable
	/// </summary>
	public abstract class VisualText : IVisual
	{
		public abstract int VisualLength { get; }

		public abstract void Construct(IVisualConstructionContext constructionContext);

		public abstract Size GetColumnBounds(int index);

		public abstract double GetColumnHeight(int index);

		public abstract Size GetColumnsBounds(int start, int count);

		public abstract double GetColumnsHeight(int start, int count);

		public abstract double GetColumnsWidth(int start, int count);

		public abstract double GetColumnWidth(int index);

		public IEnumerable<VisualColumnRange> GetSelectableColumns(int start, int count)
		{
			ThrowHelper.ThrowIfOutOfRange(start, count, VisualLength);

			yield return new VisualColumnRange(start, start + count);
		}

		public abstract double GetDistanceFromVisualHit(VisualHit visualColumnHit);

		public abstract VisualHit GetVisualHitFromDistance(double distance);

		public abstract TextHit GetTextHitFromVisualHit(VisualHit visualHit);

		public abstract VisualHit GetVisualHitFromTextHit(TextHit textHit);
	}
}
