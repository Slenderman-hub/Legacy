using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legacy.Enemies;
using static Legacy.GameSession;
using  Legacy.LocationGens;

namespace Legacy
{
    public static class MapGen
    {
        public static List<Enemy> Enemies = new List<Enemy>();
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

