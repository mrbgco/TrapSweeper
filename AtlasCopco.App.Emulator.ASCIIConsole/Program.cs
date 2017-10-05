using System;
using System.Threading;
using AtlasCopco.Integration.Maze;
using Autofac;

namespace AtlasCopco.App.Emulator.Console
{
    internal class Program
    {
        private static void Main()
        {
            // Thread to get (Escape) to exit application at anytime
            var closeConsole = new Thread(StartEmulator);
            closeConsole.SetApartmentState(ApartmentState.STA);
            closeConsole.Start();

            while (System.Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
                // do nothing until escape
            }

            Environment.Exit(0);
        }

        private static void StartEmulator()
        {
            // Autofac
            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var consolerEmulator = scope.Resolve<IEmulator>();
                consolerEmulator.EmulateMain();
            }
        }
    }
}
