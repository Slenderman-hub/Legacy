using Legacy.Enemies;
using Legacy.Items;
using Legacy.Weapons;
using static Legacy.FloorSession;
using static Legacy.GameSession;

namespace Legacy
{
    public class Hero : MapEntity
    {
        public int Gold { get; set; } = 0;
        public Inventory HeroInventory = new Inventory();
        public int Stagger = 0;
        public Weapon EquipedWeapon;
        

        public decimal Health { get; set; } = 1000;
        public decimal MaxHealth = 1000;
        public Hero()
        {
            
            Pos = (1, 1);
            Icon = '&';
            IconColor = ConsoleColor.Red;
            HeroInventory.Weapons.Add(new Fist());
            HeroInventory.Weapons.Add(new Katana());
            HeroInventory.Weapons.Add(new Oathblade());
            EquipedWeapon = HeroInventory.Weapons.First();
        }
        public void Action(Actions action)
        {
            if(Stagger > 0)
            {
                Stagger--;
                return;
            }       
            switch (action)
            {
                case Actions.Up:
                    if (Pos.y - 1 >= 1)
                        HandleNextTile(Pos.x, Pos.y - 1);
                    break;
                case Actions.Down:
                    if (Pos.y + 1 < MAP_HEIGHT - 1)
                        HandleNextTile(Pos.x, Pos.y + 1);
                    break;
                case Actions.Left:
                    if (Pos.x - 1 >= 1)
                        HandleNextTile(Pos.x - 1, Pos.y);
                    break;
                case Actions.Right:
                    if (Pos.x + 1 < MAP_WIDTH)
                        HandleNextTile(Pos.x + 1, Pos.y);
                    break;
                case Actions.Swap:
                    SwapNextTile();
                    break;
                default:
                    break;
            }
        }

        private void SwapNextTile()
        {
            Stagger += 2;
            WriteNewPosition(Icon, Pos, ConsoleColor.Green);
            (int x, int y) next = GetNextTile();
            MapEntity nextEntity = Entities.FirstOrDefault(e => e.Pos == next);
            if (nextEntity is null)
            {
                WriteNewPosition(Icon, Pos, IconColor);
            return;

            }
            SwapPosition(nextEntity, this);
            WriteNewPosition(Icon, Pos, IconColor);
            WriteNewPosition(nextEntity.Icon, nextEntity.Pos, nextEntity.IconColor);
            

        }

        private (int x, int y) GetNextTile()
        {

            var userInput = Console.ReadKey(true).Key;
            switch (userInput)
            {
                case ConsoleKey.W:
                    if(Pos.y - 1 >= 0)
                        return (Pos.x, Pos.y - 1);
                    break;
                case ConsoleKey.S:
                    if(Pos.y + 1 < MAP_HEIGHT - 1)
                        return (Pos.x, Pos.y + 1);
                    break;
                case ConsoleKey.A:
                    if (Pos.x - 1 >= 0)
                        return (Pos.x - 1, Pos.y);
                    break;
                case ConsoleKey.D:
                    if(Pos.x + 1 < MAP_WIDTH)
                        return (Pos.x + 1, Pos.y);
                    break;
                default:
                    GetNextTile();
                    break;
                
            }
            return (0, 0);
        }

        private void HandleNextTile(int x, int y)
        {
            var next = (x, y);
            switch (Map[y, x])
            {
                case ' ':
                    WriteNewPosition(' ', (Pos.x, Pos.y));
                    Pos = next;
                    WriteNewPosition(Icon, (Pos.x, Pos.y), IconColor);
                    break;
                case '|':
                    break;
                case '!':
                    Weapon weapon = (Weapon)Entities.FirstOrDefault(w => w.Pos == next);
                    HeroInventory.Weapons.Add(weapon);
                    EquipedWeapon = HeroInventory.Weapons.Last();
                    WriteNewPosition(' ', next);
                    Entities.Remove(weapon);
                    break;
                case '#':
                    Chest chest = (Chest)Entities.FirstOrDefault(c => c.Pos == next);
                    bool result = chest.Open();
                    if (result)
                        WriteNewPosition(' ', next);
                    Entities.Remove(chest);
                    break;
                case '@':
                    Portal portal = (Portal)Entities.FirstOrDefault(p => p.Pos == next);
                    portal.Teleport();
                    break;
                default:
                    Enemy enemy = (Enemy)Entities.FirstOrDefault(e => e.Pos == next);
                    Attack(enemy);
                    break;
            }
        }

        private void Attack(Enemy enemy)
        {
            if (EquipedWeapon is IPreSpecial preCast)
                preCast.PreCast(this, enemy);
            enemy.Health -= EquipedWeapon.Damage;
            
            enemy.Stagger += 1;
            if(enemy.Health <= 0)
            {
                WriteNewPosition(' ', (enemy.Pos.x, enemy.Pos.y));
                Entities.Remove(enemy);
                enemy.OnDeath();
            }
            if (EquipedWeapon is IPostSpecial postCast)
                postCast.PostCast(this, enemy);

        }
    }
}
