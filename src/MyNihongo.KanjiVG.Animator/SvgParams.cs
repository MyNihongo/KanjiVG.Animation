namespace MyNihongo.KanjiVG.Animator;

public sealed class SvgParams
{
	public SvgParams(string sourceDirectory, string destinationDirectory)
	{
		SourceDirectory = sourceDirectory;
		DestinationDirectory = destinationDirectory;
	}

	public string SourceDirectory { get; }

	public string DestinationDirectory { get; }

	public int Rounding { get; init; }

	public string OuterStroke { get; init; } = string.Empty;

	public double OuterStrokeWidth { get; init; }

	public string InnerStroke { get; init; } = string.Empty;

	public double InnerStrokeWidth { get; init; }
}