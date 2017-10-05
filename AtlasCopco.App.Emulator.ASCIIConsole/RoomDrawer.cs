
namespace AtlasCopco.App.Emulator.Console
{
    internal static class RoomDrawer
    {
        private static void DrawLine(int w, char start, char middle, char end, char? directionIndicator)
        {
            System.Console.Write(start);
            for (var i = 1; i < w - 1; ++i)
            {
                if (i == (w - 1) / 2)
                    System.Console.Write(directionIndicator);
                else
                    System.Console.Write(middle);
            }
            System.Console.WriteLine(end);
        }

        internal static void DrawBox(int width, int height, bool canGoNorth, bool canGoSouth, bool canGoWest, bool canGoEast)
        {
            // Draw top line, north indicator
            DrawLine(width, '*', '*', '*', canGoNorth ? '|' : '*');

            // Draw all lines, east and west indicator
            for (var i = 1; i < height - 1; ++i)
            {
                if (i == (height - 1) / 2)
                {
                    DrawLine(width, canGoWest ? '─' : '*', ' ', canGoEast ? '─' : '*', ' ');
                }
                else
                {
                    DrawLine(width, '*', ' ', '*', ' ');
                }
            }

            // Draw final line, south indicator
            DrawLine(width, '*', '*', '*', canGoSouth ? '|' : '*');
        }
    }
}
