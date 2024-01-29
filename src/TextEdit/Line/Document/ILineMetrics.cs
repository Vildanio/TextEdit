namespace TextEdit.Line
{
	/// <summary>
	/// Provides information about text splitted into lines
	/// </summary>
	public interface ILineMetrics
	{
		/// <summary>
		/// Gets count of lines
		/// </summary>
		public int Count { get; }

		#region GetLine

		/// <summary>
		/// Gets length of the line in the given <paramref name="index"/>
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public int GetLineLength(int index);

		/// <summary>
		/// Gets end of line end excluding line terminator length
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public int GetLineEndFromIndex(int index);

		/// <summary>
		/// Gets end of line which contains the given <paramref name="offset"/>
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		public int GetLineEndFromOffset(int offset);

		/// <summary>
		/// Gets offset of the line in the given <paramref name="index"/>
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public int GetLineStartFromIndex(int index);

		/// <summary>
		/// Gets start offset of the line which contains the <paramref name="offset"/>
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		public int GetLineStartFromOffset(int offset);

		/// <summary>
		/// Gets index of line which stores character in the given offset
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		public int GetLineAtOffset(int offset);

		/// <summary>
		/// Gets start and end offsets of the line in the given <paramref name="index"/>
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public (int Start, int End) GetLineBounds(int index);

		#endregion

		/// <summary>
		/// Gets length of line ending in the specified index
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public byte GetLineTerminatorLength(int index);

		/// <summary>
		/// Gets <see cref="LinePosition"/> from offset
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		public LinePosition GetLinePositionByOffset(int offset);

		/// <summary>
		/// Gets offset from <see cref="LinePosition"/>
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public int GetOffsetByPosition(LinePosition position);
	}
}
