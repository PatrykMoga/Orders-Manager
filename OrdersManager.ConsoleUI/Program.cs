using Autofac;
using OrdersManager.ConsoleUI.ApplicationComponents;

namespace OrdersManager.ConsoleUI
{
    internal static class Program
    {
        private static void Main()
        {
            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.Start();
            }
        }
    }
}
