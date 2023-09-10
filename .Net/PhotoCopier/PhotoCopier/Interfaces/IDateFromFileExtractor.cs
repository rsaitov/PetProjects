namespace PhotoCopier.Interfaces;

public interface IDateFromFileExtractor
{
    DateTime Extract(string fileName);
}