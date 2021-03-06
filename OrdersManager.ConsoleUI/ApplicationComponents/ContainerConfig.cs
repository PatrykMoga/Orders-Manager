﻿using Autofac;
using OrdersManager.ConsoleUI.MenuComponents;
using OrdersManager.ConsoleUI.MenuItems;
using OrdersManager.Core.Data;
using OrdersManager.Core.Deserializers;
using OrdersManager.Core.FilesProcessing;
using OrdersManager.Core.Filtering;
using OrdersManager.Core.Logs;
using OrdersManager.Core.Repository;

namespace OrdersManager.ConsoleUI.ApplicationComponents
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<DataProvider>().As<IDataProvider>();

            builder.RegisterType<FilesReader>().As<IFilesReader>().InstancePerLifetimeScope();
            builder.RegisterType<MemoryRepository>().As<IRepository>().SingleInstance();
            builder.RegisterType<ConsoleLogger>().As<ILogger>().SingleInstance();
            builder.RegisterType<DeserializingService>().As<IDeserializingService>();
            builder.RegisterType<CsvDeserializer>().As<IDeserializer>().SingleInstance();
            builder.RegisterType<XmlDeserializer>().As<IDeserializer>().SingleInstance();
            builder.RegisterType<JsonDeserializer>().As<IDeserializer>().SingleInstance();

            builder.RegisterType<RequestProvider>().As<IRequestProvider>();
            builder.RegisterType<FilterProvider>().As<IFilterProvider>().SingleInstance();
            builder.RegisterType<FilterService>().As<IFilterService>();

            builder.RegisterType<MainMenu>().As<IMainMenu>();

            builder.RegisterType<OrdersCount>().As<IMenuItem>();
            builder.RegisterType<OrdersTotalAmount>().As<IMenuItem>();
            builder.RegisterType<OrdersList>().As<IMenuItem>();
            builder.RegisterType<OrdersAverageValue>().As<IMenuItem>();
            builder.RegisterType<ProductsList>().As<IMenuItem>();
            builder.RegisterType<OrdersInPriceRange>().As<IMenuItem>();

            return builder.Build();
        }
    }
}
