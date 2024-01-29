using System.Collections;
using Avalonia;
using TextEdit.Collections;

namespace TextEdit.Visual
{
	public class VisualDocument : IVisualDocument
	{
		private readonly IVisualGroup visualGroup;

		#region Static

		/*public static VisualDocument Gap<TEqutableVisualElement>()
            where TEqutableVisualElement : IVisual, IEquatable<TEqutableVisualElement>
        {
            return new VisualDocument(
                new VisualGroup(
                    new GapBuffer<TEqutableVisualElement>()));
        }*/

		public static VisualDocument List()
		{
			return new VisualDocument(
				VisualGroup.CreateUnsafe(
					new ListBuffer<IVisual>()));
		}

		public static VisualDocument List(int count)
		{
			return new VisualDocument(
				VisualGroup.CreateUnsafe(
					new ListBuffer<IVisual>(count)));
		}

		public static VisualDocument List(IEnumerable<IVisual> enumerable)
		{
			return new VisualDocument(
				VisualGroup.CreateUnsafe(
					new ListBuffer<IVisual>(enumerable)));
		}

		internal static VisualDocument List(List<IVisual> list)
		{
			return new VisualDocument(
				VisualGroup.CreateUnsafe(
					new ListBuffer<IVisual>(list)));
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="VisualDocument"/> class
		/// </summary>
		public VisualDocument()
			: this(VisualGroup.CreateUnsafe(new ListBuffer<IVisual>()))
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="VisualDocument"/> class
		/// </summary>
		/// <param name="visualGroup"></param>
		/// <remarks>This constructor is not safe, because the visualLineGroup can change and not fire event</remarks>
		public VisualDocument(IVisualGroup visualGroup)
		{
			this.visualGroup = visualGroup;
		}

		#endregion

		#region IVisualDocument

		#region IVisualGroup

		#region IReadOnlyVisualGroup

		#region IReadOnlyBuffer

		public int Count => visualGroup.Count;

		public bool IsReadOnly => visualGroup.IsReadOnly;

		public IVisual this[int index]
		{
			get
			{
				return visualGroup[index];
			}

			set
			{
				if (VisualReplaced is not null)
				{
					IVisual old = visualGroup[index];

					visualGroup[index] = value;

					VisualReplaced.Invoke(this, new VisualReplacedEventArgs(index, old, value));
				}
				else
				{
					visualGroup[index] = value;
				}
			}
		}

		public bool Contains(IVisual item)
		{
			return visualGroup.Contains(item);
		}

		public int IndexOf(IVisual value, int startIndex)
		{
			return visualGroup.IndexOf(value, startIndex);
		}

		public int IndexOf(ReadOnlySpan<IVisual> value, int startIndex)
		{
			return visualGroup.IndexOf(value, startIndex);
		}

		public int LastIndexOf(IVisual value, int startIndex)
		{
			return visualGroup.LastIndexOf(value, startIndex);
		}

		public int LastIndexOf(ReadOnlySpan<IVisual> value, int startIndex)
		{
			return visualGroup.LastIndexOf(value, startIndex);
		}

		public int IndexOfAny(IVisual value0, IVisual value1, int startIndex)
		{
			return visualGroup.IndexOfAny(value0, value1, startIndex);
		}

		public int IndexOfAny(IVisual value0, IVisual value1, IVisual value2, int startIndex)
		{
			return visualGroup.IndexOfAny(value0, value1, value2, startIndex);
		}

		public int IndexOfAny(IEnumerable<IVisual> items, int startIndex)
		{
			return visualGroup.IndexOfAny(items, startIndex);
		}

		public int IndexOfAny(ReadOnlySpan<IVisual> items, int startIndex)
		{
			return visualGroup.IndexOfAny(items, startIndex);
		}

		public int LastIndexOfAny(IVisual value0, IVisual value1, int startIndex)
		{
			return visualGroup.LastIndexOfAny(value0, value1, startIndex);
		}

		public int LastIndexOfAny(IVisual value0, IVisual value1, IVisual value2, int startIndex)
		{
			return visualGroup.LastIndexOfAny(value0, value1, value2, startIndex);
		}

		public int LastIndexOfAny(IEnumerable<IVisual> items, int startIndex)
		{
			return visualGroup.LastIndexOfAny(items, startIndex);
		}

		public int LastIndexOfAny(ReadOnlySpan<IVisual> items, int startIndex)
		{
			return visualGroup.LastIndexOfAny(items, startIndex);
		}

		public void CopyTo(int index, int count, Span<IVisual> span)
		{
			visualGroup.CopyTo(index, count, span);
		}

		public void CopyTo(int sourceIndex, IVisual[] destination, int destinationIndex, int count)
		{
			visualGroup.CopyTo(sourceIndex, destination, destinationIndex, count);
		}

		public ReadOnlySpan<IVisual> AsSpan(int start, int count)
		{
			return visualGroup.AsSpan(start, count);
		}

		public ReadOnlyMemory<IVisual> AsMemory(int start, int count)
		{
			return visualGroup.AsMemory(start, count);
		}

		public int IndexOf(IVisual item)
		{
			return visualGroup.IndexOf(item);
		}

		public void CopyTo(IVisual[] array, int arrayIndex)
		{
			visualGroup.CopyTo(array, arrayIndex);
		}

		public IEnumerator<IVisual> GetEnumerator()
		{
			return visualGroup.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return visualGroup.GetEnumerator();
		}

		#endregion

		public int GetIndex(double height)
		{
			return visualGroup.GetIndex(height);
		}

		public Size GetBounds(int start, int count)
		{
			return visualGroup.GetBounds(start, count);
		}

		public double GetHeight(int start, int count)
		{
			return visualGroup.GetHeight(start, count);
		}

		public double GetWidth(int start, int count)
		{
			return visualGroup.GetWidth(start, count);
		}

		#endregion

		#region Buffer

		public void Add(IVisual item)
		{
			Insert(Count, item);
		}

		public bool Remove(IVisual item)
		{
			int index = IndexOf(item);

			if (index >= 0)
			{
				RemoveAt(index);

				return true;
			}

			return false;
		}

		public void Clear()
		{
			if (!IsReadOnly && VisualRangeRemoved is not null)
			{
				// Objects in event args should me immutable
				IVisual[] visuals = this.ToArray();

				visualGroup.Clear();

				VisualRangeRemoved.Invoke(this, new VisualRangeRemovedEventArgs(0, visuals));
			}
			else
			{
				visualGroup.Clear();
			}
		}

		public void Insert(int index, IVisual item)
		{
			visualGroup.Insert(index, item);

			if (!IsReadOnly)
			{
				VisualInserted?.Invoke(this, new VisualInsertedEventArgs(index, item));
			}
		}

		public void InsertRange(int index, IEnumerable<IVisual> enumerable)
		{
			visualGroup.InsertRange(index, enumerable);

			if (!IsReadOnly)
			{
				VisualRangeInserted?.Invoke(this, new VisualRangeInsertedEventArgs(index, enumerable));
			}
		}

		public void InsertSpan(int index, ReadOnlySpan<IVisual> span)
		{
			visualGroup.InsertSpan(index, span);

			if (!IsReadOnly)
			{
				VisualRangeInserted?.Invoke(this, new VisualRangeInsertedEventArgs(index, span.ToArray()));
			}
		}

		public void RemoveAt(int index)
		{
			if (!IsReadOnly && VisualRemoved is not null)
			{
				IVisual visual = this[index];

				visualGroup.RemoveAt(index);

				VisualRemoved.Invoke(this, new VisualRemovedEventArgs(index, visual));
			}
			else
			{
				visualGroup.RemoveAt(index);
			}
		}

		public void RemoveRange(int index, int count)
		{
			if (!IsReadOnly && VisualRangeRemoved is not null)
			{
				// Objects in event args should me immutable
				IVisual[] lines = new IVisual[count];

				CopyTo(index, count, lines);

				visualGroup.RemoveRange(index, count);

				VisualRangeRemoved.Invoke(this, new VisualRangeRemovedEventArgs(index, lines));
			}
			else
			{
				visualGroup.RemoveRange(index, count);
			}
		}

		#endregion

		#endregion

		public event EventHandler<IVisualRemovedEventArgs>? VisualRemoved;
		public event EventHandler<IVisualInsertedEventArgs>? VisualInserted;
		public event EventHandler<IVisualReplacedEventArgs>? VisualReplaced;
		public event EventHandler<IVisualRangeRemovedEventArgs>? VisualRangeRemoved;
		public event EventHandler<IVisualRangeInsertedEventArgs>? VisualRangeInserted;

		#endregion
	}
}
