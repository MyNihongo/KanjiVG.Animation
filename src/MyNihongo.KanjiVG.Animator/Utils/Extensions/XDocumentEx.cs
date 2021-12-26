using System.Xml.Linq;
using NUglify;
using NUglify.Html;

namespace MyNihongo.KanjiVG.Animation.Utils.Extensions;

internal static class XDocumentEx
{
	private static readonly HtmlSettings MinifySettings = new()
	{
		RemoveAttributeQuotes = false
	};

	public static string Minify(this XDocument @this)
	{
		var xmlString = @this.ToString();
		var result = Uglify.Html(xmlString, MinifySettings);
		if (result.HasErrors)
			throw new InvalidOperationException("Cannot minify the SVG");

		return result.Code;
	}
}