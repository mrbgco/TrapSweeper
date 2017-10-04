
namespace AtlasCopco.App.Emulator.Console
{
    internal static class SquareDrawer
    {
        private static void DrawLine(int w, char ends, char mids, bool canGoNorth = false, bool canGoSouth = false, bool canGoWest = false, bool canGoEast = false)
        {
            System.Console.Write(ends);
            for (var i = 1; i < w - 1; ++i)
            {
                if ((canGoNorth || canGoSouth) && i == w - 1 / 2)
                    System.Console.Write("│");
                else if (canGoEast && i == w - 1)
                    System.Console.Write("─");
                else if (canGoWest && i == 1)
                    System.Console.Write("─");
                else
                    System.Console.Write(mids);
            }
            System.Console.WriteLine(ends);
        }

        internal static void DrawBox(int width, int height, bool canGoNorth, bool canGoSouth, bool canGoWest, bool canGoEast)
        {
            DrawLine(width, '*', '*', canGoNorth: canGoNorth);
            for (var i = 1; i < height - 1; ++i)
                DrawLine(width, '*', ' ', canGoEast: canGoEast, canGoWest: canGoWest);
            DrawLine(width, '*', '*', canGoSouth: canGoSouth);
        }
    }
}
