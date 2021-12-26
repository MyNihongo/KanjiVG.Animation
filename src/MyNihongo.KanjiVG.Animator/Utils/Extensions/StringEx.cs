namespace MyNihongo.KanjiVG.Animator.Utils.Extensions;

internal static class StringEx
{
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
}