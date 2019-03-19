using Autofac;

namespace OrdersManager.ConsoleUI
{
    internal static class Program
    {
        private static void Main()
        {
            var container = Container.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<Application>();
                app.Run();
            }
        }
    }
}
