namespace AtlasCopco.Integration.Maze
{
    /// <summary>
    /// Used for integrating with external Treasure Adventure Emulators.
    /// </summary>
    public interface IMazeIntegration
    {
        /// <summary>
        /// Builds a new randomized maze with the given size. This is always called first.
        /// </summary>
        /// <param name="size">Width and height of maze dimensions.</param>
        void BuildMaze(int size);

        /// <summary>
        /// Gets the ID of the entrance room for the built maze.
        /// </summary>
        /// <returns>ID of the entrance room.</returns>
        int GetEntranceRoom();

        /// <summary>
        /// Gets the room adjacent to the passed room, given a direction. NULL signals invalid room, i.e. edge of the maze.
        /// </summary>
        /// <param name="roomId">ID of the originating room.</param>
        /// <param name="direction">N, S, W or E.</param>
        /// <returns>ID of the room relative to the originating room.</returns>
        int? GetRoom(int roomId, char direction);

        /// <summary>
        /// Gets the computed textual description of the passed room.
        /// </summary>
        /// <param name="roomId">ID of a room.</param>
        /// <returns>Textual room description.</returns>
        string GetDescription(int roomId);
        
        /// <summary>
        /// Checks whether a room has a treasure.
        /// </summary>
        /// <param name="roomId">ID of a room.</param>
        /// <returns>True if the room has a treasure, or false if not.</returns>
        bool HasTreasure(int roomId);

        /// <summary>
        /// Checks whether a room causes injury to the player.
        /// </summary>
        /// <param name="roomId">ID of a room.</param>
        /// <returns>True if the room causes injury (sinking, dehydration, etc), or false if not.</returns>
        bool CausesInjury(int roomId);
    }
}
