namespace MyNihongo.KanjiVG.Animator.Utils;

internal static class FileUtils
{
	public static FileStream AsyncStream(string path, FileMode fileMode, FileAccess fileAccess, FileShare fileShare) =>
		new(path, fileMode, fileAccess, fileShare, 4086, true);
}