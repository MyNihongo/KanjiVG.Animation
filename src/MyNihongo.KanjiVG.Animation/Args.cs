using CommandLine;

namespace MyNihongo.KanjiVG.Animator;

public sealed record Args
{
	[Option('s', "source")]
	public string SourceDirectory { get; init; } = string.Empty;

	[Option('d', "destination")]
	public string DestinationDirectory { get; init; } = string.Empty;

	[Option('r', "rounding", Required = false, Default = 3)]
	public int Rounding { get; init; }

	[Option("outerStroke", Required = false, Default = "#666")]
	public string OuterStroke { get; init; } = string.Empty;

	[Option("outerStrokeWidth", Required = false, Default = 6d)]
	public double OuterStrokeWidth { get; init; }

	[Option("innerStroke", Required = false, Default = "#000")]
	public string InnerStroke { get; init; } = string.Empty;

	[Option("innerStrokeWidth", Required = false, Default = 3d)]
	public double InnerStrokeWidth { get; init; }
}