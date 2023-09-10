using Microsoft.Extensions.Configuration;
using PhotoCopier.Models;
using PhotoCopier.Services;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile("appsettings.Development.json", true, true)
    .AddEnvironmentVariables();

var configuration = builder.Build();
var appConfig = configuration.Get<AppConfig>();

UserInteractionService.Start(appConfig);