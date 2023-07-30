using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramGPT.Interfaces;
using TelegramGPT.Models;

namespace TelegramGPT.Services;

public sealed class MessageReceiverService : BackgroundService
{
    private readonly TelegramBotClient _client;
    private readonly TelegramConfiguration _telegramConfiguration;
    private readonly IAiEngine _aiEngine;
    private readonly ILogger<MessageReceiverService> _logger;

    public MessageReceiverService(
        TelegramConfiguration telegramConfiguration,
        IAiEngine aiEngine,
        ILogger<MessageReceiverService> logger)
    {
        _telegramConfiguration = telegramConfiguration;
        _aiEngine = aiEngine;
        _logger = logger;

        _client = new TelegramBotClient(telegramConfiguration.Token);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var cts = new CancellationTokenSource();

                var receiverOptions = new ReceiverOptions
                {
                    AllowedUpdates = Array.Empty<UpdateType>()
                };

                _client.StartReceiving(
                    updateHandler: HandleUpdateAsync,
                    pollingErrorHandler: HandlePollingErrorAsync,
                    receiverOptions: receiverOptions,
                    cancellationToken: cts.Token
                );

                var me = await _client.GetMeAsync(cts.Token);
                _logger.LogInformation($"Start listening for @{me.Username}");

                while (true) { }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception raised on message subscription. Message: {e.Message}", e);
                _logger.LogInformation($"Restarting in {_telegramConfiguration.SecondsToRestartOnFailed} seconds");

                await Task.Delay(_telegramConfiguration.SecondsToRestartOnFailed * 1000, stoppingToken);
            }
        }
    }

    async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        long chatId = -1;
        var response = "";
        var errorMessage = "";
        
        try
        {
            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
                return;

            chatId = message.Chat.Id;

            _logger.LogInformation($"Received message '{messageText}' in chat {chatId}");

            response = await _aiEngine.SendRequest(messageText);

            _logger.LogInformation($"Received AI response '{response}'");
            _logger.LogInformation($"Sending message to chat {chatId}...");
        }
        catch (Exception e)
        {
            _logger.LogError("HandleUpdateAsync ERROR on SendRequest {e.Message}", e);

            errorMessage = e.Message;
        }

        try
        {
            await _client.SendTextMessageAsync(
                new ChatId(chatId), 
                string.IsNullOrWhiteSpace(response) 
                    ? $"Error: {errorMessage}"  
                    : response, 
                cancellationToken: cancellationToken);
        }
        
        catch (Exception e)
        {
            _logger.LogError("HandleUpdateAsync ERROR on SendMessageAsync {e.Message}", e);
        }
    }

    private Task HandlePollingErrorAsync(
        ITelegramBotClient botClient,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        _logger.LogError(errorMessage);
        return Task.CompletedTask;
    }
}