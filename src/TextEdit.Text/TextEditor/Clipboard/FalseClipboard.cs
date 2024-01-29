using Avalonia.Input;

namespace TextEdit.Text
{
	internal sealed class FalseClipboard : IClipboard
	{
		public static FalseClipboard Instance { get; } = new FalseClipboard();

		public Task ClearAsync()
		{
			return Task.CompletedTask;
		}

		public Task<object?> GetDataAsync(string format)
		{
			return Task.FromResult<object?>(null);
		}

		public Task<string[]> GetFormatsAsync()
		{
			return Task.FromResult(Array.Empty<string>());
		}

		public Task<string?> GetTextAsync()
		{
			return Task.FromResult<string?>(null);
		}

		public Task SetDataObjectAsync(IDataObject data)
		{
			return Task.CompletedTask;
		}

		public Task SetTextAsync(string? text)
		{
			return Task.CompletedTask;
		}
	}
}
