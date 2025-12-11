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
        private List<Enemy> Bestiary = new List<Enemy>();

        private int _selectedIndex = 0;
        private int _scrollOffset = 0;
        private int _itemsPerPage = 0;
        public InventoryInterface()
        {
            
        }
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
            
            Bestiary.Clear();
            foreach (var pair in GameSession.Hero.HeroInventory.Bestiary.Creatures)
                 Bestiary.Add(pair.Value);

            Bestiary.OrderBy(e => e.Name);


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
            GameSession.Logger.DrawLoggerUI();

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
                    case ConsoleKey.Y:
                        TabType = TabTypes.Bestiary;
                        Open(DrawBestiary);
                        return;
                    case ConsoleKey.T:
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
                        if (TabType == TabTypes.Bestiary)
                            count = Bestiary.Count - 1;

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
            TabType = TabTypes.Bestiary;
            int highlightWidth = WIDTH / 2 - 2;

            //Список
            for (int i = 0; i < _itemsPerPage; i++)
            {
                int itemIndex = _scrollOffset + i;
                int cursorY = (i + 5) + i;

                Console.SetCursorPosition(1, cursorY);

                if (itemIndex < Bestiary.Count)
                {

                    if (itemIndex == _selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                        Console.ResetColor();

                    var item = Bestiary[itemIndex];
                    string mark = $"{{{i + 1}}}";
                    string itemText = $"{mark} {item.Name}";


                    if (itemText.Length > highlightWidth)
                        itemText = itemText.Substring(0, highlightWidth);


                    Console.Write(itemText);


                    if (itemText.Length < highlightWidth)
                        Console.Write(new string(' ', highlightWidth - itemText.Length));

                    Console.ResetColor();
                }
                else
                    Console.Write(new string(' ', highlightWidth));
            }


            // Панель описания
            if (_selectedIndex >= 0 && _selectedIndex < Bestiary.Count)
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

                // Имя 
                string name = Bestiary[_selectedIndex].Name.ToUpper()+$" [{Bestiary[_selectedIndex].Icon}]";
                int nameX = rightPanelStartX + (rightPanelWidth - name.Length) / 2;
                Console.SetCursorPosition(nameX, (HEIGHT / 6 - HEIGHT / 12));
                Console.ForegroundColor = Bestiary[_selectedIndex].IconColor;
                Console.Write(name);

                int descY = HEIGHT / 6 + 2;
                Console.SetCursorPosition(rightPanelStartX, descY);
                Console.ForegroundColor = ConsoleColor.Yellow;
                string descr = $"[┼] {Bestiary[_selectedIndex].Description}";
                List<string> descrLines = SplitMessage(descr, rightPanelWidth);
                for (int j = 0; j < descrLines.Count; j++)
                {
                    Console.Write(descrLines[j]);
                    Console.SetCursorPosition(rightPanelStartX, ++descY);
                }

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                int specialY = descY + descrLines.Count + 2;
                Console.SetCursorPosition(rightPanelStartX, specialY);
                string special = $"{{!}} {Bestiary[_selectedIndex].Special}";
                List<string> specialLines = SplitMessage(special, rightPanelWidth);
                for (int j = 0; j < specialLines.Count; j++)
                {
                    Console.Write(specialLines[j]);
                    Console.SetCursorPosition(rightPanelStartX, ++specialY);
                }

                Console.SetCursorPosition(rightPanelStartX, specialY + 2);
                Console.ForegroundColor = ConsoleColor.Cyan;
                string healthText = $"{{+}} О.З: [{Bestiary[_selectedIndex].Health}]";
                Console.Write(healthText);

                Console.SetCursorPosition(rightPanelStartX, specialY + 4);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                string damageText = $"{{!}} УРОН: [{Bestiary[_selectedIndex].Damage}]";
                Console.Write(damageText);

                Console.SetCursorPosition(rightPanelStartX, specialY + 6);
                Console.ForegroundColor = ConsoleColor.Blue;
                string typeText = $"{{?}} Тип: [{Bestiary[_selectedIndex].Type}]";
                Console.Write(typeText);
            }

            Console.ResetColor();
            Console.SetCursorPosition(3, HEIGHT - 3);
            int curr = Bestiary.Count > 0 ? _selectedIndex + 1 : 0;
            Console.Write($"[{curr}/{Bestiary.Count}] │ [W/S] - Листать Вверх/Вниз │ [Enter] - Экипировать");
        }
        private void DrawItems()
        {
            TabType = TabTypes.Items;
            int highlightWidth = WIDTH / 2 - 2; 

            
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
                        Console.ResetColor();

                    var item = Items[itemIndex];
                    string mark = GameSession.Hero.EquippedItem == item ? "(X)" : "( )";
                    string itemText = $"{mark} {item.Name}";

                    
                    if (itemText.Length > highlightWidth)
                        itemText = itemText.Substring(0, highlightWidth);


                    for (int k = 0; k < itemText.Length; k++)
                    {
                        if (itemIndex != _selectedIndex)
                        {
                            if (k < 3)
                                Console.ForegroundColor = ConsoleColor.White;
                            else
                                Console.ForegroundColor = item.InventoryColor;
                        }
                        Console.Write(itemText[k]);
                    }


                    if (itemText.Length < highlightWidth)
                        Console.Write(new string(' ', highlightWidth - itemText.Length));

                    Console.ResetColor();
                }
                else
                    Console.Write(new string(' ', highlightWidth));
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

                string name = Items[_selectedIndex].Name.ToUpper();
                int nameX = rightPanelStartX + (rightPanelWidth - name.Length) / 2;
                Console.SetCursorPosition(nameX, (HEIGHT / 6 - HEIGHT / 12));
                Console.ForegroundColor = Items[_selectedIndex].InventoryColor;
                Console.Write(name);
                Console.ResetColor();

                int descY = HEIGHT / 6 + 2;
                Console.SetCursorPosition(rightPanelStartX, descY);
                Console.ForegroundColor = ConsoleColor.Yellow;
                string descr = $"[┼] {Items[_selectedIndex].Description}";
                List<string> descrLines = SplitMessage(descr, rightPanelWidth);
                for (int j = 0; j < descrLines.Count; j++)
                {
                    Console.Write(descrLines[j]);
                    Console.SetCursorPosition(rightPanelStartX, ++descY);
                }

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                int specialY = descY + descrLines.Count + 2;
                Console.SetCursorPosition(rightPanelStartX, specialY);
                string special = $"{{!}} {Items[_selectedIndex].Special}";
                List<string> specialLines = SplitMessage(special, rightPanelWidth);
                for (int j = 0; j < specialLines.Count; j++)
                {
                    Console.Write(specialLines[j]);
                    Console.SetCursorPosition(rightPanelStartX, ++specialY);
                }

            }

            // Подсказка в левом нижнем углу.
            Console.ResetColor();
            Console.SetCursorPosition(3, HEIGHT - 3);
            int curr = Items.Count > 0 ? _selectedIndex + 1 : 0;
            Console.Write($"[{curr}/{Items.Count}] │ [W/S] - Листать Вверх/Вниз │ [Enter] - Экипировать");
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
                        Console.ResetColor();

                    var weapon = Weapons[itemIndex];
                    string mark = GameSession.Hero.EquippedWeapon == weapon ? "[X]" : "[ ]";
                    string itemText = $"{mark} {weapon.Name}";

                    
                    if (itemText.Length > highlightWidth)
                        itemText = itemText.Substring(0, highlightWidth);

                    for (int k = 0; k < itemText.Length; k++)
                    {
                        if(itemIndex != _selectedIndex)
                        {
                            if (k < 3)
                                Console.ForegroundColor = ConsoleColor.White;
                            else
                                Console.ForegroundColor = weapon.InventoryColor;
                        }
                        Console.Write(itemText[k]);
                    }

                    if (itemText.Length < highlightWidth)
                        Console.Write(new string(' ', highlightWidth - itemText.Length));

                    Console.ResetColor();
                }
                else
                    Console.Write(new string(' ', highlightWidth));
            }

            
            
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
                Console.SetCursorPosition(nameX, (HEIGHT / 6 - HEIGHT/12));
                Console.ForegroundColor = Weapons[_selectedIndex].InventoryColor;
                Console.Write(name);
                Console.ResetColor();

                int descY = HEIGHT / 6 + 2;
                Console.SetCursorPosition(rightPanelStartX, descY);
                Console.ForegroundColor = ConsoleColor.Yellow;
                string descr =  $"[┼] {Weapons[_selectedIndex].Description}";
                List<string> descrLines = SplitMessage(descr, rightPanelWidth);
                for (int j = 0; j < descrLines.Count; j++)
                {
                    Console.Write(descrLines[j]);
                    Console.SetCursorPosition(rightPanelStartX, ++descY);
                }

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                int specialY = descY + descrLines.Count + 2;
                Console.SetCursorPosition(rightPanelStartX, specialY);
                string special = $"{{!}} {Weapons[_selectedIndex].Special}";
                List<string> specialLines = SplitMessage(special, rightPanelWidth);
                for (int j = 0; j < specialLines.Count; j++)
                {
                    Console.Write(specialLines[j]);
                    Console.SetCursorPosition(rightPanelStartX, ++specialY);
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                string damageText = $"{{^}} Урон: [{Weapons[_selectedIndex].Damage}]";
                int damageX = rightPanelStartX;
                Console.SetCursorPosition(damageX, specialY + 2);
                Console.Write(damageText);

            }

            Console.ResetColor();
            Console.SetCursorPosition(3, HEIGHT - 3);
            Console.Write($"[{_selectedIndex + 1}/{Weapons.Count}] │ [W/S] - Листать Вверх/Вниз │ [Enter] - Экипировать");
        }
        private void DrawCore()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
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
            Console.Write("ПРЕДМЕТЫ [T]".PadLeft(WIDTH / 7));
            if (TabType == TabTypes.Bestiary)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(WIDTH / 2 + 1, 1);
            Console.Write("БЕСТИАРИЙ [Y]".PadLeft(2 * WIDTH / 14));
            if (TabType == TabTypes.Save)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(3 * WIDTH / 4 + 1, 1);
            Console.Write("СОХРАНИТЬ И ВЫЙТИ [L]".PadLeft(3 * WIDTH / 24));


        }
        public static List<string> SplitMessage(string message, int maxWidth)
        {
            List<string> lines = new List<string>();

            if (message.Length <= maxWidth)
            {
                lines.Add(message);
                return lines;
            }

            int currentIndex = 0;
            while (currentIndex < message.Length)
            {
                int length = Math.Min(maxWidth, message.Length - currentIndex);

                if (currentIndex + length < message.Length)
                {
                    int lastSpace = message.LastIndexOf(' ', currentIndex + length - 1, length);
                    if (lastSpace > currentIndex)
                    {
                        length = lastSpace - currentIndex + 1;
                    }
                }

                lines.Add(message.Substring(currentIndex, length).TrimEnd());
                currentIndex += length;
            }

            return lines;
        }


    }
}

