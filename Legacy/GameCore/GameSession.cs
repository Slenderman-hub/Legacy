

using System;

namespace Legacy
{
    public class MapEntity
    {
        public char Icon { get; set; } 
        public (int x, int y) Pos { get; set; }
        public ConsoleColor IconColor { get; set; }
    }

    public static class GameSession
    {
        public static InventoryInterface InvInterface { get; private set; }
        public const int MAP_WIDTH = 100; 
        public const int MAP_HEIGHT = 50;
        public static readonly int CenterIndent = (int)(Console.WindowWidth / 2.95);
        public static bool GameIsRunning { get; set; } = false;

        public static Hero Hero = new Hero();
        public static int Level = 1;
        public static Locations Location = Locations.Castle;

        //public static char[,] Map;
        public enum Actions
        {
            Up = 1,
            Down,
            Left,
            Right,
            Swap,
            Inventory,
            Use,
            Exit,
        }
        public enum Locations
        {
            Castle,
            Forest
        }

        public static bool StartSession()
        {
            Console.CursorVisible = false;
            InvInterface = new InventoryInterface();
            GameIsRunning = true;
            while (GameIsRunning)
            {
                FloorSession  floor = new FloorSession();
                bool result = floor.StartFloor();
                if (!result)
                    return false;

            }
            return false;
            

        }
        public static ConsoleKey UserInput(int milliseconds)
        {
            Thread.Sleep(milliseconds);
            ConsoleKeyInfo key = default;
            while (Console.KeyAvailable)
                key = Console.ReadKey(true);
            return key.Key;
        }




    }
}
