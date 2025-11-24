using Legacy.Items;
using Legacy.Enemies;
using Legacy.LocationGens;
using Legacy.Weapons;
using static Legacy.GameSession;

namespace Legacy
{
    public static class MapGen
    {
        public static List<Enemy> Enemies = new List<Enemy>();
        public static List<Weapon> Weapons = new List<Weapon>();
        public static List<Chest> Chests = new List<Chest>();

        static int Level;
        public static void GenerateMap(int level, Locations location)
        {
            Enemies = new List<Enemy>();
            Level = level;
            Map = new char[HEIGHT, WIDTH];
            Enemies.Clear();

            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    Map[y, x] = '|';
                }
            }

            switch (location)
            {
                case Locations.Castle:
                    CastleGen.InitializeCastle(Level);
                    break;
                case Locations.Forest:
                    break;
                default:
                    break;
            }
        }
    }
        

}

