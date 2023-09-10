using PhotoCopier.Interfaces;

namespace PhotoCopier.Services;

/// <summary>
/// When new folder with group of files created - need to give it some name
/// Name consists of date in format and new folder suffix
/// Later you can replace suffix with description of photo/video event 
/// </summary>
public class NewFolderNameCreatorByFormat : INewFolderNameCreator
{
    private const string Format = "yyyy MM dd";
    private const string NewFolderSuffix = "aaa";
    
    public string CreateFolderName(DateTime date) => 
        $"{date.ToString(Format)} {NewFolderSuffix}";
}