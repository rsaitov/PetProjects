using RSaitov.SoftwareDevelop.Data;
using RSaitov.SoftwareDevelop.Domain;
using System;

namespace RSaitov.SoftwareDevelop.SoftwareDevelopConsole
{
    internal class AddWorker : ICommand
    {
        public event SendMessage Notify;
        public event ReadString ReadString;
        private readonly IService _service;

        public AddWorker(IService service)
        {
            _service = service;
        }

        public ResponseObject Execute(IWorker sender)
        {
            var workerName = ReadString?.Invoke("Введите имя сотрудника: ");
            var workerRoleString = ReadString?.Invoke("Выберите роль сотрудника (1 - Manager, 2 - Employee, 3 - Freelancer): ");
            int workerRoleNumber = UserEnteredValueParser.ParseInt(workerRoleString, Notify);
            if (workerRoleNumber == Int32.MinValue || workerRoleNumber < 1 || workerRoleNumber > 3)
                return new ResponseObject("Выбрана некорректная роль");

            var worker = WorkerFactory.GenerateWorker(workerName, (WorkerRole)workerRoleNumber);
            var createWorkerResult = _service.AddWorker(sender, worker);
            if (!createWorkerResult.Success)
            {
                var message = "Ошибка: сотрудник не добавлен";
                Notify?.Invoke(message);
                return new ResponseObject(message);
            }

            Notify?.Invoke("Сотрудник успешно добавлен");
            return new ResponseObject(true);
        }
        public bool Access(IWorker sender)
        {
            return sender.GetRole() == WorkerRole.Manager;
        }

        public string Title() => "Добавить сотрудника";
    }
}
