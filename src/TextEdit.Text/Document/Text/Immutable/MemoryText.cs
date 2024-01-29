using TextEdit.Collections;

namespace TextEdit.Text
{
	/// <summary>
	/// <see cref="ITextDocument"/> implemented through <see cref="MemoryBuffer{T}"/>
	/// </summary>
	public class MemoryText : TextBuffer
	{
		private MemoryBuffer<char> MemoryBuffer => (MemoryBuffer<char>)Buffer;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MemoryText"/> class
		/// </summary>
		public MemoryText()
			: this(ReadOnlyMemory<char>.Empty)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MemoryText"/> class
		/// </summary>
		/// <param name="memory"></param>
		public MemoryText(ReadOnlyMemory<char> memory)
			: this(new MemoryBuffer<char>(memory))
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MemoryText"/> class
		/// </summary>
		/// <param name="memoryBuffer"></param>
		private MemoryText(MemoryBuffer<char> memoryBuffer)
			: base(memoryBuffer)
		{

		}

		#endregion

		public override string AsString(int start, int count)
		{
			return MemoryBuffer.Memory.Slice(start, count).ToString();
		}

		public override IText Clone()
		{
			return new MemoryText(MemoryBuffer.Memory);
		}
	}
}