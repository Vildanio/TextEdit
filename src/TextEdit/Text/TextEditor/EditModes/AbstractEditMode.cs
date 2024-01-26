namespace TextEdit.Text
{
	internal abstract class AbstractEditMode
	{
		public static AbstractEditMode InsertMode { get; } = new InsertEditMode();

		public static AbstractEditMode OverstrikeMode { get; } = new OverstrikeEditMode();

		public static AbstractEditMode GetEditMode(EditMode editMode)
		{
			return editMode switch
			{
				EditMode.Insert => InsertMode,
				EditMode.Overstrike => OverstrikeMode,
				_ => throw new ArgumentOutOfRangeException(nameof(editMode)),
			};
		}

		public abstract void Insert(TextEditor editor, char character);
	}
}
