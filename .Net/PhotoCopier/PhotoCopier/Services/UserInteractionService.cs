using PhotoCopier.Models;
using PhotoCopier.Validators;

namespace PhotoCopier.Services;

public static class UserInteractionService
{
    public static void Start(AppConfig config)
    {
        var (result, errorMessage) = AppConfigValidator.IsValid(config);

        if (!result)
        {
            Console.WriteLine($"AppConfig is not valid: {errorMessage}");
            return;
        }
        
        while (true)
        {
            var exitCommand = false;
            Console.WriteLine(@$"
Application config: Folder = {config.SourceFileFolder}, File extensions = {string.Join(',',config.Extensions)}, Min files number for new folder = {config.MinFilesNumberForNewFolder}

Available commands:
1. dry
2. move
3. exit
");
            Console.Write("Enter command--> ");
            var enteredCommand = Console.ReadLine();

            var fileCopier = new FileCopier();

            switch (enteredCommand?.Trim())
            {
                case "1":
                case "dry":
                    fileCopier.Process(config, true);
                    break;

                case "2":
                case "move":
                    fileCopier.Process(config, false);
                    break;

                case "3":
                case "exit":
                    exitCommand = true;
                    break;

                default:
                    Console.WriteLine("Unknown command");
                    break;
            }

            if (exitCommand)
                break;

            Console.WriteLine("Finished");
        }
    }
}