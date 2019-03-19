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
            builder.RegisterType<MemoryRepository>();
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.RegisterType<CsvDeserializer>().As<IDeserializer>();
            builder.RegisterType<FilesReader>().As<IFilesReader>().SingleInstance();
            builder.RegisterType<DeserializeService>().As<IDeserializeService>();



            return builder.Build();
        }
    }
}
