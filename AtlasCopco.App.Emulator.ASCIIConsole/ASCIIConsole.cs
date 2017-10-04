using System;
using System.Text;
using AtlasCopco.App.Emulator.Model;
using AtlasCopco.Integration.Maze;

namespace AtlasCopco.App.Emulator.Console
{
    public class ASCIIConsole : IEmulator
    {
        private readonly IMazeIntegration _mazeGenerator;

        public ASCIIConsole(IMazeIntegration mazeGenerator)
        {
            _mazeGenerator = mazeGenerator;
        }

        public ASCIIConsole()
        {
        }

        #region IEmulator Implementation

        public void Initialize()
        {
            System.Console.Clear();
            System.Console.OutputEncoding = Encoding.GetEncoding(866);
            System.Console.SetCursorPosition((System.Console.WindowWidth) / 2, System.Console.CursorTop);
            System.Console.WriteLine("┌─────────────────────────┐");

            System.Console.SetCursorPosition((System.Console.WindowWidth) / 2, System.Console.CursorTop);
            System.Console.WriteLine("│Atlas Copco Treasure Hunt│");

            System.Console.SetCursorPosition((System.Console.WindowWidth) / 2, System.Console.CursorTop);
            System.Console.WriteLine("└─────────────────────────┘");

            System.Console.WriteLine("Welcome to ASCII Console to emulate Treasure Hunting Adventure");
            System.Console.WriteLine("Emulator integrates with any implementation of the IMazeIntegration using Reflection");
            System.Console.WriteLine("Edit configuration file to adjust dll Path and some other nice stuff");
            System.Console.WriteLine("You can close the emulator by pressing (Escape) at any time");
            System.Console.WriteLine();
            ShowMenu();
        }

        public void ShowMenu()
        {
            System.Console.WriteLine("Choose:-");
            System.Console.WriteLine("1. New Maze");
            System.Console.WriteLine("2. Challenge me! (Creates a random maze with size from 2:10)");
        }

        public MazeResult StartNavigation(Hunter hunter)
        {
            var entranceRoomId = -1;

            System.Console.WriteLine("Starting maze navigation.");
            System.Console.WriteLine("Parsing maze metadata..");
            System.Console.WriteLine("Getting entrance room...");
            try
            {
                entranceRoomId = _mazeGenerator.GetEntranceRoom();
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Maze metadata is wrong, no entrance room was found!");
                // log e
                System.Console.ReadLine();
                Environment.Exit(0);
            }


            System.Console.WriteLine("Initialize entrance room....");

            return EmulateMaze(hunter, entranceRoomId);
        }

        public MazeResult EmulateMaze(Hunter hunter, int roomId)
        {
            while (true)
            {
                hunter.StepsCount++;

                // Check if treasure found
                if (_mazeGenerator.HasTreasure(roomId))
                {
                    Celebrate(hunter);
                    return MazeResult.TreasureFound;
                }

                if (_mazeGenerator.CausesInjury(roomId))
                {
                    if (hunter.HealthPoint <= 1)
                    {
                        OfferCondelonces(hunter);
                        return MazeResult.HunterDied;
                    }

                    System.Console.WriteLine("Oj! You have been injured..");
                    System.Console.WriteLine("You suffered a loss of 1 HP");
                    hunter.HealthPoint--;
                }

                DisplayRoomInformation(roomId);
                DisplayHunterStatus(hunter);

                var northRoom = _mazeGenerator.GetRoom(roomId, 'N');
                var canGoNorth = northRoom != null;

                var southRoom = _mazeGenerator.GetRoom(roomId, 'S');
                var canGoSouth = southRoom != null;

                var westRoom = _mazeGenerator.GetRoom(roomId, 'W');
                var canGoWest = westRoom != null;

                var eastRoom = _mazeGenerator.GetRoom(roomId, 'E');
                var canGoEast = eastRoom != null;

                DrawRoom(canGoNorth, canGoSouth, canGoWest, canGoEast);

                System.Console.WriteLine("Choose your destiny..");
                var menuChoice = System.Console.ReadKey(true).Key;
                switch (menuChoice)
                {
                    case ConsoleKey.UpArrow:
                        if (canGoNorth)
                        {
                            System.Console.WriteLine("Going North!");
                            roomId = (int)northRoom;
                            continue;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (canGoSouth)
                        {
                            System.Console.WriteLine("Going South!");
                            roomId = (int)southRoom;
                            continue;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (canGoWest)
                        {
                            System.Console.WriteLine("Going West!");
                            roomId = (int)westRoom;
                            continue;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (canGoEast)
                        {
                            System.Console.WriteLine("Going East!");
                            roomId = (int)eastRoom;
                            continue;
                        }
                        break;
                    default:
                        System.Console.WriteLine("The treasure is important, come on!");
                        continue;
                }
                break;
            }
            return MazeResult.Unknown;
        }

        public void Celebrate(Hunter hunter)
        {
            System.Console.Clear();
            System.Console.WriteLine("Congratulations {0}!", hunter.Name);
            System.Console.WriteLine("You made it to the treasure with {0} steps and {1} health points and proved you are a lucky person..", hunter.StepsCount, hunter.HealthPoint);
            System.Console.WriteLine();
            PlayRandomMusic();
        }

        public void OfferCondelonces(Hunter hunter)
        {
            System.Console.Clear();
            System.Console.WriteLine("{0}, you are Doooooooooooooooooooooooooooomed!", hunter.Name);
            System.Console.WriteLine("You stepped on so many traps and didn't find the treasure..");
            System.Console.WriteLine("Good luck next time!");
            System.Console.WriteLine();
        }

        public void DisplayRoomInformation(int roomId)
        {
            try
            {
                var roomDescription = _mazeGenerator.GetDescription(roomId);
                System.Console.WriteLine("Room is {0}", roomDescription);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Maze metadata is wrong, room description wasn't found!");
                // log e
                System.Console.ReadLine();
                Environment.Exit(0);
            }
        }

        public void DisplayHunterStatus(Hunter hunter)
        {
            if (hunter == null)
            {
                throw new ArgumentNullException(nameof(hunter));
            }

            System.Console.WriteLine("Player:{0}     HP:{1}      Steps:{2}", hunter.Name, hunter.HealthPoint, hunter.StepsCount);
        }

        public void DrawRoom(bool canGoNorth, bool canGoSouth, bool canGoWest, bool canGoEast)
        {
            SquareDrawer.DrawBox(5, 5, canGoNorth, canGoSouth, canGoWest, canGoEast);
        }

        public void NewGame(bool isRandom = false)
        {
            int mazeSize;
            if (isRandom)
            {
                mazeSize = GetRandomMazeSize();
            }
            else
            {
                System.Console.WriteLine("Enter maze size..");
                var strSize = System.Console.ReadLine();
                if (!int.TryParse(strSize, out mazeSize))
                {
                    // :)
                    System.Console.WriteLine("Not a valid value!");
                    System.Console.WriteLine("Enjoy the random maze size as a prize..");
                    mazeSize = GetRandomMazeSize();
                }

            }

            // Build Maze
            try
            {
                System.Console.WriteLine("Generating maze of size {0}", mazeSize);
                _mazeGenerator.BuildMaze(mazeSize);
                System.Console.WriteLine("Maze generation succeeded!");
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Maze generation failed, check log file for details");
                // log e
                System.Console.ReadLine();
                Environment.Exit(0);
            }
        }

        #endregion

        private static int GetRandomMazeSize()
        {
            System.Console.WriteLine("Generating Random maze size..");
            var mazeSizeGenerator = new Random();
            var size = mazeSizeGenerator.Next(2, 10);
            System.Console.WriteLine("Random maze size {0} is generated", size);

            return size;
        }

        private static void PlayRandomMusic()
        {
            var randomSounds = new Random();
            for (var index = 0; index < 50; index++)
            {
                System.Console.Beep(randomSounds.Next(1000) + 100, 100);
            }
        }
    }
}
