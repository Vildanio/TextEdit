using System.Runtime.CompilerServices;
using Avalonia.Media;

namespace TextEdit.Visual
{
	/// <summary>
	/// Represents a position between visual columns or the start and the end positions in <see cref="IVisual"/>.
	/// </summary>
	public readonly record struct VisualHit
	{
		/// <summary>
		/// Gets the index of the column.
		/// </summary>
		public int ColumnIndex { get; }

		/// <summary>
		/// Gets the trailing length value for the column.
		/// </summary>
		/// <remarks>
		/// In the case of a leading edge, this value is 0. In the case of a trailing edge, 
		/// this value is the number of columns until the next valid caret position.
		/// </remarks>
		public int TrailingLength { get; }

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="VisualHit"/> struct
		/// </summary>
		/// <param name="columnIndex"></param>
		public VisualHit(int columnIndex)
		{
			ThrowHelper.ThrowIfNegative(columnIndex);

			ColumnIndex = columnIndex;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="VisualHit"/> struct
		/// </summary>
		/// <param name="columnIndex"></param>
		/// <param name="trailingEdge"></param>
		public VisualHit(int columnIndex, int trailingEdge)
		{
			ThrowHelper.ThrowIfNegative(columnIndex);
			ThrowHelper.ThrowIfNegative(trailingEdge);

			TrailingLength = trailingEdge;
			ColumnIndex = columnIndex;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets sum of the <see cref="ColumnIndex"/> and <see cref="TrailingLength"/>
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int GetCharacterIndex()
		{
			return ColumnIndex + TrailingLength;
		}

		#endregion

		#region Operators

		public static bool operator <(VisualHit left, VisualHit right)
		{
			return left.GetCharacterIndex() < right.GetCharacterIndex();
		}

		public static bool operator >(VisualHit left, VisualHit right)
		{
			return left.GetCharacterIndex() > right.GetCharacterIndex();
		}

		public static bool operator <=(VisualHit left, VisualHit right)
		{
			return left.GetCharacterIndex() <= right.GetCharacterIndex();
		}

		public static bool operator >=(VisualHit left, VisualHit right)
		{
			return left.GetCharacterIndex() >= right.GetCharacterIndex();
		}

		#endregion

		#region CharacterHit cast

		public static implicit operator CharacterHit(VisualHit hit)
		{
			return new CharacterHit(hit.ColumnIndex, hit.TrailingLength);
		}

		public static implicit operator VisualHit(CharacterHit hit)
		{
			return new VisualHit(hit.FirstCharacterIndex, hit.TrailingLength);
		}

		#endregion
	}
}