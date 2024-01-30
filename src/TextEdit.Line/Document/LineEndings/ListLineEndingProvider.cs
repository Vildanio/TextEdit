using System.Collections;

namespace TextEdit.Line
{
    public class ListLineEndingProvider : ILineEndingProvider, IList<byte>
    {
        private readonly List<byte> lineEndings;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ListLineEndingProvider"/> class
		/// </summary>
		public ListLineEndingProvider()
			: this(new List<byte>())
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListLineEndingProvider"/> class
        /// </summary>
        /// <param name="capacity"></param>
        public ListLineEndingProvider(int capacity)
			: this(new List<byte>(capacity))
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListLineEndingProvider"/> class
        /// </summary>
        /// <param name="lineEndings"></param>
        public ListLineEndingProvider(IEnumerable<byte> lineEndings)
			: this(new List<byte>(lineEndings))
        {

        }

		/// <summary>
		/// Initializes a new instance of the <see cref="ListLineEndingProvider"/> class
		/// </summary>
		/// <param name="lineEndings"></param>
		public ListLineEndingProvider(List<byte> lineEndings)
        {
			this.lineEndings = lineEndings;
		}

		#endregion

		#region IList

		public byte this[int index]
		{
			get
			{
				return lineEndings[index];
			}

			set
			{
				lineEndings[index] = value;
			}
		}

		public int Count => lineEndings.Count;

		public bool IsReadOnly => false;

		public void Add(byte item)
		{
			lineEndings.Add(item);
		}

		public void Clear()
		{
			lineEndings.Clear();
		}

		public bool Contains(byte item)
		{
			return lineEndings.Contains(item);
		}

		public void CopyTo(byte[] array, int arrayIndex)
		{
			lineEndings.CopyTo(array, arrayIndex);
		}

		public IEnumerator<byte> GetEnumerator()
		{
			return lineEndings.GetEnumerator();
		}

		public int IndexOf(byte item)
		{
			return lineEndings.IndexOf(item);
		}

		public void Insert(int index, byte item)
		{
			lineEndings.Insert(index, item);
		}

		public bool Remove(byte item)
		{
			return lineEndings.Remove(item);
		}

		public void RemoveAt(int index)
		{
			lineEndings.RemoveAt(index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return lineEndings.GetEnumerator();
		}

		#endregion

		#region ILineMetrics

		public byte GetLineTerminatorLength(int index)
		{
			return lineEndings[index];
		}

		public int GetLineTerminatorsLength(int index, int count)
		{
			int length = 0;

			for (int i = index; i < index + count; i++)
			{
				length += lineEndings[index];
			}

			return length;
		}

		#endregion
	}
}