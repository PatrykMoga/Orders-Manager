using Autofac;
using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core;
using OrdersManager.Core.Domain;
using OrdersManager.Core.Deserializers;
using OrdersManager.Core.Repository;
using OrdersManager.ConsoleUI.UIComponents;

namespace OrdersManager.ConsoleUI
{
    public static class Container
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MemoryRepository>().As<IRepository>().SingleInstance();
            builder.RegisterType<ConsoleLogger>().As<ILogger>().SingleInstance();

            builder.RegisterType<CsvDeserializer>().As<IDeserializer>();
            builder.RegisterType<XmlDeserializer>().As<IDeserializer>();
            builder.RegisterType<JsonDeserializer>().As<IDeserializer>();

            builder.RegisterType<FilesReader>().As<IFilesReader>().InstancePerLifetimeScope();
            builder.RegisterType<DeserializeService>().As<IDeserializeService>();


            builder.RegisterType<MenuService>().As<IMenuService>();
            builder.RegisterType<OrdersAmount>().As<IMenuComponent>();
            builder.RegisterType<AllOrdersList>().As<IMenuComponent>();
            builder.RegisterType<Application>();



            return builder.Build();
        }
    }
}
