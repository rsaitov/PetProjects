using RSaitov.SoftwareDevelop.Data;
using RSaitov.SoftwareDevelop.Domain;
using System;

namespace RSaitov.SoftwareDevelop.SoftwareDevelopConsole
{
    internal class ReportAllWorkers : ICommand
    {
        public event SendMessage Notify;
        public event ReadString ReadString;
        private readonly IService _service;
        public ReportAllWorkers(IService service)
        {
            _service = service;
        }
        public ResponseObject Execute(IWorker sender)
        {
            var defaultDateStart = $"01.01.{DateTime.Now.Year}";
            var dateStartString = ReadString?.Invoke($"Введите дату начала в формате dd.MM.yyyy (по умолчанию {defaultDateStart}): ");
            var dateStart = UserEnteredValueParser.ParseDate(string.IsNullOrEmpty(dateStartString) ? defaultDateStart
                : dateStartString, Notify);
            if (dateStart == DateTime.MinValue)
                new ResponseObject("Ошибка: дата не распознана");

            var defaultDateEnd = $"31.12.{DateTime.Now.Year}";
            var dateEndString = ReadString?.Invoke($"Введите дату окончания в формате dd.MM.yyyy (по умолчанию {defaultDateEnd}): ");
            var dateEnd = UserEnteredValueParser.ParseDate(string.IsNullOrEmpty(dateEndString) ? defaultDateEnd 
                : dateEndString, Notify);
            if (dateEnd == DateTime.MinValue)
                new ResponseObject("Ошибка: дата не распознана");

            var report = _service.GetReportAllWorkers(sender, dateStart, dateEnd);
            Notify?.Invoke($"Отчет за период с {report.Start.ToShortDateString()} по {report.End.ToShortDateString()}");
            foreach(var workerReport in report.SingleWorkerReports)
                Notify?.Invoke($"{workerReport.Worker.GetName()} отработал {workerReport.Hours} " +
                    $"часов и заработал за период {workerReport.Salary} рублей");
            Notify?.Invoke($"Всего часов отработано за период {report.HoursTotal}, " +
                $"сумма к выплате {report.SalaryTotal}");

            return new ResponseObject(true);
        }
        public bool Access(IWorker sender)
        {
            return sender.GetRole() == WorkerRole.Manager;
        }

        public string Title() => "Просмотреть отчет по всем сотрудникам";
    }
}
