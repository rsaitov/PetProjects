namespace TelegramGPT.Models;

public class ChatGptConfiguration
{
    public static string ConfigKey = nameof(ChatGptConfiguration);
    
    public string BearerToken { get; set; }
    
    public string ModelName { get; set; }
}