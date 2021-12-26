using System.Xml.Linq;

namespace MyNihongo.KanjiVG.Animator.Resources.Const;

internal static class XNames
{
	public static readonly XName Id = SvgAttrs.Id,
		Stroke = SvgAttrs.Stroke,
		StrokeWidth = SvgAttrs.StrokeWidth,
		StrokeDashArray = SvgAttrs.StrokeDashArray,
		StrokeDashOffset = SvgAttrs.StrokeDashOffset,
		AttributeName = SvgAttrs.AttributeName,
		Values = SvgAttrs.Values,
		Duration = SvgAttrs.Duration,
		Fill = SvgAttrs.Fill,
		Begin = SvgAttrs.Begin,
		Dimension = SvgAttrs.Dimension,
		KeyTimes = SvgAttrs.KeyTimes;
}