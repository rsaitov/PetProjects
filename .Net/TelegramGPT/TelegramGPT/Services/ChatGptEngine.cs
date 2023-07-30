using System.Text;
using Newtonsoft.Json;
using TelegramGPT.Interfaces;
using TelegramGPT.Models;

namespace TelegramGPT.Services;

public class ChatGptEngine : IAiEngine
{
    private const string AiUrl = "https://api.openai.com/v1/completions";
    
    private readonly ILogger<ChatGptEngine> _logger;
    private readonly ChatGptConfiguration _config;

    public ChatGptEngine(ILogger<ChatGptEngine> logger,
        ChatGptConfiguration config)
    {
        _logger = logger;
        _config = config;
    }
    
    public async Task<string> SendRequest(string text)
    {
        _logger.LogInformation($"Received ChatGPT message request: {text}");
        
        var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add(
            "authorization", 
            $"Bearer {_config.BearerToken}");
        
        var content = new StringContent(
            "{\"model\": \"" + _config.ModelName + "\", \"prompt\": \"" + text + "\",\"temperature\": 1,\"max_tokens\": 100}",
            Encoding.UTF8,
            "application/json"
        );

        var response = await httpClient.PostAsync(AiUrl, content);
        var responseString = await response.Content.ReadAsStringAsync();
        
        _logger.LogInformation($"Received ChatGPT message response: {responseString}");

        var dyData = JsonConvert.DeserializeObject<dynamic>(responseString);
        var errorMessage = dyData?.error.message;
        if (errorMessage != null && !string.IsNullOrWhiteSpace(errorMessage.ToString()))
        {
            _logger.LogInformation($"Error message found: {errorMessage}");
            
            return $"Error AI: {errorMessage}";
        }
        
        var responseText = dyData!.choices[0].text;
        
        _logger.LogInformation($"Parsed ChatGPT message response: {responseText}");
        
        return responseText;
    }
}