using Legacy.LocationGens;

namespace Legacy
{
    public static class MapGen
    {
        
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
                    CastleGen.InitializeCastle();
                    break;
                case GameSession.Locations.Forest:
                    break;
                default:
                    break;
            }
        }
    }
        

}

