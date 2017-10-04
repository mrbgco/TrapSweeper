using System;
using System.Threading;
using AtlasCopco.App.Emulator.Model;
using AtlasCopco.Integration.Maze;

namespace AtlasCopco.App.Emulator.Console
{
    internal class Program
    {
        private static void Main()
        {
            // Thread to get Escape charachter pressing to exit
            var closeConsole = new Thread(Emulate);
            closeConsole.SetApartmentState(ApartmentState.STA);
            closeConsole.Start();
            while (System.Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
                // do nothing until escape
            }

            Environment.Exit(0);
        }

        private static void Emulate()
        {
            IEmulator consolerEmulator = new ASCIIConsole();
            consolerEmulator.Initialize();


            var hunter = new Hunter
            {
                HealthPoint = 2, // Read from config
                StepsCount = 0,
                Name = "X" // Get from input somewhere
            };

            while (!System.Console.KeyAvailable)
            {
                // Load dll dynamically
                // Temporarily use a dummy object to draw the skeleton
                // IMazeIntegration mazeGenerator;

                var menuChoice = System.Console.ReadKey(true).Key;

                switch (menuChoice)
                {
                    case ConsoleKey.D1:
                        consolerEmulator.NewGame();
                        consolerEmulator.StartNavigation(hunter);
                        break;
                    case ConsoleKey.D2:
                        consolerEmulator.NewGame(true);
                        consolerEmulator.StartNavigation(hunter);
                        break;
                    default:
                        System.Console.WriteLine("Not a valid choice!");
                        continue;
                }

                consolerEmulator.ShowMenu();
            }
        }
    }
}
