namespace MyNihongo.KanjiVG.Animation;

public interface IKanjiAnimatorService
{
	/// <summary>
	/// Generates the animated KanjiVG string
	/// </summary>
	/// <param name="text">Original KanjiVG text</param>
	/// <param name="fileName">Name of the KanjiVG file</param>
	/// <param name="svgParams">Parameters for creating the animation</param>
	/// <returns>Animated KanjiVG text</returns>
	string Generate(string text, string fileName, SvgParams svgParams);
}