using Autofac;
using OrdersManager.Core;
using OrdersManager.Core.Domain;
using OrdersManager.Core.Importers;
using OrdersManager.Core.Repository;

namespace OrdersManager.ConsoleUI
{
    public static class Container
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();
            builder.RegisterType<MemoryRepository>().As<IRepository>().SingleInstance();
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.RegisterType<CsvDeserializer>().As<IDeserializer>();
            builder.RegisterType<FilesReader>().As<IFilesReader>().InstancePerLifetimeScope();
            builder.RegisterType<DeserializeService>().As<IDeserializeService>();



            return builder.Build();
        }
    }
}
