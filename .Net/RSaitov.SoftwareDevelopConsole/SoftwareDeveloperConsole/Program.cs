using Autofac;
using log4net;
using log4net.Config;
using RSaitov.SoftwareDevelop.Data;
using System;

/*
 * Консольное приложение сотрудника
 */

namespace RSaitov.SoftwareDevelop.SoftwareDevelopConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new Application();
            app.Run();
        }
    }
}
