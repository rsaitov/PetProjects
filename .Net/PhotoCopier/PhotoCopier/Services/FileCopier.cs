using PhotoCopier.Helper;
using PhotoCopier.Interfaces;
using PhotoCopier.Models;

namespace PhotoCopier.Services;

public class FileCopier
{
    private const string OutputFolderName = "result";
    private const string OtherFolderName = "Разное";
    private const string VideoFolderName = "видео";
    
    private readonly IDateFromFileExtractor _dateExtractorService = new DateFromFileExtractorByFileName();
    private readonly IMonthFolderResolver _monthFolderResolver = new MonthFolderResolverRu();
    private readonly INewFolderNameCreator _newFolderNameCreator = new NewFolderNameCreatorByFormat();

    public bool Process(AppConfig config, bool dryRun)
    {
        var destination = Path.Combine(config.SourceFileFolder, OutputFolderName);
        
        if (!Directory.Exists(config.SourceFileFolder))
        {
            throw new DirectoryNotFoundException(config.SourceFileFolder);
        }

        if (!Directory.Exists(destination))
        {
            Directory.CreateDirectory(destination);
        }

        var sourceDirectoryInfo = new DirectoryInfo(config.SourceFileFolder);
        var extensions = config.Extensions;
        var sourceFiles = extensions.SelectMany(ext => sourceDirectoryInfo.GetFiles(ext));

        var archive = new Dictionary<DateTime, List<FileInfo>>();

        foreach (var sourceFile in sourceFiles)
        {
            var fileDateFromFileName = _dateExtractorService.Extract(sourceFile.Name);
            if (fileDateFromFileName == DateTime.MinValue)
            {
                Console.WriteLine($"Can't get date from file name {sourceFile.Name}");
                continue;
            }

            if (archive.ContainsKey(fileDateFromFileName.Date))
            {
                archive[fileDateFromFileName.Date].Add(sourceFile);
            }
            else
            {
                archive[fileDateFromFileName.Date] = new List<FileInfo> { sourceFile };
            }
        }

        foreach (var archiveDate in archive)
        {
            var date = archiveDate.Key;
            var files = archiveDate.Value;
            string destinationImageFolder = "";

            if (files.Count >= config.MinFilesNumberForNewFolder)
            {
                destinationImageFolder = Path.Combine(
                    destination, 
                    date.Year.ToString(), 
                    _newFolderNameCreator.CreateFolderName(date));
            }
            else
            {
                destinationImageFolder = Path.Combine(destination, date.Year.ToString(), OtherFolderName, 
                    _monthFolderResolver.GetMonthName(date.Month));
            }

            Console.WriteLine($"Destination folder: {destinationImageFolder}");
            if (!Directory.Exists(destinationImageFolder))
            {
                Console.WriteLine($"Creating folder: {destinationImageFolder}");
                Directory.CreateDirectory(destinationImageFolder);
            }

            foreach (var file in files)
            {
                var destinationFileFolder = destinationImageFolder;

                if (FileTypeHelper.IsVideoFile(file.FullName))
                {
                    destinationFileFolder = Path.Combine(destinationImageFolder, VideoFolderName);
                }

                if (!Directory.Exists(destinationFileFolder))
                {
                    Console.WriteLine($"Creating folder: {destinationFileFolder}");
                    Directory.CreateDirectory(destinationFileFolder);
                }

                if (!dryRun)
                {
                    File.Move(file.FullName, Path.Combine(destinationFileFolder, file.Name));
                }

                Console.WriteLine(file.FullName);
            }
        }

        return true;
    }
}