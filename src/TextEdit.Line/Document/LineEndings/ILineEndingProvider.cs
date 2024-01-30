namespace TextEdit.Line
{
    /// <summary>
    /// Provides lengths of line endings
    /// </summary>
    internal interface ILineEndingProvider
    {
        /// <summary>
        /// Gets line ending length of the line at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte GetLineTerminatorLength(int index);

        /// <summary>
        /// Gets sum of lengths of the lines in the given range
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int GetLineTerminatorsLength(int index, int count);
    }
}