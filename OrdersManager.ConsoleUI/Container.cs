﻿using Autofac;
using OrdersManager.ConsoleUI.UIComponents;
using OrdersManager.ConsoleUI.UIServiceComponents;
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
            builder.RegisterType<MemoryRepository>().As<IRepository>().SingleInstance();
            builder.RegisterType<ConsoleLogger>().As<ILogger>().SingleInstance();
            builder.RegisterType<CsvDeserializer>().As<IDeserializer>();
            builder.RegisterType<FilesReader>().As<IFilesReader>().InstancePerLifetimeScope();
            builder.RegisterType<DeserializeService>().As<IDeserializeService>();


            builder.RegisterType<UIService>().As<IUIService>();
            builder.RegisterType<LoadFiles>().As<IUIComponent>();



            return builder.Build();
        }
    }
}
