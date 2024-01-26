using Avalonia.Input;

namespace TextEdit.Line
{
	public abstract class LineRenderer : InputElement
	{
		public abstract ILineDocument LineDocument { get; set; }

		#region WordWrap

		public bool WordWrapEnabled { get; set; }

		#endregion

		#region Selections

		/// <summary>
		/// Gets selections.
		/// </summary>
		public abstract IEnumerable<ILineSelection> Selections { get; set; }

		#endregion
	}
}
