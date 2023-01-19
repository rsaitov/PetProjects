using Autofac;
using RSaitov.SoftwareDevelop.Data;
using RSaitov.SoftwareDevelop.Domain;
using System;

namespace RSaitov.SoftwareDevelop.SoftwareDevelopConsole
{
    internal class AuthConsoleCommand
    {
        public AuthConsoleCommand()
        {
        }
        public IWorker Execute()
        {
            using (var scope = ContainerConfig.Container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IService>();
                Console.Write("Введите имя: ");
                var user = Console.ReadLine();
                var worker = service.GetWorker(user);

                return worker;
            }
        }
    }
}
