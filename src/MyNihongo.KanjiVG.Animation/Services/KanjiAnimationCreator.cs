using System.Text;
using MyNihongo.KanaDetector.Extensions;
using MyNihongo.KanjiVG.Animation;
using MyNihongo.KanjiVG.Animator.Utils;
using MyNihongo.KanjiVG.Animator.Utils.Extensions;

namespace MyNihongo.KanjiVG.Animator.Services;

internal sealed class KanjiAnimationCreator
{
	private static readonly UnicodeEncoding UnicodeEncoding = new();
	private readonly IKanjiAnimatorService _animatorService = new KanjiAnimatorService();

	public Task CreateAsync(Args args)
	{
		if (!Directory.Exists(args.SourceDirectory))
			throw new InvalidOperationException($"{nameof(args.SourceDirectory)} does not exist");

		if (!Directory.Exists(args.DestinationDirectory))
			throw new InvalidOperationException($"{nameof(args.DestinationDirectory)} does not exist");

		var svgParams = args.ToSvgParams();
		var tasks = GetResourcePaths(args.SourceDirectory)
			.Select<string, Task>(async (x, i) =>
			{
				if (!TryGetFileName(x, out var fileName))
					return;

				var kanjiChar = GetKanjiChar(fileName);
				if (!kanjiChar.IsKanaOrKanji())
					return;

				var svgString = await GetXmlDocumentAsync(x)
					.ConfigureAwait(false);

				svgString = _animatorService.Generate(svgString, fileName, svgParams);

				await svgString.WriteTo(Path.Combine(args.DestinationDirectory, $"{fileName}.svg"))
					.ConfigureAwait(false);

				Console.Write($"\r{i + 1}");
			});

		return Task.WhenAll(tasks);
	}

	private static IEnumerable<string> GetResourcePaths(string folderPath) =>
		Directory.EnumerateFiles(folderPath, "*svg");

	private static bool TryGetFileName(string path, out string fileName)
	{
		fileName = Path.GetFileNameWithoutExtension(path);

		// Skip modified kanji files
		if (fileName.Contains('-'))
			return false;

		fileName = fileName.PadLeft(6, '0');
		if (fileName.StartsWith("00"))
			fileName = fileName[2..];

		return true;
	}

	public static char GetKanjiChar(string fileName)
	{
		var bytes = GetHexBytes(fileName);
		var str = UnicodeEncoding.GetString(bytes);

		if (str.Length == 1)
			return str[0];

		Console.WriteLine("Skipping: {0}", fileName);
		return '\0';

		static byte[] GetHexBytes(string hexString)
		{
			if (hexString.StartsWith("00"))
				hexString = hexString[2..];

			var bytes = new byte[hexString.Length / 2];
			for (var i = 0; i < hexString.Length; i += 2)
			{
				var str = hexString.Substring(i, 2);
				var index = (hexString.Length - i) / 2 - 1;
				bytes[index] = Convert.ToByte(str, 16);
			}

			return bytes;
		}
	}

	private static async Task<string> GetXmlDocumentAsync(string filePath)
	{
		await using var stream = FileUtils.AsyncStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
		using var reader = new StreamReader(stream);

		return await reader.ReadToEndAsync()
			.ConfigureAwait(false);
	}
}