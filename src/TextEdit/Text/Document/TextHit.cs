using System.Runtime.CompilerServices;
using Avalonia.Media;

namespace TextEdit.Text
{
	/// <summary>
	/// Represents a position between characters or the start and the end positions in <see cref="ITextDocument"/>.
	/// </summary>
	public readonly record struct TextHit
	{
		public static TextHit Default { get; } = new TextHit();

		/// <summary>
		/// Gets the index of the first character
		/// </summary>
		public int CharacterIndex { get; }

		/// <summary>
		/// Gets the trailing length value for the character
		/// </summary>
		/// </summary>
		/// <remarks>
		/// In the case of a leading edge, this value is 0. In the case of a trailing edge, 
		/// this value is the number of characters until the next valid caret position
		/// </remarks>
		public int TrailingLength { get; }

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="TextHit"/> struct
		/// </summary>
		/// <param name="characterIndex"></param>
		public TextHit(int characterIndex)
		{
			ThrowHelper.ThrowIfNegative(characterIndex);

			CharacterIndex = characterIndex;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TextHit"/> struct
		/// </summary>
		/// <param name="characterIndex"></param>
		/// <param name="trailingLength"></param>
		public TextHit(int characterIndex, int trailingLength)
		{
			ThrowHelper.ThrowIfNegative(characterIndex);
			ThrowHelper.ThrowIfNegative(trailingLength);

			CharacterIndex = characterIndex;
			TrailingLength = trailingLength;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets sum of the <see cref="CharacterIndex"/> and <see cref="TrailingLength"/>
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int GetLastCharacterIndex()
		{
			return CharacterIndex + TrailingLength;
		}

		#endregion

		#region Operators

		public static bool operator <(TextHit left, TextHit right)
		{
			return left.GetLastCharacterIndex() < right.GetLastCharacterIndex();
		}

		public static bool operator >(TextHit left, TextHit right)
		{
			return left.GetLastCharacterIndex() > right.GetLastCharacterIndex();
		}

		public static bool operator <=(TextHit left, TextHit right)
		{
			return left.GetLastCharacterIndex() <= right.GetLastCharacterIndex();
		}

		public static bool operator >=(TextHit left, TextHit right)
		{
			return left.GetLastCharacterIndex() >= right.GetLastCharacterIndex();
		}

		#endregion

		#region Cast

		public static implicit operator CharacterHit(TextHit textHit)
		{
			return new CharacterHit(textHit.CharacterIndex, textHit.TrailingLength);
		}

		public static implicit operator TextHit(CharacterHit characterHit)
		{
			return new TextHit(characterHit.FirstCharacterIndex, characterHit.TrailingLength);
		}

		#endregion
	}
}
