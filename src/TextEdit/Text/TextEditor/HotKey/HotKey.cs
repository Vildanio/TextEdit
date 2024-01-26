using Avalonia.Input;

namespace TextEdit.Text
{
	/// <summary>
	/// Represents a combination of <see cref="KeyGesture"/> for executing some command
	/// </summary>
	public abstract class HotKey
	{
		public abstract IEnumerable<KeyGesture> KeyGestures { get; }

		internal HotKey()
		{

		}

		#region Create

		public static HotKey Create(KeyGesture keyGesture)
		{
			return new SimpleHotKey(keyGesture);
		}

		public static HotKey Create(IEnumerable<KeyGesture> keyGestures)
		{
			return new ComplexHotKey(keyGestures);
		}

		#endregion
    }
}
