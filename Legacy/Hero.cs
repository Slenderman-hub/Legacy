using Legacy.Enemies;
using Legacy.Items;
using Legacy.Weapons;
using Legacy.Weapons.OtherWeapons;
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

        public decimal Health { get; set; } = 10;
        public decimal MaxHealth = 10;
        public Hero()
        {
            //Fist()
            Pos = (1, 1);
            Icon = '&';
            HeroInventory.Weapons.Add(new Katana());
            EquipedWeapon = HeroInventory.Weapons[0];
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
                    if (Pos.y + 1 < HEIGHT - 1)
                        HandleNextTile(Pos.x, Pos.y + 1);
                    break;
                case Actions.Left:
                    if (Pos.x - 1 >= 1)
                        HandleNextTile(Pos.x - 1, Pos.y);
                    break;
                case Actions.Right:
                    if (Pos.x + 1 < WIDTH)
                        HandleNextTile(Pos.x + 1, Pos.y);
                    break;
                default:
                    break;
            }
        }

        private void HandleNextTile(int x, int y)
        {
            var next = (x, y);
            switch (Map[y, x])
            {
                case ' ':
                    WriteNewPosition(' ', (Pos.x, Pos.y));
                    Pos = next;
                    WriteNewPosition(Icon, (Pos.x, Pos.y), ConsoleColor.Yellow);
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
            }
            if (EquipedWeapon is IPostSpecial postCast)
                postCast.PostCast(this, enemy);

        }
    }
}
