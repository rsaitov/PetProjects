using RSaitov.SoftwareDevelop.Data;

namespace RSaitov.SoftwareDevelop.SoftwareDevelopConsole
{
    public interface ICommand
    {
        event SendMessage Notify;
        event ReadString ReadString;
        bool Access(IWorker sender);
        ResponseObject Execute(IWorker sender);
        string Title();
    }
}
