using MyNihongo.KanjiVG.Animator.Resources.Const;
using MyNihongo.KanjiVG.Animator.Utils.Extensions;
using System.Xml;
using System.Xml.Linq;

namespace MyNihongo.KanjiVG.Animator.Services;

public sealed class KanjiAnimatorService : IKanjiAnimatorService
{
	private const string DefaultNamespace = "http://www.w3.org/2000/svg";

	public string Generate(string text, string fileName, SvgParams svgParams)
	{
		var xmlDoc = XDocument.Parse(text);
		xmlDoc = CreateAnimatedDocument(xmlDoc, fileName, svgParams);

		return xmlDoc.Minify();
	}

	private static XDocument CreateAnimatedDocument(XDocument src, string fileName, SvgParams svgParams)
	{
		var duration = 0d;
		var descendantNodes = src.DescendantNodes().ToArray();

		XElement? svgElement = null, animatedContainer = null;
		var animatedPaths = new List<XElement>();

		foreach (var node in descendantNodes)
		{
			if (node.NodeType is XmlNodeType.Comment or XmlNodeType.DocumentType)
			{
				node.Remove();
				continue;
			}

			if (node is not XElement element)
				continue;

			HandleAttributes(element, svgParams, out var elementId);
			switch (element.Name.LocalName)
			{
				case SvgElements.Svg:
					{
						element.SetAttributeValue(XNames.Id, fileName);
						svgElement = element;
						break;
					}
				case SvgElements.Path:
					{
						var animatedPath = element.Copy();
						duration = SetInnerPath(animatedPath, svgParams, duration, fileName);
						animatedPaths.Add(animatedPath);
						break;
					}
				case SvgElements.Graphics when elementId.Contains("StrokePaths") && svgElement != null:
					{
						animatedContainer = element.Copy();
						SetInnerGraphic(animatedContainer, svgParams);

						svgElement.Add(animatedContainer);
						svgElement = null;
						break;
					}
			}
		}

		if (animatedContainer != null)
			foreach (var animatedPath in animatedPaths)
				animatedContainer.Add(animatedPath);


		return src;
	}

	private static void HandleAttributes(XElement element, SvgParams svgParams, out string id)
	{
		id = string.Empty;
		var attrs = element.Attributes()
			.ToArray();

		foreach (var attr in attrs)
			switch (attr.Name.LocalName)
			{
				case SvgAttrs.Id:
					id = attr.Value;
					if (attr.Value.Contains("StrokeNumbers"))
					{
						element.Remove();
						return;
					}
					else
					{
						goto case "kvg";
					}
				case SvgAttrs.Style:
					var styleProps = attr.Value
						.ParseStyleAttrs();

					foreach (var (key, value) in styleProps)
					{
						object val = key switch
						{
							SvgAttrs.StrokeWidth => svgParams.OuterStrokeWidth,
							SvgAttrs.Stroke => svgParams.OuterStroke,
							_ => value
						};

						element.SetAttributeValue(key, val);
					}

					goto case "kvg";
				case "kvg":
				case "element":
				case "type":
				case "variant":
				case "original":
				case "radical":
				case "position":
				case "part":
				case "width":
				case "height":
					attr.Remove();
					break;
			}
	}

	private static void SetInnerGraphic(XElement graphicElement, SvgParams svgParams)
	{
		graphicElement.SetAttributeValue(XNames.Stroke, svgParams.InnerStroke);
		graphicElement.SetAttributeValue(XNames.StrokeWidth, svgParams.InnerStrokeWidth);
	}

	private static double SetInnerPath(in XElement pathElement, in SvgParams svgParams, in double accumulatedDuration, in string fileName)
	{
		var isFirstStroke = accumulatedDuration == 0d;

		var pathLength = pathElement.GetPathLength(svgParams.Rounding);
		pathElement.SetAttributeValue(XNames.StrokeDashArray, pathLength);
		pathElement.SetAttributeValue(XNames.StrokeDashOffset, pathLength);

		var duration = pathLength switch
		{
			< 60d => pathLength / 100d,
			_ => pathLength / 133
		};
		duration = Math.Round(accumulatedDuration + duration, svgParams.Rounding);

		XName name;
		XElement animate;

		if (!isFirstStroke)
		{
			name = XName.Get(SvgElements.Animate, DefaultNamespace);
			animate = new XElement(name);

			animate.SetAttributeValue(XNames.AttributeName, SvgAttrs.StrokeDashOffset);
			animate.SetAttributeValue(XNames.Values, pathLength);
			animate.SetAttributeValue(XNames.Fill, "freeze");
			animate.SetAttributeValue(XNames.Begin, $"{fileName}.click");
			pathElement.Add(animate);
		}

		name = XName.Get(SvgElements.Animate, DefaultNamespace);
		animate = new XElement(name);

		animate.SetAttributeValue(XNames.AttributeName, SvgAttrs.StrokeDashOffset);
		animate.SetAttributeValue(XNames.Values, $"{pathLength};{pathLength};0");

		if (!isFirstStroke)
		{
			var timeBreakpoint = Math.Round(accumulatedDuration / duration, svgParams.Rounding);
			animate.SetAttributeValue(XNames.KeyTimes, $"0;{timeBreakpoint};1");
		}

		animate.SetAttributeValue(XNames.Duration, $"{duration}s");
		animate.SetAttributeValue(XNames.Fill, "freeze");
		animate.SetAttributeValue(XNames.Begin, $"0s;{fileName}.click");
		pathElement.Add(animate);

		return duration;
	}
}