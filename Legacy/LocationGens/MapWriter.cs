using Legacy.LocationGens;
using Legacy.LocationGens.LocationGens;

namespace Legacy
{
    public static class MapWriter
    {
        public static HashSet<Char> WallIcons { get; private set; } = ['|', '│', '─', '└', '┘', '┌', '┐', '├', '┤', '┬', '┴', '┼', '┴', '┬', '┤', '├'];
        public static void GenerateMap()
        {
            FloorSession.Map = new char[GameSession.MAP_HEIGHT, GameSession.MAP_WIDTH];

            for (int y = 0; y < GameSession.MAP_HEIGHT; y++)
            {
                for (int x = 0; x < GameSession.MAP_WIDTH; x++)
                {
                    FloorSession.Map[y, x] = '|';
                }
            }

            switch (GameSession.Location)
            {
                case GameSession.Locations.Castle:
                    var castleGen = new CastleGen();
                    castleGen.GenerateLocation();
                    break;
                case GameSession.Locations.Forest:
                    break;
                default:
                    break;
            }
        }
    }
        

}

