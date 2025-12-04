using Legacy.Enemies;
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
        private readonly Dictionary<string, Enemy> Bestiary = GameSession.Hero.HeroInventory.Bestiary.Creatures;

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
            _itemsPerPage = 5;
            
            switch (TabType)
            {
                case TabTypes.Weapons:
                    Open(DrawWeapons);
                    break;
                case TabTypes.Items:
                    Open(DrawItems);
                    break;
                case TabTypes.Bestiary:
                    Open(DrawBestiary);
                    break;
                case TabTypes.Save:
                    //TODO SaveGame()
                    break;
            }
            FloorSession.DrawMap();

        }
        private void Open(Action drawSomething)
        {
            Console.Clear();
            DrawCore();
            DrawTabs();
            _selectedIndex = 0;
            _scrollOffset = 0;
            drawSomething();
            while (true)
            {
                switch (GameSession.UserInput(10))
                {
                    case ConsoleKey.Escape:
                        return;
                    case ConsoleKey.J:
                        TabType = TabTypes.Bestiary;
                        Open(DrawBestiary);
                        return;
                    case ConsoleKey.I:
                        TabType = TabTypes.Items;
                        Open(DrawItems);
                        return;
                    case ConsoleKey.R:
                        TabType = TabTypes.Weapons;
                        Open(DrawWeapons);
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
                            drawSomething();
                        }
                        break;
                    case ConsoleKey.S:
                        int count = 0;
                        if (TabType == TabTypes.Weapons)
                            count = Weapons.Count - 1;
                        if (TabType == TabTypes.Items)
                            count = Items.Count - 1;

                        if (_selectedIndex < count)
                            {
                                _selectedIndex++;
                                if (_selectedIndex >= _scrollOffset + _itemsPerPage)
                                    _scrollOffset = _selectedIndex - _itemsPerPage + 1;
                                drawSomething();
                            }

                        break;
                    case ConsoleKey.Enter:
                        if(TabType == TabTypes.Weapons)
                            GameSession.Hero.EquippedWeapon = Weapons[_selectedIndex];
                        if (TabType == TabTypes.Items)
                            GameSession.Hero.EquippedItem = Items[_selectedIndex];
                        drawSomething();
                        break;


                }
            }
        }

        private void DrawBestiary()
        {
            throw new NotImplementedException();
        }

        private void DrawItems()
        {
            TabType = TabTypes.Items;
            int highlightWidth = WIDTH / 2 - 2; 

            //Список
            for (int i = 0; i < _itemsPerPage; i++)
            {
                int itemIndex = _scrollOffset + i;
                int cursorY = (i + 5) + i;

                Console.SetCursorPosition(1, cursorY);

                if (itemIndex < Items.Count)
                {
                    
                    if (itemIndex == _selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.ResetColor();
                    }

                    var item = Items[itemIndex];
                    string mark = GameSession.Hero.EquippedItem == item ? "(X)" : "( )";
                    string itemText = $"{mark} {item.Name}";

                    
                    if (itemText.Length > highlightWidth)
                    {
                        itemText = itemText.Substring(0, highlightWidth);
                    }

                    
                    Console.Write(itemText);

                    
                    if (itemText.Length < highlightWidth)
                    {
                        Console.Write(new string(' ', highlightWidth - itemText.Length));
                    }

                    Console.ResetColor();
                }
                else
                {
                    
                    Console.Write(new string(' ', highlightWidth));
                }
            }


            // Панель описания
            if (_selectedIndex >= 0 && _selectedIndex < Items.Count)
            {
                int rightPanelStartX = WIDTH / 2 + 1;
                int rightPanelWidth = WIDTH / 2 - 2;

                for (int row = HEIGHT / 12; row < HEIGHT / 6; row++)
                {
                    Console.SetCursorPosition(rightPanelStartX, row);
                    Console.Write(new string(' ', rightPanelWidth));
                }
                for (int row = HEIGHT / 6 + 1; row < HEIGHT - 3; row++)
                {
                    Console.SetCursorPosition(rightPanelStartX, row);
                    Console.Write(new string(' ', rightPanelWidth));
                }

                // Имя предмета 
                string name = Items[_selectedIndex].Name.ToUpper();
                int nameX = rightPanelStartX + (rightPanelWidth - name.Length) / 2;
                Console.SetCursorPosition(nameX, HEIGHT / 12);
                Console.ForegroundColor = Items[_selectedIndex].InventoryColor;
                Console.Write(name);

                // Описание
                int descY = HEIGHT / 6 + 4;
                Console.SetCursorPosition(rightPanelStartX, descY);
                Console.ResetColor();
                string descr = "ОПИСАНИЕ: " + Items[_selectedIndex].Description;

                
                int currentDescrLine = 0;
                for (int j = 0; j < descr.Length; j++)
                {
                    Console.Write(descr[j]);
                    if ((j + 1) % (rightPanelWidth) == 0)
                    {
                        currentDescrLine++;
                        Console.SetCursorPosition(rightPanelStartX, descY + currentDescrLine);
                    }
                }

            }

            // Подсказка в левом нижнем углу.
            Console.ResetColor();
            Console.SetCursorPosition(3, HEIGHT - 3);
            Console.Write($"[{_selectedIndex + 1}/{Items.Count}] │ [W/S] - Листать Вверх/Вниз │ [Enter] - Экипировать");
        }

        private void DrawWeapons()
        {
            TabType = TabTypes.Weapons;

            
            int highlightWidth = WIDTH / 2 - 2; 

            for (int i = 0; i < _itemsPerPage; i++)
            {
                int itemIndex = _scrollOffset + i;
                int cursorY = (i + 5) + i;

                Console.SetCursorPosition(1, cursorY);

                if (itemIndex < Weapons.Count)
                {
                    
                    if (itemIndex == _selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.ResetColor();
                    }

                    var weapon = Weapons[itemIndex];
                    string mark = GameSession.Hero.EquippedWeapon == weapon ? "[X]" : "[ ]";
                    string itemText = $"{mark} {weapon.Name}";

                    
                    if (itemText.Length > highlightWidth)
                    {
                        itemText = itemText.Substring(0, highlightWidth);
                    }

                    
                    Console.Write(itemText);

                    
                    if (itemText.Length < highlightWidth)
                    {
                        Console.Write(new string(' ', highlightWidth - itemText.Length));
                    }

                    Console.ResetColor();
                }
                else
                {
                    
                    Console.Write(new string(' ', highlightWidth));
                }
            }

            
            // Правая панель с деталями предмета
            if (_selectedIndex >= 0 && _selectedIndex < Weapons.Count)
            {
                int rightPanelStartX = WIDTH / 2 + 1;
                int rightPanelWidth = WIDTH / 2 - 2;

                
                for (int row = HEIGHT / 12; row < HEIGHT / 6; row++)
                {
                    Console.SetCursorPosition(rightPanelStartX, row);
                    Console.Write(new string(' ', rightPanelWidth));
                }

                
                for (int row = HEIGHT / 6 + 1; row < HEIGHT - 3; row++)
                {
                    Console.SetCursorPosition(rightPanelStartX, row);
                    Console.Write(new string(' ', rightPanelWidth));
                }

                
                string name = Weapons[_selectedIndex].Name.ToUpper();
                int nameX = rightPanelStartX + (rightPanelWidth - name.Length) / 2;
                Console.SetCursorPosition(nameX, HEIGHT / 12);
                Console.ForegroundColor = Weapons[_selectedIndex].InventoryColor;
                Console.Write(name);

                
                Console.SetCursorPosition(rightPanelStartX, HEIGHT / 6 + 2);
                Console.ForegroundColor = ConsoleColor.Yellow;
                string damageText = $"УРОН: {Weapons[_selectedIndex].Damage}";
                int damageX = rightPanelStartX + (rightPanelWidth - damageText.Length) / 2;
                Console.SetCursorPosition(damageX, HEIGHT / 6 + 2);
                Console.Write(damageText);

                
                int descY = HEIGHT / 6 + 4;
                Console.SetCursorPosition(rightPanelStartX, descY);
                Console.ResetColor();
                string descr = "ОПИСАНИЕ: " + Weapons[_selectedIndex].Description;

                
                int currentDescrLine = 0;
                for (int j = 0; j < descr.Length; j++)
                {
                    Console.Write(descr[j]);
                    if ((j + 1) % (rightPanelWidth) == 0)
                    {
                        currentDescrLine++;
                        Console.SetCursorPosition(rightPanelStartX, descY + currentDescrLine);
                    }
                }

                
                int specialY = descY + currentDescrLine + 2;
                Console.SetCursorPosition(rightPanelStartX, specialY);
                string special = $"[ОСОБЕННОСТЬ]: {Weapons[_selectedIndex].Special}";

                
                int currentSpecialLine = 0;
                for (int j = 0; j < special.Length; j++)
                {
                    Console.Write(special[j]);
                    if ((j + 1) % (rightPanelWidth) == 0)
                    {
                        currentSpecialLine++;
                        Console.SetCursorPosition(rightPanelStartX, specialY + currentSpecialLine);
                    }
                }
            }

            
            Console.ResetColor();
            Console.SetCursorPosition(3, HEIGHT - 3);
            Console.Write($"[{_selectedIndex + 1}/{Weapons.Count}] │ [W/S] - Листать Вверх/Вниз │ [Enter] - Экипировать");
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

