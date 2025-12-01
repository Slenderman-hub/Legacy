using Legacy.Enemies;
using Legacy.Items;
using Legacy.Music;
using Legacy.Weapons;
using System;
using static Legacy.GameSession;

namespace Legacy
{
    public class FloorSession
    {
        public static List<MapEntity> Entities = new List<MapEntity>();

        public static bool FloorIsRunning = false;
        public bool StartFloor()
        {
            //MusicPlayer.Play();
            FloorIsRunning = true;
            MapGen.GenerateMap();
            DrawMap();
            MusicPlayer.Play();
            
            Console.Write($"            Оружие: {GameSession.Hero.EquipedWeapon.Name}     {Console.WindowWidth / 2.95}                      Ошеломление:{GameSession.Hero.Stagger}                    HP:{GameSession.Hero.Health} / {GameSession.Hero.MaxHealth}  Золото: {GameSession.Hero.Gold}      \r");
            while (FloorIsRunning)
            {
                if (GameSession.Hero.Health <= 0)
                {
                    
                    return false;
                }

                Thread.Sleep(10);
                HeroMove();
                EnemiesMove();

                Console.Write($"            Оружие: {GameSession.Hero.EquipedWeapon.Name}     {Console.WindowWidth}                      Ошеломление:{GameSession.Hero.Stagger}                    HP:{GameSession.Hero.Health} / {GameSession.Hero.MaxHealth}  Золото: {GameSession.Hero.Gold}      \r");
                
                
            }
            
            
            return true;

        }

        private void EnemiesMove()
        {
                foreach (var entity in Entities)
                {
                    if(entity is Enemy enemy)
                        enemy.Action((Actions)Random.Shared.Next(1, 5));
                }
        }

        public static void HeroMove()
        {
            if (GameSession.Hero.Health <= 0)
                return;
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
                case ConsoleKey.R:
                    GameSession.InvInterface.OpenInterface(InventoryInterface.TabTypes.Weapons);
                    break;
                case ConsoleKey.J:
                    GameSession.InvInterface.OpenInterface(InventoryInterface.TabTypes.Bestiary);
                    break;
                case ConsoleKey.I:
                    GameSession.InvInterface.OpenInterface(InventoryInterface.TabTypes.Items);
                    break;
                case ConsoleKey.Spacebar:
                    GameSession.Hero.Action(Actions.Swap);
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
            Console.Clear();
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                foreach (var item in Enumerable.Range(0, (int)(Console.WindowWidth / 2.95)))
                    Console.Write(" ");

                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == '|' && Location == Locations.Castle)
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    if (Map[i, j] == '&')
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    if (Map[i,j] == '!')
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    if (Map[i, j] == '#')
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    if (Map[i, j] == '@')
                        Console.ForegroundColor = ConsoleColor.Cyan;
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
            Console.SetCursorPosition(value.x + MAP_INDENT, value.y);
            Console.ForegroundColor = color;
            Console.Write(Map[value.y, value.x]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(defaultPos.Left, defaultPos.Top);
        }
        public static void SwapPosition(MapEntity entity1, MapEntity entity2)
        {
            var temp = entity1.Pos;
            entity1.Pos = entity2.Pos;
            entity2.Pos = temp;
        }
    }
}
