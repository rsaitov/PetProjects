using PhotoCopier.Interfaces;

namespace PhotoCopier.Services;

public class DateFromFileExtractorByFileName : IDateFromFileExtractor
{
    public DateTime Extract(string fileName)
    {
        try
        {
            // 0123456789012345678
            // 2023-03-08 09-43-46

            var year = Convert.ToInt32(fileName.Substring(0, 4));
            var month = Convert.ToInt32(fileName.Substring(5, 2));
            var day = Convert.ToInt32(fileName.Substring(8, 2));
            var hour = Convert.ToInt32(fileName.Substring(11, 2));
            var minute = Convert.ToInt32(fileName.Substring(14, 2));
            var second = Convert.ToInt32(fileName.Substring(17, 2));

            return new DateTime(year, month, day, hour, minute, second);
        }
        catch
        {
            return DateTime.MinValue;
        }
    }
}