using System.ComponentModel.DataAnnotations;

namespace PhotoCopier.Models;

public class AppConfig
{
    /// <summary>
    /// Path for files foled
    /// </summary>
    [Required]
    public string SourceFileFolder { get; init; }

    /// <summary>
    /// File search patterns - extensions
    /// </summary>
    [Required]
    public string[] Extensions { get; init; }

    /// <summary>
    /// Minimum number of file per day for creating new folder
    /// If day contains less files - they would be placed to OTHER folder
    /// </summary>
    [Required]
    [Range(1, uint.MaxValue)]
    public uint MinFilesNumberForNewFolder { get; init; }
}