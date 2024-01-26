using Avalonia.Controls;

namespace TextEdit.Text
{
	/// <summary>
	/// <see cref="Control"/> for rendering and editing <see cref="ITextDocument"/>
	/// </summary>
	public abstract class TextEditor : Control
	{
		public abstract ITextDocument TextDocument { get; set; }

		#region EditMode

		public abstract EditMode EditMode { get; set; }

		#endregion

		#region Undo

		public abstract IUndoManager UndoManager { get; }

		#endregion

		#region HotKey

		public abstract IHotKeyManager HotKeyManager { get; }

		#endregion

		#region WordWrap

		public abstract bool WordWrapEnabled { get; set; }

		#endregion

		#region Caret

		public IEnumerable<ITextCaret> Carets
		{
			get
			{
				foreach (var selectedRange in Selections)
				{
					yield return selectedRange.Caret;
				}
			}
		}

		#endregion

		#region Selections

		/// <summary>
		/// Gets selections.
		/// </summary>
		public abstract IEnumerable<ITextSelection> Selections { get; set; }

		#endregion
	}
}
