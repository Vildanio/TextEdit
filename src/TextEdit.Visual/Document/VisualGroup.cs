using System.Collections;
using Avalonia;
using TextEdit.Collections;

namespace TextEdit.Visual
{
	public class VisualGroup : IVisualGroup
    {
        private readonly Buffer<IVisual> buffer;

        #region Static

        /*public static VisualGroup GapBuffer<TEqutableVisualElement>()
            where TEqutableVisualElement : IVisual, IEquatable<TEqutableVisualElement>
        {
            return new VisualGroup(new GapBuffer<TEqutableVisualElement>());
        }*/

        public static VisualGroup ListBuffer()
        {
            return new VisualGroup(new ListBuffer<IVisual>());
        }

        public static VisualGroup ListBuffer(int capacity)
        {
            return new VisualGroup(new ListBuffer<IVisual>(capacity));
        }

        #endregion

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="VisualGroup"/> class
		/// </summary>
		/// <param name="buffer"></param>
        public VisualGroup(Buffer<IVisual> buffer)
        {
            this.buffer = buffer;
        }

		#endregion

		#region IVisualGroup

		#region IReadOnlyVisualGroup

		#region IReadOnlyBuffer

		#region IReadOnlyList

		#region IEnumerable

		public IEnumerator<IVisual> GetEnumerator()
		{
			return buffer.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return buffer.GetEnumerator();
		}

		#endregion

		public int Count => buffer.Count;

        public IVisual this[int index]
        {
            get
            {
                return buffer[index];
            }

            set
            {
                buffer[index] = value;
            }
        }

		#endregion

		public bool Contains(IVisual item)
		{
			return buffer.Contains(item);
		}

		public int IndexOf(IVisual value, int startIndex)
		{
			return buffer.IndexOf(value, startIndex);
		}

		public int IndexOf(ReadOnlySpan<IVisual> value, int startIndex)
		{
			return buffer.IndexOf(value, startIndex);
		}

		public int LastIndexOf(IVisual value, int startIndex)
		{
			return buffer.LastIndexOf(value, startIndex);
		}

		public int LastIndexOf(ReadOnlySpan<IVisual> value, int startIndex)
		{
			return buffer.LastIndexOf(value, startIndex);
		}

		public int IndexOfAny(IVisual value0, IVisual value1, int startIndex)
		{
			return buffer.IndexOfAny(value0, value1, startIndex);
		}

		public int IndexOfAny(IVisual value0, IVisual value1, IVisual value2, int startIndex)
		{
			return buffer.IndexOfAny(value0, value1, value2, startIndex);
		}

		public int IndexOfAny(IEnumerable<IVisual> items, int startIndex)
		{
			return buffer.IndexOfAny(items, startIndex);
		}

		public int IndexOfAny(ReadOnlySpan<IVisual> items, int startIndex)
		{
			return buffer.IndexOfAny(items, startIndex);
		}

		public int LastIndexOfAny(IVisual value0, IVisual value1, int startIndex)
		{
			return buffer.LastIndexOfAny(value0, value1, startIndex);
		}

		public int LastIndexOfAny(IVisual value0, IVisual value1, IVisual value2, int startIndex)
		{
			return buffer.LastIndexOfAny(value0, value1, value2, startIndex);
		}

		public int LastIndexOfAny(IEnumerable<IVisual> items, int startIndex)
		{
			return buffer.LastIndexOfAny(items, startIndex);
		}

		public int LastIndexOfAny(ReadOnlySpan<IVisual> items, int startIndex)
		{
			return buffer.LastIndexOfAny(items, startIndex);
		}

		public void CopyTo(int index, int count, Span<IVisual> span)
		{
			buffer.CopyTo(index, count, span);
		}

		public void CopyTo(int sourceIndex, IVisual[] destination, int destinationIndex, int count)
		{
			buffer.CopyTo(sourceIndex, destination, destinationIndex, count);
		}

		public ReadOnlySpan<IVisual> AsSpan(int start, int count)
		{
			return buffer.AsSpan(start, count);
		}

		public ReadOnlyMemory<IVisual> AsMemory(int start, int count)
		{
			return buffer.AsMemory(start, count);
		}

		public int IndexOf(IVisual item)
		{
			return buffer.IndexOf(item);
		}

		#endregion

		public virtual int GetIndex(double height)
        {
			ThrowHelper.ThrowIfNegative(height);

            int i = 0;
            double bottom = 0;

            for (; i < buffer.Count; i++)
            {
                IVisual visualLine = buffer[i];

                if (height >= bottom && height <= bottom + visualLine.Height)
                {
                    break;
                }

                bottom += visualLine.Height;
            }

            return i;
        }

        public virtual double GetHeight(int start, int count)
        {
            ThrowHelper.ThrowIfOutOfRange(start, count, Count);

            double height = 0;

            for (int i = start; i < start + count; i++)
            {
                IVisual visualLine = buffer[i];

                height += visualLine.Height;
            }

            return height;
        }

        public virtual double GetWidth(int start, int count)
        {
            ThrowHelper.ThrowIfOutOfRange(start, count, Count);

            double width = 0;

            for (int i = start; i < start + count; i++)
            {
                IVisual visualLine = buffer[i];

                width = Math.Max(width, visualLine.Width);
            }

            return width;
        }

        public virtual Size GetBounds(int start, int count)
        {
            ThrowHelper.ThrowIfOutOfRange(start, count, Count);

            double width = 0;
            double height = 0;

            for (int i = start; i < start + count; i++)
            {
                IVisual visualLine = buffer[i];

                height += visualLine.Height;
                width = Math.Max(width, visualLine.Width);
            }

            return new Size(width, height);
        }

		#endregion

		#region IList

		#region ICollection

		public bool IsReadOnly => buffer.IsImmutable;

		public void Add(IVisual item)
		{
			buffer.Add(item);
		}

		public void Clear()
		{
			buffer.Clear();
		}

		public void CopyTo(IVisual[] array, int arrayIndex)
		{
			buffer.CopyTo(array, arrayIndex);
		}

		public bool Remove(IVisual item)
		{
			return buffer.Remove(item);
		}

		#endregion

		public void RemoveAt(int index)
		{
			buffer.RemoveAt(index);
		}

		public void Insert(int index, IVisual item)
		{
			buffer.Insert(index, item);
		}

		#endregion

		public void RemoveRange(int index, int count)
		{
			buffer.RemoveRange(index, count);
		}

		public void InsertSpan(int index, ReadOnlySpan<IVisual> span)
		{
			buffer.InsertSpan(index, span);
		}

		public void InsertRange(int index, IEnumerable<IVisual> enumerable)
		{
			buffer.InsertRange(index, enumerable);
		}

		#endregion
	}
}
