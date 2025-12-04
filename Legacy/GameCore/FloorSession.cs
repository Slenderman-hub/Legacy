using Legacy.Enemies;
using Legacy.Items;
using Legacy.Music;
using Legacy.Weapons;
using System;
using Legacy;
using Legacy.Interfaces;

namespace Legacy
{
    public class FloorSession
    {
        public static List<MapEntity> Entities = new List<MapEntity>();

        public static bool FloorIsRunning = false;

        public static char[,] Map { get; set; }

        public bool StartFloor()
        {
            FloorIsRunning = true;
            Legacy.MapWriter.GenerateMap();
            DrawMap();
            
            MusicPlayer.Play();
            
            
            while (FloorIsRunning)
            {
                if (GameSession.Hero.Health <= 0)
                {
                    if (ResurrectionCheck())
                    {
                    }
                    else
                        return false;

                }
                Thread.Sleep(10);
                HeroMove();
                EnemiesMove();
                StatUI.DrawStatUI();

            }
            GameSession.Hero.HeroInventory.Bestiary.Save();
            
            return true;

        }

        private bool ResurrectionCheck()
        {
            foreach (var item in GameSession.Hero.HeroInventory.Weapons)
            {
                if(item is IResurrection resurrect)
                {
                    if (resurrect.Resurrect(GameSession.Hero))
                    {
                        return true;
                    }
                }

            }
            foreach (var item in GameSession.Hero.HeroInventory.Items)
            {
                if (item is IResurrection resurrect)
                {
                    if (resurrect.Resurrect(GameSession.Hero))
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        private void EnemiesMove()
        {
                foreach (var entity in Entities)
                {
                    if(entity is Enemy enemy)
                        enemy.Action((GameSession.Actions)Random.Shared.Next(1, 5));
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
                    GameSession.Hero.Action(GameSession.Actions.Up);
                    break;
                case ConsoleKey.S:
                    GameSession.Hero.Action(GameSession.Actions.Down);
                    break;
                case ConsoleKey.A:
                    GameSession.Hero.Action(GameSession.Actions.Left);
                    break;
                case ConsoleKey.D:
                    GameSession.Hero.Action(GameSession.Actions.Right);
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
                    GameSession.Hero.Action(GameSession.Actions.Swap);
                    break;
                case ConsoleKey.E:
                    GameSession.Hero.Action(GameSession.Actions.Use);
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
                    if (MapWriter.WallIcons.Contains(Map[i, j]))
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    if (Map[i, j] == '&')
                        Console.ForegroundColor = ConsoleColor.Red;
                    if (Map[i,j] == '!')
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    if (Map[i, j] == '#')
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    if (Map[i, j] == '@')
                        Console.ForegroundColor = ConsoleColor.Blue;

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(Map[i, j]);
                    Console.ResetColor();

                }
                Console.WriteLine();
            }
            
        }
        public static void WriteNewPosition(char s, (int x, int y) value, ConsoleColor color = ConsoleColor.White)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            var defaultPos = Console.GetCursorPosition();
            Map[value.y, value.x] = s;
            Console.SetCursorPosition(value.x + GameSession.CenterIndent, value.y);
            Console.ForegroundColor = color;
            Console.Write(Map[value.y, value.x]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(defaultPos.Left, defaultPos.Top);
            Console.ResetColor();
        }
        public static void SwapPosition(MapEntity entity1, MapEntity entity2)
        {
            var temp = entity1.Pos;
            entity1.Pos = entity2.Pos;
            entity2.Pos = temp;
        }
    }
}
