using RSaitov.SoftwareDevelop.Data;
using System;

namespace RSaitov.SoftwareDevelop.SoftwareDevelopConsole
{
    internal class ExitCommand : ICommand
    {
        public event SendMessage Notify;
        public event ReadString ReadString;
        public ResponseObject Execute(IWorker sender)
        {
            return new ResponseObject(true, "exit");
        }
        public bool Access(IWorker sender) => true;
        public string Title() => "Выход из программы";
    }
}
