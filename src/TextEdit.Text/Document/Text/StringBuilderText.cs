using TextEdit.Collections;

namespace TextEdit.Text
{
	/// <summary>
	/// <see cref="IText"/> implemented through <see cref="StringBuilderBuffer"/>
	/// </summary>
	public class StringBuilderText : TextBuffer
	{
		private StringBuilderBuffer StringBuffer => (StringBuilderBuffer)Buffer;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StringBuilderText"/> class
		/// </summary>
		public StringBuilderText()
			: this(new StringBuilderBuffer())
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StringBuilderText"/> class
		/// </summary>
		/// <param name="text"></param>
		public StringBuilderText(string text)
			: this(new StringBuilderBuffer(text))
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StringBuilderText"/> class
		/// </summary>
		/// <param name="buffer"></param>
		private StringBuilderText(StringBuilderBuffer buffer)
			: base(buffer)
		{

		}

		#endregion

		#region Text

		public override string AsString(int start, int count)
		{
			return StringBuffer.ToString(start, count);
		}

		public override IText Clone()
		{
			return new StringBuilderText(StringBuffer.ToString());
		}

		#endregion
	}
}