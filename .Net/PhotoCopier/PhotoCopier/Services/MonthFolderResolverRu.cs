using PhotoCopier.Interfaces;

namespace PhotoCopier.Services;

/// <summary>
/// All files, not dedicated to separate folder, will be placed in OTHER folder,
/// divided by months. This class describe how these months' folders should be named.
/// </summary>
public class MonthFolderResolverRu : IMonthFolderResolver
{
    private readonly Dictionary<int, string> _months = new Dictionary<int, string> {
        {1, "01 январь"},
        {2, "02 февраль"},
        {3, "03 март"},
        {4, "04 апрель"},
        {5, "05 май"},
        {6, "06 июнь"},
        {7, "07 июль"},
        {8, "08 август"},
        {9, "09 сентябрь"},
        {10, "10 октябрь"},
        {11, "11 ноябрь"},
        {12, "12 декабрь"},
    };

    public string GetMonthName(int month) => _months[month];
}