namespace MyNihongo.KanjiVG.Animator.Services;

public interface IKanjiAnimatorService
{
	string Generate(string text, string fileName, SvgParams svgParams);
}