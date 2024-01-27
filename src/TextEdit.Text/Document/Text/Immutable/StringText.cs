using TextEdit.Collections;

namespace TextEdit.Text
{
    /// <summary>
    /// <see cref="IReadOnlyText"/> implemented through <see cref="StringBuffer"/>
    /// </summary>
    public class StringText : TextBuffer
    {
        private StringBuffer StringBuffer => (StringBuffer)Buffer;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StringText"/> class
        /// </summary>
        public StringText()
            : this(string.Empty)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringText"/> class
        /// </summary>
        /// <param name="text"></param>
        public StringText(string text)
            : this(new StringBuffer(text))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringText"/> class
        /// </summary>
        /// <param name="stringBuffer"></param>
        private StringText(StringBuffer stringBuffer)
            : base(stringBuffer)
        {
            
        }

        #endregion

        #region Text

        public override string AsString(int start, int count)
        {
            return StringBuffer.AsString(start, count);
        }

		public override IText Clone()
		{
            return new StringText(StringBuffer.String);
		}

		#endregion
	}
}