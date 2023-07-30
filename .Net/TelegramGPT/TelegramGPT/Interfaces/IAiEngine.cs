namespace TelegramGPT.Interfaces;

public interface IAiEngine
{
    Task<string> SendRequest(string text);
}