using Autofac;
using RSaitov.SoftwareDevelop.Data;
using RSaitov.SoftwareDevelop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSaitov.SoftwareDevelop.SoftwareDevelopConsole
{    
    public static class ContainerConfig
    {
        private static IContainer _container;
        public static IContainer Container => _container ?? (_container = Builder.Build());

        private static ContainerBuilder _builder;
        public static ContainerBuilder Builder => _builder ?? (_builder = new ContainerBuilder());
        public static void RegisterDependencies()
        {
            Builder.RegisterType<Application>().AsSelf();
            Builder.RegisterType<TextFileDB>().As<IRepository>();
            Builder.RegisterType<Service>().As<IService>();
        }
    }
}
