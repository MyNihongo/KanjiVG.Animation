using CommandLine;
using MyNihongo.KanjiVG.Animator;
using MyNihongo.KanjiVG.Animator.Services;

await Parser.Default.ParseArguments<Args>(args)
	.WithParsedAsync(new KanjiAnimationCreator().CreateAsync);