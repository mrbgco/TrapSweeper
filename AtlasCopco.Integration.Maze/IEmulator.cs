using AtlasCopco.App.Emulator.Model;

namespace AtlasCopco.Integration.Maze
{
    /// <summary>
    /// Standard for any type of Maze emulator
    /// </summary>
    public interface IEmulator
    {
        /// <summary>
        /// Main method to run the emulator
        /// </summary>
        void EmulateMain();

        /// <summary>
        /// Initialize emulator by preparing and displaying information
        /// </summary>
        void Initialize();

        /// <summary>
        /// Start maze navigating from entrance room
        /// </summary>
        /// <param name="hunter">Current exploring hunter</param>
        /// <returns>Enum indicating result</returns>
        MazeResult StartNavigation(Hunter hunter);

        /// <summary>
        /// /// Go in maze rooms until you die, find treasure or keep roaming eternally
        /// </summary>
        /// <param name="hunter">Current exploring hunter</param>
        /// <param name="roomId">Current room Id</param>
        /// <returns>Enum indicating result</returns>
        MazeResult EmulateMaze(Hunter hunter, int roomId);

        /// <summary>
        /// Display some text and play random music, celebrating find the treasure
        /// </summary>
        /// <param name="hunter">Current exploring hunter</param>
        void Celebrate(Hunter hunter);

        /// <summary>
        /// Display some text to offer condelonces for hunter death from traps
        /// </summary>
        void OfferCondelonces(Hunter hunter);

        /// <summary>
        /// Display room information
        /// </summary>
        /// <param name="roomId">Required room id</param>
        void DisplayRoomInformation(int roomId);

        /// <summary>
        /// Display hunter information
        /// </summary>
        /// <param name="hunter"></param>
        void DisplayHunterStatus(Hunter hunter);

        /// <summary>
        /// Draw square room with signs for available directions
        /// </summary>
        /// <param name="canGoNorth">Can go north from this room</param>
        /// <param name="canGoSouth">Can go south from this room</param>
        /// <param name="canGoWest">Can go west from this room</param>
        /// <param name="canGoEast">Can go east from this room</param>
        void DrawRoom(bool canGoNorth, bool canGoSouth, bool canGoWest, bool canGoEast);

        /// <summary>
        /// Start a new game
        /// </summary>
        /// <param name="isRandom">Indicates to create a random size for maze from 2:10</param>
        void NewGame(bool isRandom = false);

        /// <summary>
        /// Show selection menu
        /// </summary>
        void ShowMenu();
    }
}
