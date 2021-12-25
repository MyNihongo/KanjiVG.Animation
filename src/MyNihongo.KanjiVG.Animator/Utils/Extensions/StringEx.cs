using System.Text;

namespace MyNihongo.KanjiVG.Animator.Utils.Extensions;

internal static class StringEx
{
	private static readonly UnicodeEncoding UnicodeEncoding = new();

	public static async Task WriteTo(this string @this, string path)
	{
		await using var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 4086, true);
		await using var writer = new StreamWriter(stream);

		await writer.WriteAsync(@this);
		await writer.FlushAsync();
	}

	public static IReadOnlyDictionary<string, string> ParseStyleAttrs(this string @this)
	{
		var dictionary = new Dictionary<string, string>();
		var pairs = @this.Split(';', StringSplitOptions.RemoveEmptyEntries);

		for (var i = 0; i < pairs.Length; i++)
		{
			var keyValue = pairs[i].Split(':');
			if (keyValue.Length != 2)
				continue;

			dictionary.Add(keyValue[0], keyValue[1]);
		}

		return dictionary;
	}

	public static bool TryGetFileName(this string path, out string fileName)
	{
		int start = 0, end = 0;
		fileName = string.Empty;

		for (int i = path.Length - 1, j = 0; i >= 0 && j != 2; i--)
		{
			// Skip modified kanji files
			if (path[i] == '-')
				return false;
			if (path[i] != '.')
				continue;

			switch (++j)
			{
				case 1:
					end = i;
					break;
				case 2:
					start = i + 1;
					break;
			}
		}

		if (start == end)
			throw new InvalidOperationException("kanji unicode is not found");

		fileName = path[start..end].PadLeft(6, '0');

		if (fileName.StartsWith("00"))
			fileName = fileName[2..];

		return true;
	}

	public static char GetKanjiChar(this string fileName)
	{
		var bytes = fileName.GetHexBytes();
		var str = UnicodeEncoding.GetString(bytes);

		if (str.Length == 1)
			return str[0];

		Console.WriteLine("Skipping: {0}", fileName);
		return '\0';
	}

	private static byte[] GetHexBytes(this string hexString)
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