namespace PhotoCopier.Helper;

public static class FileTypeHelper
{
    private static readonly List<string> VideoFileExtensions = new List<string>
    {
        ".avi",
        ".mp4",
        ".mov",
        ".mkv",
        ".mpg"
    };

    public static bool IsVideoFile(string fileName)
        => VideoFileExtensions.Any(x => fileName.EndsWith(x));
}