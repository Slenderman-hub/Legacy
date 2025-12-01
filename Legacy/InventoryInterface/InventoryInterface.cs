using Legacy.Items;
using Legacy.Weapons;

namespace Legacy
{
    public class InventoryInterface
    {
        private readonly int WIDTH = Console.WindowWidth - (Console.WindowWidth / 9);
        private readonly int HEIGHT = Console.WindowHeight - (Console.WindowHeight / 4);

        private readonly List<Weapon> Weapons = GameSession.Hero.HeroInventory.Weapons;
        private readonly List<Item> Items = GameSession.Hero.HeroInventory.Items;

        private int _selectedIndex = 0;
        private int _scrollOffset = 0;
        private int _itemsPerPage = 0;

        public enum TabTypes
        {
            Weapons,
            Items,
            Bestiary,
            Save
            
        }
        private TabTypes TabType;
        public void OpenInterface(TabTypes tabType)
        {
            TabType = tabType;
            _itemsPerPage = HEIGHT / 3;
            Console.Clear();
            switch (TabType)
            {
                case TabTypes.Weapons:
                    OpenWeapons();
                    break;
                case TabTypes.Items:
                    OpenItems();
                    break;
                case TabTypes.Bestiary:
                    OpenBestiary();
                    break;
                case TabTypes.Save:
                    //TODO SaveGame()
                    break;
            }
            FloorSession.DrawMap();

        }
        private void OpenWeapons()
        {
            DrawCore();
            DrawTabs();
            _selectedIndex = 0;
            _scrollOffset = 0;
            DrawWeapons();
            while (true)
            {
                switch (GameSession.UserInput(50))
                {
                    case ConsoleKey.Escape:
                        return;
                    case ConsoleKey.J:
                        OpenBestiary();
                        return;
                    case ConsoleKey.I:
                        OpenItems();
                        return;
                    case ConsoleKey.L:
                        //Save();
                        return;
                    case ConsoleKey.W:
                        if (_selectedIndex > 0)
                        {
                            _selectedIndex--;
                            if (_selectedIndex < _scrollOffset)
                                _scrollOffset = _selectedIndex;
                            DrawWeapons();
                        }
                        break;
                    case ConsoleKey.S:
                        if (_selectedIndex < Weapons.Count - 1)
                        {
                            _selectedIndex++;
                            if (_selectedIndex >= _scrollOffset + _itemsPerPage)
                                _scrollOffset = _selectedIndex - _itemsPerPage + 1;
                            DrawWeapons();
                        }
                        break;
                    case ConsoleKey.Enter:
                        GameSession.Hero.EquipedWeapon = Weapons[_selectedIndex];
                        DrawWeapons();
                        break;


                }
            }
            
        }

        private void DrawWeapons()
        {
            
            for (int i = 0; i < _itemsPerPage; i++)
            {
                int itemIndex = _scrollOffset + i;
                if (itemIndex < Weapons.Count)
                {
                    if (itemIndex == _selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.SetCursorPosition(1, (i + 5) + i);

                    var weapon = Weapons[itemIndex];
                    string mark = GameSession.Hero.EquipedWeapon == weapon ? "[X]" : "[ ]";
                    string result = $"{mark} {weapon.Name}";
                    Console.Write(result.PadRight(WIDTH / 2 - 2 - result.Length));
                    Console.ResetColor();
                    //Имя
                    Console.SetCursorPosition(WIDTH / 2 + 1, HEIGHT / 12);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(Weapons[_selectedIndex].Name.ToUpper().PadLeft((3 * WIDTH / 16) + (3 * WIDTH / 16) / 3));
                    //Урон
                    Console.SetCursorPosition(WIDTH / 2 + 1, HEIGHT / 12 * 3);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Урон :  ".ToUpper().PadLeft(10) + $"{Weapons[_selectedIndex].Damage}".PadRight(10));
                    
                    //Описание предмета:
                    int startDescr = HEIGHT / 12 * 4;
                    Console.SetCursorPosition(WIDTH / 2 + 1, startDescr);
                    var descr = "  ОПИСАНИЕ: " + Weapons[_selectedIndex].Description.PadRight(WIDTH * 2);
                    
                    if (descr.Length > WIDTH / 2 - 10)
                    {
                        for (int j = 0; j < descr.Length; j++)
                        {
                            Console.Write(descr[j]);
                            if (j % (WIDTH / 2 - 10) == 0)
                            {
                                startDescr += 2;
                                Console.SetCursorPosition(WIDTH / 2 + 1, startDescr);
                            }
                        }
                        
                    }
                    else
                    {
                        Console.Write(descr);
                    }
                    //Описание способности
                    var curr = Console.GetCursorPosition();
                    curr.Top += 1;
                    Console.SetCursorPosition(WIDTH / 2 + 1, curr.Top);
                    var special = $"  [ОСОБЕННОСТЬ]: {Weapons[_selectedIndex].Special}".PadRight(WIDTH*2);

                    if (special.Length > WIDTH / 2 - 10)
                    {
                        for (int j = 0; j < special.Length; j++)
                        {
                            Console.Write(special[j]);
                            if (j % (WIDTH / 2 - 10) == 0)
                            {
                                curr.Top += 2;
                                Console.SetCursorPosition(WIDTH / 2 + 1, curr.Top);
                            }
                        }

                    }
                    else
                    {
                        Console.Write(special);
                    }


                    //Левая нижняя подсказка
                    Console.ResetColor();
                    Console.SetCursorPosition(3, HEIGHT - 3);
                    Console.Write($"[{itemIndex+ 1 - _scrollOffset}/{Weapons.Count}] │ [W/S] - Листать Вверх/Вниз │ [Enter] - Экипировать");
                }
            }
        }

        private void OpenBestiary()
        {
            throw new NotImplementedException();
        }

        private void OpenItems()
        {
            throw new NotImplementedException();
        }


        private void DrawCore()
        {
            for (int i = 3; i < HEIGHT; i++)
            {
                Console.SetCursorPosition(0, i);

                if (i == HEIGHT - 1)
                    Console.Write("└");
                else
                    Console.Write("│");

                Console.SetCursorPosition(WIDTH / 2, i);
                if (i == HEIGHT - 1)
                    Console.Write("┴");
                else
                    Console.Write("│");

                Console.SetCursorPosition(WIDTH, i);
                if (i == HEIGHT - 1)
                    Console.Write("┘");
                else
                    Console.Write("│");
            }
            for (int i = 1; i < WIDTH; i++)
            {
                Console.SetCursorPosition(i, HEIGHT - 1);
                if (i == WIDTH / 2 || i == WIDTH)
                    continue;
                Console.Write("─");


            }
            for (int i = 0; i <= WIDTH; i++)
            {
                if (i == 0)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("┌");
                    Console.SetCursorPosition(i, 1);
                    Console.Write("│");
                    Console.SetCursorPosition(i, 2);
                    Console.Write("├");
                }
                else if (i == WIDTH / 4)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("┬");
                    Console.SetCursorPosition(i, 1);
                    Console.Write("│");
                    Console.SetCursorPosition(i, 2);
                    Console.Write("┴");
                }
                else if (i == WIDTH / 2)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("┬");
                    Console.SetCursorPosition(i, 1);
                    Console.Write("│");
                    Console.SetCursorPosition(i, 2);
                    Console.Write("┼");
                }
                else if (i == 3 * WIDTH / 4)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("┬");
                    Console.SetCursorPosition(i, 1);
                    Console.Write("│");
                    Console.SetCursorPosition(i, 2);
                    Console.Write("┴");
                }
                else if (i == WIDTH)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("┐");
                    Console.SetCursorPosition(i, 1);
                    Console.Write("│");
                    Console.SetCursorPosition(i, 2);
                    Console.Write("┤");
                }
                else
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("─");
                    Console.SetCursorPosition(i, 2);
                    Console.Write("─");
                }

                if (i - 1 > WIDTH / 2)
                {
                    Console.SetCursorPosition(i - 1, HEIGHT / 6);
                    Console.Write("─");
                }

                if (i + 1 < WIDTH / 2)
                {
                    Console.SetCursorPosition(i + 1, HEIGHT - 5);
                    Console.Write("─");
                }

            }
        }
        private void DrawTabs()
        {
            if (TabType == TabTypes.Weapons)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(1, 1);
            Console.Write("ОРУЖИЕ [R]".PadLeft(WIDTH / 8));
            if (TabType == TabTypes.Items)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(WIDTH / 4 + 1, 1);
            Console.Write("ПРЕДМЕТЫ [I]".PadLeft(WIDTH / 7));
            if (TabType == TabTypes.Bestiary)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(WIDTH / 2 + 1, 1);
            Console.Write("БЕСТИАРИЙ [J]".PadLeft(2 * WIDTH / 14));
            if (TabType == TabTypes.Save)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(3 * WIDTH / 4 + 1, 1);
            Console.Write("СОХРАНИТЬ И ВЫЙТИ [L]".PadLeft(3 * WIDTH / 24));


        }


    }
}

