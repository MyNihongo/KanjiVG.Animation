namespace MyNihongo.KanjiVG.Animation.CLI.Utils.Extensions;

internal static class ArgsEx
{
	public static SvgParams ToSvgParams(this Args @this) =>
		new()
		{
			Rounding = @this.Rounding,
			OuterStroke = @this.OuterStroke,
			OuterStrokeWidth = @this.OuterStrokeWidth,
			InnerStroke = @this.InnerStroke,
			InnerStrokeWidth = @this.InnerStrokeWidth
		};
}