using System.Collections;
using TextEdit.Line;

namespace TextEdit.Text.Rendering
{
	/// <summary>
	/// Implements <see cref="ITextSelectionList"/> through <see cref="ILineSelectionList"/>
	/// </summary>
	internal sealed class LineSelectionListAdapter : ITextSelectionList
	{
		private readonly ILineMetrics lineMetrics;
		private readonly ILineSelectionList lineList;

		public LineSelectionListAdapter(ILineMetrics lineMetrics, ILineSelectionList lineList)
		{
			this.lineList = lineList;
			this.lineMetrics = lineMetrics;
			this.lineList.SelectionRemoved += LineSelectionList_SelectionRemoved;
			this.lineList.SelectionInserted += LineSelectionList_SelectionInserted;
			this.lineList.SelectionReplaced += LineSelectionList_SelectionReplaced;
		}

		#region Event handlers

		private void LineSelectionList_SelectionRemoved(object? sender, ILineSelectionRemovedEventArgs e)
		{
			if (SelectionRemoved is not null)
			{
				var textHitRange = HitConverter.ToTextHitRange(e.Selection, lineMetrics);

				SelectionRemoved.Invoke(this, new TextSelectionRemovedEventArgs(e.Index, textHitRange));
			}
		}

		private void LineSelectionList_SelectionInserted(object? sender, ILineSelectionInsertedEventArgs e)
		{
			if (SelectionInserted is not null)
			{
				var textHitRange = HitConverter.ToTextHitRange(e.Selection, lineMetrics);

				SelectionInserted.Invoke(this, new TextSelectionInsertedEventArgs(e.Index, textHitRange));
			}
		}

		private void LineSelectionList_SelectionReplaced(object? sender, ILineSelectionReplacedEventArgs e)
		{
			if (SelectionReplaced is not null)
			{
				var oldTextHitRange = HitConverter.ToTextHitRange(e.OldSelection, lineMetrics);
				var newTextHitRange = HitConverter.ToTextHitRange(e.NewSelection, lineMetrics);

				SelectionReplaced.Invoke(this, new TextSelectionReplacedEventArgs(e.Index, oldTextHitRange, newTextHitRange));
			}
		}

		#endregion

		public TextHitRange this[int index]
		{
			get
			{
				return HitConverter.ToTextHitRange(lineList[index], lineMetrics);
			}
			set
			{
				var lineHitRange = HitConverter.ToLineHitRange(value, lineMetrics);

				lineList[index] = lineHitRange;
			}
		}

		public int Count => lineList.Count;

		public bool IsReadOnly => lineList.IsReadOnly;

		public event EventHandler<ITextSelectionRemovedEventArgs>? SelectionRemoved;
		public event EventHandler<ITextSelectionInsertedEventArgs>? SelectionInserted;
		public event EventHandler<ITextSelectionReplacedEventArgs>? SelectionReplaced;

		public void Add(TextHitRange item)
		{
			Insert(Count, item);
		}

		public void Clear()
		{
			for (int i = 0; i < lineList.Count; i++)
			{
				lineList.RemoveAt(0);
			}
		}

		public bool Contains(TextHitRange item)
		{
			return IndexOf(item) >= 0;
		}

		public void CopyTo(TextHitRange[] array, int arrayIndex)
		{
			for (int i = 0; i < lineList.Count; i++)
			{
				var textHitRange = HitConverter.ToTextHitRange(lineList[i], lineMetrics);

				array[arrayIndex + i] = textHitRange;
			}
		}

		public int IndexOf(TextHitRange item)
		{
			var lineHitRange = HitConverter.ToLineHitRange(item, lineMetrics);

			return lineList.IndexOf(lineHitRange);
		}

		public void Insert(int index, TextHitRange item)
		{
			var lineHitRange = HitConverter.ToLineHitRange(item, lineMetrics);

			lineList.Insert(index, lineHitRange);
		}

		public bool Remove(TextHitRange item)
		{
			var lineHitRange = HitConverter.ToLineHitRange(item, lineMetrics);

			return lineList.Remove(lineHitRange);
		}

		public void RemoveAt(int index)
		{
			lineList.RemoveAt(index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<TextHitRange> GetEnumerator()
		{
			foreach (var lineSelection in lineList)
			{
				yield return HitConverter.ToTextHitRange(lineSelection, lineMetrics);
			}
		}
	}
}
