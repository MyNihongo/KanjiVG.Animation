using CommandLine;
using MyNihongo.KanjiVG.Animation;
using MyNihongo.KanjiVG.Animation.Services;

await Parser.Default.ParseArguments<Args>(args)
	.WithParsedAsync(new KanjiAnimationCreator().CreateAsync);