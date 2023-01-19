using Autofac;
using log4net;
using RSaitov.SoftwareDevelop.Data;
using RSaitov.SoftwareDevelop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RSaitov.SoftwareDevelop.SoftwareDevelopConsole
{
    public delegate void SendMessage(string message);

    public delegate string ReadString(string message);
    internal class MainMenu
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainMenu));

        public event SendMessage Notify;
        public event ReadString ReadString;
        private List<ICommand> _commands { get; }
        public MainMenu(IWorker worker)
        {
            using (var scope = ContainerConfig.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IService>();

                var addWorkerCommand = new AddWorker(service);
                var reportAllWorkersCommand = new ReportAllWorkers(service);
                var reportWorkerCommand = new ReportWorker(worker, service);
                var addTimeRecordCommand = new AddTimeRecord(service);
                var exitCommand = new ExitCommand();

                addWorkerCommand.Notify += SendToNotify;
                reportAllWorkersCommand.Notify += SendToNotify;
                reportWorkerCommand.Notify += SendToNotify;
                addTimeRecordCommand.Notify += SendToNotify;
                exitCommand.Notify += SendToNotify;

                addWorkerCommand.ReadString += SendReadString;
                reportAllWorkersCommand.ReadString += SendReadString;
                reportWorkerCommand.ReadString += SendReadString;
                addTimeRecordCommand.ReadString += SendReadString;
                exitCommand.ReadString += SendReadString;

                _commands = new List<ICommand>();

                if (addWorkerCommand.Access(worker))
                    _commands.Add(addWorkerCommand);
                if (addTimeRecordCommand.Access(worker))
                    _commands.Add(addTimeRecordCommand);
                if (reportAllWorkersCommand.Access(worker))
                    _commands.Add(reportAllWorkersCommand);
                if (reportWorkerCommand.Access(worker))
                    _commands.Add(reportWorkerCommand);
                if (exitCommand.Access(worker))
                    _commands.Add(exitCommand);
            }
        }
        private void SendToNotify(string message)
        {
            Notify(message);
        }
        private string SendReadString(string message)
        {
            return ReadString(message);
        }
        public ICommand GetCommand(string command)
        {
            int commandNumber;
            var result = Int32.TryParse(command, out commandNumber);
            if (!result)
                return null;

            log.Debug($"Выбрана команда {_commands[commandNumber - 1].Title()}");
            return _commands[commandNumber - 1];
        }
        public void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Выберите пункт меню:");

            var menu = string.Join("\r\n",
                _commands.Select((x, index) => $"   ({index + 1}). {x.Title()}"));
            Console.WriteLine(menu);
        }

        public string Title() => "";
    }
}
