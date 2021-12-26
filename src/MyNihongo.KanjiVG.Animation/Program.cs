using CommandLine;
using MyNihongo.KanjiVG.Animation.CLI;
using MyNihongo.KanjiVG.Animation.CLI.Services;

await Parser.Default.ParseArguments<Args>(args)
	.WithParsedAsync(new KanjiAnimationCreator().CreateAsync);