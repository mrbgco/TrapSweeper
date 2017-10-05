namespace AtlasCopco.App.Emulator.Model
{
    public class RoomProperties
    {
        public int? NorthRoom { get; set; }
        public int? SouthRoom { get; set; }
        public int? WestRoom { get; set; }
        public int? EastRoom { get; set; }

        public bool CanGoNorth => NorthRoom != null;

        public bool CanGoSouth => SouthRoom != null;

        public bool CanGoWest => WestRoom != null;

        public bool CanGoEast => EastRoom != null;
    }
}