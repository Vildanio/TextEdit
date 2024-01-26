using System.Text;
using TextEdit.Utils;

namespace TextEdit.Collections
{
	/// <summary>
	/// <see cref="Buffer{T}"/> implemented through <see cref="StringBuilder"/>
	/// </summary>
	public class StringBuilderBuffer : Buffer<char>
    {
        private readonly StringBuilder builder;

		#region Static

        /// <summary>
        /// Creates <see cref="StringBuilderBuffer"/> and sets the given <paramref name="builder"/> as internal buffer without copy.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StringBuilderBuffer CreateUnsafe(StringBuilder builder)
        {
            return new StringBuilderBuffer(builder);
        }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StringBuilderBuffer"/> class
		/// </summary>
		public StringBuilderBuffer()
            : this(new StringBuilder())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringBuilderBuffer"/> class
        /// </summary>
        /// <param name="text"></param>
        public StringBuilderBuffer(string text)
            : this(new StringBuilder(text))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringBuilderBuffer"/> class
        /// </summary>
        /// <param name="builder"></param>
        private StringBuilderBuffer(StringBuilder builder)
        {
            this.builder = builder;
        }

        #endregion

        #region Buffer

        #region IReadOnlyBuffer

		#region IReadOnlyList

		#region IEnumerable

		public override IEnumerator<char> GetEnumerator()
		{
			for (int i = 0; i < Count; i++)
			{
				yield return builder[i];
			}
		}

		#endregion

		public override int Count => builder.Length;

        public override char this[int index]
        {
            get
            {
                return builder[index];
            }

            set
            {
                builder[index] = value;
            }
        }

		#endregion

		public override bool IsImmutable => false;

		public override ReadOnlyMemory<char> AsMemory(int start, int count)
        {
            return builder.ToString(start, count).AsMemory();
        }

        public override ReadOnlySpan<char> AsSpan(int start, int count)
        {
            return builder.ToString(start, count).AsSpan();
        }

        public override void CopyTo(int index, int count, Span<char> span)
        {
            builder.CopyTo(index, span, count);
        }

        public override void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
        {
            builder.CopyTo(sourceIndex, destination, destinationIndex, count);
        }

        public override int IndexOf(char value, int startIndex)
        {
            return builder.IndexOf(value, startIndex);
        }

        public override int IndexOf(ReadOnlySpan<char> value, int startIndex)
        {
            return builder.IndexOf(value, startIndex);
        }

        public override int LastIndexOf(char value, int startIndex)
        {
            return builder.LastIndexOf(value, startIndex);
        }

        public override int LastIndexOf(ReadOnlySpan<char> value, int startIndex)
        {
            return builder.LastIndexOf(value, startIndex);
        }

		public override int IndexOfAny(char value0, char value1, int startIndex)
		{
			return builder.IndexOfAny(value0, value1, startIndex);
		}

		public override int IndexOfAny(char value0, char value1, char value2, int startIndex)
		{
			return builder.IndexOfAny(value0, value1, value2, startIndex);
		}

		public override int IndexOfAny(IEnumerable<char> items, int startIndex)
		{
			return builder.IndexOfAny(items, startIndex);
		}

		public override int IndexOfAny(ReadOnlySpan<char> items, int startIndex)
		{
			return builder.IndexOfAny(items, startIndex);
		}

		public override int LastIndexOfAny(char value0, char value1, int startIndex)
		{
			return builder.LastIndexOfAny(value0, value1, startIndex);
		}

		public override int LastIndexOfAny(char value0, char value1, char value2, int startIndex)
		{
			return builder.LastIndexOfAny(value0, value1, value2, startIndex);
		}

		public override int LastIndexOfAny(IEnumerable<char> items, int startIndex)
		{
			return builder.LastIndexOfAny(items, startIndex);
		}

		public override int LastIndexOfAny(ReadOnlySpan<char> items, int startIndex)
		{
			return builder.LastIndexOfAny(items, startIndex);
		}

		#endregion

		public override void RemoveAt(int index)
        {
            builder.Remove(index, 1);
        }

		public override void RemoveRange(int index, int count)
        {
            builder.Remove(index, count);
        }

		public override void Insert(int index, char item)
		{
			builder.Insert(index, item);
		}

		public override void InsertSpan(int index, ReadOnlySpan<char> span)
		{
			builder.Insert(index, span);
		}

		public override void InsertRange(int index, IEnumerable<char> enumerable)
		{
			builder.Insert(index, string.Join(' ', enumerable));
		}

		#endregion

		#region StringBuilder

		public string ToString(int start, int count)
        {
            return builder.ToString(start, count);
        }

        #endregion
    }
}
