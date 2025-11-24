using static Legacy.GameSession;
using Legacy.Enemies;
using Legacy.Music;

namespace Legacy
{
    public class FloorSession
    {
        public static List<Enemy> Enemies = new List<Enemy>();
        public bool FloorIsRunning = true;
        public bool StartFloor()
        {
            MapGen.GenerateMap(Level, Location);
            DrawMap();
            MusicPlayer.Location(Location);
            
            Enemies = MapGen.Enemies;

            while (FloorIsRunning)
            {
                if (GameSession.Hero.Health <= 0)
                {
                    MusicPlayer.Stop();
                    return false;
                }

                HeroMove();
                EnemiesMove();

                Console.Write($"                                                        HP:{GameSession.Hero.Health}        \r");
                
            }
            MusicPlayer.Stop();
            return true;

        }

        private void EnemiesMove()
        {
            foreach (var enemy in Enemies)
                enemy.Action((Actions)Random.Shared.Next(1, 5));

        }

        public static void HeroMove()
        {
            var userInput = Console.ReadKey(true).Key;
            switch (userInput)
            {
                case ConsoleKey.W:
                    GameSession.Hero.Action(Actions.Up);
                    break;
                case ConsoleKey.S:
                    GameSession.Hero.Action(Actions.Down);
                    break;
                case ConsoleKey.A:
                    GameSession.Hero.Action(Actions.Left);
                    break;
                case ConsoleKey.D:
                    GameSession.Hero.Action(Actions.Right);
                    break;
                case ConsoleKey.I:
                    GameSession.Hero.HeroInventory.DisplayInventory();
                    break;
                case ConsoleKey.Escape:
                    GameSession.Hero.HeroInventory.DisplayInventory();
                    break;
                default:
                    break;
            }
        }
        public static void DrawMap()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == '|' && Location == Locations.Castle)
                        Console.ForegroundColor = ConsoleColor.Gray;
                    if (Map[i, j] == 'I')
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(Map[i, j]);

                    Console.ForegroundColor = ConsoleColor.White;

                }
                Console.WriteLine();
            }
        }
        public static void WriteNewPosition(char s, (int x, int y) value, ConsoleColor color = ConsoleColor.White)
        {
            var defaultPos = Console.GetCursorPosition();
            Map[value.y, value.x] = s;
            Console.SetCursorPosition(value.x, value.y);
            Console.ForegroundColor = color;
            Console.Write(Map[value.y, value.x]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(defaultPos.Left, defaultPos.Top);
        }
    }
}
