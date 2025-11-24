using static Legacy.GameSession;
using Legacy.Enemies;
using Legacy.Music;
using Legacy.Weapons;
using Legacy.Items;

namespace Legacy
{
    public class FloorSession
    {
        public static List<Enemy> Enemies = new List<Enemy>();
        public static List<Weapon> Weapons = new List<Weapon>();
        public static List<Chest> Chests = new List<Chest>();

        public bool FloorIsRunning = true;
        public bool StartFloor()
        {
            MapGen.GenerateMap(Level, Location);
            DrawMap();
            
            Enemies = MapGen.Enemies;
            Weapons = MapGen.Weapons;
            Chests = MapGen.Chests;

            while (FloorIsRunning)
            {
                if (GameSession.Hero.Health <= 0)
                {
                    
                    return false;
                }

                HeroMove();
                Thread.Sleep(10);
                EnemiesMove();

                Console.Write($"            Оружие: {GameSession.Hero.EquipedWeapon.Name}                           Ошеломление:{GameSession.Hero.Stagger}                    HP:{GameSession.Hero.Health}  Золото: {GameSession.Hero.Gold}       \r");
                
            }
            
            
            return true;

        }

        private void EnemiesMove()
        {
            foreach (var enemy in Enemies)
                enemy.Action((Actions)Random.Shared.Next(1, 5));

        }

        public static void HeroMove()
        {
            
            ConsoleKeyInfo key = default;
            while (Console.KeyAvailable)
                key = Console.ReadKey(true);

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
                    if (Map[i, j] == '&')
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    if (Map[i,j] == '!')
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    if (Map[i, j] == '#')
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
