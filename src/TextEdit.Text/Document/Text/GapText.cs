using TextEdit.Collections;

namespace TextEdit.Text
{
	/// <summary>
	/// <see cref="IText"/> implemented through <see cref="GapBuffer{T}"/>
	/// </summary>
	public class GapText : TextBuffer
	{
		private GapBuffer<char> GapBuffer => (GapBuffer<char>)Buffer;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="GapText"/> class
		/// </summary>
		public GapText()
			: this(new GapBuffer<char>())
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GapText"/> class
		/// </summary>
		/// <param name="capacity"></param>
		public GapText(int capacity)
			: this(new GapBuffer<char>(capacity))
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GapText"/> class
		/// </summary>
		/// <param name="enumerable"></param>
		public GapText(IEnumerable<char> enumerable)
			: this(new GapBuffer<char>(enumerable))
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GapText"/> class
		/// </summary>
		/// <param name="gapBuffer"></param>
		private GapText(GapBuffer<char> gapBuffer)
			: base(gapBuffer)
		{

		}

		#endregion

		#region Text

		public override string AsString(int start, int count)
		{
			return AsSpan(start, count).ToString();
		}

		public override IText Clone()
		{
			return new GapText(GapBuffer.Clone());
		}

		#endregion
	}
}