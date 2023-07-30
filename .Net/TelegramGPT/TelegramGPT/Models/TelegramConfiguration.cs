namespace TelegramGPT.Models;

public class TelegramConfiguration
{
    public static string ConfigKey = nameof(TelegramConfiguration);
    
    public string Token { get; set; }
    
    public int SecondsToRestartOnFailed { get; set; }
}