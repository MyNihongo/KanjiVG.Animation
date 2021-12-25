namespace MyNihongo.KanjiVG.Animator.Services;

public interface IKanjiAnimatorService
{
	Task GenerateAsync(SvgParams svgParams);
}