

namespace Legacy
{
    public class MapEntity
    {
        public char Icon { get; protected set; }
        public (int x, int y) Pos { get; set; }
    }
    public static class GameSession
    {
        public const int WIDTH = 100; 
        public const int HEIGHT = 50; 
        public static bool GameIsRunning { get; set; } = false;

        public static char[,] Map;
        public static Hero Hero = new Hero();
        public static int Level = 11;
        public static Locations Location = Locations.Castle;
        public enum Actions
        {
            Up = 1,
            Down = 2,
            Left = 3,
            Right = 4,

            Inventory,
            Exit,
            Use
        }
        public enum Locations
        {
            Castle,
            Forest
        }

        public static bool StartSession()
        {
            GameIsRunning = true;
            while (GameIsRunning)
            {
                FloorSession  floor = new FloorSession();
                bool result = floor.StartFloor();
                if (!result)
                    return false;

                if (result && Level == 24)
                    return true;

            }
            return false;
            

        }




    }
}
