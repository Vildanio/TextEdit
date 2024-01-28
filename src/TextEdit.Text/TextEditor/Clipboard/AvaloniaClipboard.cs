using Avalonia.Input;

namespace TextEdit.Text.TextEditor.Clipboard
{
	internal class AvaloniaClipboard : IClipboard
	{
		private readonly Avalonia.Input.Platform.IClipboard avaloniaClipboard;

		public AvaloniaClipboard(Avalonia.Input.Platform.IClipboard avaloniaClipboard)
		{
			this.avaloniaClipboard = avaloniaClipboard;
		}

		public Task ClearAsync()
		{
			return avaloniaClipboard.ClearAsync();
		}

		public Task<object?> GetDataAsync(string format)
		{
			return avaloniaClipboard.GetDataAsync(format);
		}

		public Task<string[]> GetFormatsAsync()
		{
			return avaloniaClipboard.GetFormatsAsync();
		}

		public Task<string?> GetTextAsync()
		{
			return avaloniaClipboard.GetTextAsync();
		}

		public Task SetDataObjectAsync(IDataObject data)
		{
			return avaloniaClipboard.SetDataObjectAsync(data);
		}

		public Task SetTextAsync(string? text)
		{
			return avaloniaClipboard.SetTextAsync(text);
		}
	}
}
