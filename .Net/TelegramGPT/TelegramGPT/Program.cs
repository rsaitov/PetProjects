using TelegramGPT.Interfaces;
using TelegramGPT.Models;
using TelegramGPT.Services;

var builder = WebApplication.CreateBuilder(args);

var telegramConfiguration = builder.Configuration
    .GetSection(TelegramConfiguration.ConfigKey)
    .Get<TelegramConfiguration>();

var chatGptConfiguration = builder.Configuration
    .GetSection(ChatGptConfiguration.ConfigKey)
    .Get<ChatGptConfiguration>();

builder.Services
    .AddSingleton(telegramConfiguration)
    .AddSingleton(chatGptConfiguration)
    .AddSingleton<IAiEngine, ChatGptEngine>()
    .AddHostedService<MessageReceiverService>();

IHost host = builder.Build();
host.Run();