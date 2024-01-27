namespace TextEdit.Text.Unicode
{
	internal static class UnicodeLineTerminators
	{
		#region Constants

		/// <summary>
		/// Carriage return
		/// </summary>
		public const char CR = '\r';

		/// <summary>
		/// Line feed
		/// </summary>
		public const char LF = '\n';

		/// <summary>
		/// CR + LF
		/// </summary>
		public const string CRLF = "\r\n";

		/// <summary>
		/// Verical tab
		/// </summary>
		public const char VT = '\u000B';

		/// <summary>
		/// Form feed
		/// </summary>
		public const char FF = '\u000C';

		/// <summary>
		/// Next line
		/// </summary>
		public const char NEL = '\u0085';

		/// <summary>
		/// Line separator
		/// </summary>
		public const char LS = '\u2028';

		/// <summary>
		/// Paragraph separator
		/// </summary>
		public const char PS = '\u2029';

		#endregion

		public static readonly ReadOnlyMemory<char> LineTerminators = new char[] { CR, LF, VT, FF, NEL, LS, PS };

		public static bool IsLineTerminator(this char character)
		{
			return character switch
			{
				CR or LF or VT or FF or NEL or LS or PS => true,
				_ => false,
			};
		}
	}
}
