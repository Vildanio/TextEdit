using Avalonia.Input;

namespace TextEdit.Text
{
	/// <summary>
	/// Represents a combination of <see cref="KeyGesture"/> for executing some command
	/// </summary>
	public class HotKeyGesture
	{
		public KeyGesture FirstGesture { get; }

		public KeyGesture? SecondGesture { get; }

		public HotKeyGesture(KeyGesture firstGesture, KeyGesture? secondGesture = null)
		{
			ThrowHelper.ThrowIfNull(firstGesture);

			FirstGesture = firstGesture;
			SecondGesture = secondGesture;
		}
    }
}
