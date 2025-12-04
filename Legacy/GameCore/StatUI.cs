using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy
{
    public static class StatUI
    {
        private const int HEIGHT = 11;
        private static string LocationName = "";
        public static void DrawStatUI()
        {
            switch (GameSession.Location)
            {

                default:
                    LocationName = "Замок Лича";
                    break;
            }
            DrawCore();
            DrawStats();
        }

        private static void DrawStats()
        {
            if (GameSession.Hero.Health >= GameSession.Hero.MaxHealth / 2)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;
            DrawStat($"[+]  О.З: [{GameSession.Hero.Health} / {GameSession.Hero.MaxHealth}]", 2);
            Console.ForegroundColor = ConsoleColor.Yellow;
            DrawStat($"[$]  Золото: [{GameSession.Hero.Gold}]", 4);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            DrawStat($"[@]  Этаж: [{GameSession.Level % 1000}]", 6);
            Console.ForegroundColor = ConsoleColor.Cyan;
            DrawStat($"[?]  Степень ошеломления: [{GameSession.Hero.Stagger}]", 8);
            Console.ForegroundColor = ConsoleColor.Magenta;
            DrawStat($"[!]  Оружие: [{GameSession.Hero.EquippedWeapon?.Name}]", 10);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            DrawStat($"[№]  Предмет: [{GameSession.Hero.EquippedItem?.Name ?? "НЕТ"}]", 12);
            Console.ResetColor();

           void DrawStat(string lineUI, int y)
           {
            Console.SetCursorPosition(1, y);
            Console.Write(lineUI.PadRight(GameSession.CenterIndent - 2 - lineUI.Length));
           }

        }
        


        private static void DrawCore()
        {
            // WASD
            // I - инвентарь
            // Пробел - Свап
            // E - использовать предмет

            int nameStart = (GameSession.CenterIndent) / 2 - LocationName.Length;
            for (int i = 0; i < GameSession.CenterIndent - 5; i++)
            {
                Console.SetCursorPosition(i, 0);
                if (i == nameStart - 2)
                {
                    Console.Write('┐');
                    Console.SetCursorPosition(i, 1);
                    Console.Write('└');
                    for (int j = 0; j < LocationName.Length + 2; j++)
                    {
                        i++;
                        Console.Write('─');
                    }
                    
                    Console.Write('┘');
                    i++;
                    Console.SetCursorPosition(i, 0);
                    
                    Console.Write('┌');
                }
                
                if (i == 0)
                    Console.Write("┌");
                if(i == GameSession.CenterIndent - 6)
                    Console.Write("┐");
                else
                {
                    Console.Write("─");
                }
            }
            for (int i = 0; i < GameSession.CenterIndent - 5; i++)
            {
                Console.SetCursorPosition(i, GameSession.MAP_HEIGHT / 4 * 3);
                if (i == 0)
                    Console.Write("└");
                else if (i == GameSession.CenterIndent - 6)
                    Console.Write("┘");
                else
                    Console.Write("─");
            }
            string guideBanner = " [?        ?]";
            for (int i = 1; i < GameSession.CenterIndent - 6; i++)
            {
                Console.SetCursorPosition(i, 18);
                if (i == (GameSession.CenterIndent / 2 - guideBanner.Length + 2))
                {
                    Console.Write(guideBanner);
                    i += guideBanner.Length;

                }
                else
                {
                    Console.Write('-');
                }
            }
            Console.SetCursorPosition(1, 20);
            Console.Write("* [W/A/S/D] - Перемещение");
            Console.SetCursorPosition(1, 22);
            Console.Write("* [Пробел] - Свап");
            Console.SetCursorPosition(1, 24);
            Console.Write("* [I] - Открыть инвентарь");
            Console.SetCursorPosition(1, 26);
            Console.Write("* [E] - Использовать предмет");


            Console.SetCursorPosition(nameStart, 0);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(LocationName.ToUpper());
            Console.ResetColor();

            for (int i = 1; i < GameSession.MAP_HEIGHT/4 * 3; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("│");
                Console.SetCursorPosition(GameSession.CenterIndent - 6, i);
                Console.Write("│");
            }
            
        }
    }
}
