using CommandLine;
using MyNihongo.KanjiVG.Animation;
using MyNihongo.KanjiVG.Animation.Utils.Extensions;
using MyNihongo.KanjiVG.Animator.Services;

await Parser.Default.ParseArguments<Args>(args)
	.WithParsedAsync(async x =>
	{
		var svgParams = x.ToSvgParams();
		await new KanjiAnimatorService().GenerateAsync(svgParams);
	});