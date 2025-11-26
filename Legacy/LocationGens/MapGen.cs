using Legacy.LocationGens;
using static Legacy.GameSession;

namespace Legacy
{
    public static class MapGen
    {
        //public static List<Enemy> Enemies = new List<Enemy>();
        //public static List<Weapon> Weapons = new List<Weapon>();
        //public static List<Chest> Chests = new List<Chest>();
        public static List<(int x, int y)> FreeCells = new List<(int x, int y)>();
        public static void GenerateMap()
        {
            Map = new char[HEIGHT, WIDTH];

            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
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

