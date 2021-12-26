namespace MyNihongo.KanjiVG.Animation;

public sealed record SvgParams
{
	public int Rounding { get; init; }

	public string OuterStroke { get; init; } = string.Empty;

	public double OuterStrokeWidth { get; init; }

	public string InnerStroke { get; init; } = string.Empty;

	public double InnerStrokeWidth { get; init; }
}