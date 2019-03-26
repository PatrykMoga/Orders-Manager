using Autofac;
using OrdersManager.ConsoleUI.MenuServiceComponents;
using OrdersManager.Core;
using OrdersManager.Core.Logs;
using OrdersManager.Core.Deserializers;
using OrdersManager.Core.Repository;
using OrdersManager.ConsoleUI.MenuComponents;
using OrdersManager.ConsoleUI.ApplicationComponents;
using OrdersManager.Core.Data;
using OrdersManager.Core.Filtering;

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
            builder.RegisterType<RequestProvider>().As<IRequestProvider>();
            builder.RegisterType<FilteringProvider>().As<IFilteringProvider>().InstancePerDependency();
            builder.RegisterType<FilteringService>().As<IFilteringService>();


            builder.RegisterType<MenuService>().As<IMenuService>();
            builder.RegisterType<DataProvider>().As<IDataProvider>();

            builder.RegisterType<OrdersCount>().As<IMenuComponent>();
            builder.RegisterType<OrdersTotalAmount>().As<IMenuComponent>();
            builder.RegisterType<OrdersList>().As<IMenuComponent>();
            builder.RegisterType<OrdersAverageValue>().As<IMenuComponent>();            
            builder.RegisterType<ProductsList>().As<IMenuComponent>();
            builder.RegisterType<OrdersInPriceRange>().As<IMenuComponent>();
            builder.RegisterType<TEST>().As<IMenuComponent>();
            builder.RegisterType<Application>();



            return builder.Build();
        }
    }
}
