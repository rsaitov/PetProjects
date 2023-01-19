using log4net;
using log4net.Config;
using RSaitov.SoftwareDevelop.Data;
using RSaitov.SoftwareDevelop.Domain;
using System;

/*
 * Что можно улучшить:
 * меню крутится в бесконечном цикле, команда "выход" завершает приложение
 * лучше чтобы цикл корректно завершался, получив команду "выход" от объекта MainMenu
 */

namespace RSaitov.SoftwareDevelop.SoftwareDevelopConsole
{
    public class Application
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        public Application()
        {
            ContainerConfig.RegisterDependencies();
        }
        public void Run()
        {
            var log4netConfig = new System.IO.FileInfo("./log4net.config");
            XmlConfigurator.Configure(log4netConfig);

            log.Info("Запуск приложения...");

            var isUnauth = true;
            var auth = new AuthConsoleCommand();
            IWorker user = null;
            while (isUnauth)
            {
                var authResult = auth.Execute();
                if (ReferenceEquals(null, authResult))
                {
                    Console.WriteLine("Сотрудник не найден");
                }
                else
                {
                    isUnauth = false;
                    user = authResult;

                    Console.WriteLine();
                    Console.WriteLine("Добро пожаловать!");
                    Console.WriteLine($"Ваша роль в системе: {user.GetRole()}.");
                }
            }

            var mainMenuCommand = new MainMenu(user);
            mainMenuCommand.Notify += ShowConsoleMessage;
            mainMenuCommand.ReadString += ReadLineString;
            var exit = false;
            while (!exit)
            {
                mainMenuCommand.ShowMenu();
                Console.Write("$ ");
                var commandNumber = Console.ReadLine();
                var command = mainMenuCommand.GetCommand(commandNumber);
                if (ReferenceEquals(null, command))
                {
                    Console.WriteLine("Невозможно распознать выбранную команду.");
                    continue;
                }
                Console.WriteLine();
                var response = command.Execute(user);
                if (!string.IsNullOrEmpty(response.Value) && response.Value == "exit")
                {
                    exit = true;
                }
            }
        }

        static void ShowConsoleMessage(string message)
        {
            Console.WriteLine(message);
        }
        static string ReadLineString(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }
}
