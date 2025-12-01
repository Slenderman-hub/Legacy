using Legacy.LocationGens;
using static Legacy.GameSession;

namespace Legacy
{
    public static class MapGen
    {
        public static List<(int x, int y)> FreeCells = new List<(int x, int y)>();
        public static void GenerateMap()
        {
            Map = new char[MAP_HEIGHT, MAP_WIDTH];

            for (int y = 0; y < MAP_HEIGHT; y++)
            {
                for (int x = 0; x < MAP_WIDTH; x++)
                {
                    Map[y, x] = '|';
                }
            }

            switch (Location)
            {
                case Locations.Castle:
                    CastleGen.InitializeCastle();
                    break;
                case Locations.Forest:
                    break;
                default:
                    break;
            }
        }
    }
        

}

