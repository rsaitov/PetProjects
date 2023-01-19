using System;

namespace RSaitov.SoftwareDevelop.SoftwareDevelopConsole
{
    public static class UserEnteredValueParser
    {
        public static DateTime ParseDate(string inputString, SendMessage Notify)
        {
            DateTime date;
            var result = DateTime.TryParse(inputString, out date);
            if (!result)
            {
                Notify?.Invoke("Ошибка: дата не распознана");
                return DateTime.MinValue;
            }
            return date;
        }
        public static int ParseInt(string inputString, SendMessage Notify)
        {
            int intValue;
            var result = Int32.TryParse(inputString, out intValue);
            if (!result)
            {
                Notify?.Invoke("Ошибка: число не распознано");
                return int.MinValue;
            }
            return intValue;
        }
    }
}
