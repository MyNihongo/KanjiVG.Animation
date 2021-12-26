using MyNihongo.KanjiVG.Animator.Utils;

namespace MyNihongo.KanjiVG.Animation.Utils.Extensions;

internal static class StringEx
{
	public static async Task WriteTo(this string @this, string path)
	{
		await using var stream = FileUtils.AsyncStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
		await using var writer = new StreamWriter(stream);

		await writer.WriteAsync(@this)
			.ConfigureAwait(false);

		await writer.FlushAsync()
			.ConfigureAwait(false);
	}
}