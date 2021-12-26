using System.Xml.Linq;
using MyNihongo.KanjiVG.Animation.Resources.Const;
using SvgPathProperties;

namespace MyNihongo.KanjiVG.Animation.Utils.Extensions;

internal static class XElementEx
{
	public static XElement Copy(this XElement @this)
	{
		var newElement = new XElement(@this.Name);

		foreach (var attr in @this.Attributes())
			newElement.SetAttributeValue(attr.Name, attr.Value);

		return newElement;
	}

	public static double GetPathLength(this XElement @this, int rounding)
	{
		var pathAttr = @this.Attribute(XNames.Dimension);
		if (pathAttr == null)
			throw new InvalidOperationException("Path dimension is not set");

		var pathLength = new SVGPathProperties(pathAttr.Value)
			.GetTotalLength();

		// ensure that there are no single dots (when the dashoffset is too short)
		var adjustment = Math.Pow(10, -rounding);
		return Math.Round(pathLength + adjustment, rounding);
	}
}