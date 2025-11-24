using Legacy.Enemies;
using Legacy.Weapons;
using Legacy.Weapons.OtherWeapons;
using static Legacy.FloorSession;
using static Legacy.GameSession;

namespace Legacy
{
    public class Hero 
    {
        public int Gold { get; set; } = 0;
        public char Icon { get; protected set; } = '&';
        public (int x, int y) Pos = (1, 1);
        public Inventory HeroInventory = new Inventory();
        public int Stagger = 0;
        public Weapon EquipedWeapon;

        public decimal Health { get; set; } = 10;
        public decimal MaxHealth = 10;
        public Hero()
        {
            //Fist()
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
                    {
                        var next = (Pos.x, Pos.y - 1);
                        switch (Map[Pos.y - 1, Pos.x])
                        {
                            case ' ':
                                WriteNewPosition(' ', (Pos.x, Pos.y));
                                Pos.y -= 1;
                                WriteNewPosition(Icon, (Pos.x, Pos.y),ConsoleColor.Yellow);
                                break;
                            case '|':
                                break;
                            case '!':
                                var weapon = FloorSession.Weapons.FirstOrDefault(w => w.Pos == next);
                                HeroInventory.Weapons.Add(weapon);
                                EquipedWeapon = HeroInventory.Weapons.Last();
                                WriteNewPosition(' ', next);
                                FloorSession.Weapons.Remove(weapon);
                                break;
                            case '#':
                                var chest = FloorSession.Chests.FirstOrDefault(c => c.Pos == next);
                                var result = chest.Open();
                                if(!result)
                                    WriteNewPosition(' ', next);
                                FloorSession.Chests.Remove(chest);
                                break;
                            default:
                                var enemy = FloorSession.Enemies.FirstOrDefault(e => e.Pos == next);
                                Attack(enemy);
                                break;
                        }
                    }
                    break;
                case Actions.Down:
                    if (Pos.y + 1 < HEIGHT - 1)
                    {
                        var next = (Pos.x, Pos.y + 1);
                        switch (Map[Pos.y + 1, Pos.x])
                        {
                            
                            case ' ':
                                WriteNewPosition(' ', (Pos.x, Pos.y));
                                Pos.y += 1;
                                WriteNewPosition(Icon, (Pos.x, Pos.y), ConsoleColor.Yellow);
                                break;
                            case '|':
                                break;
                            case '!':
                                var weapon = FloorSession.Weapons.FirstOrDefault(w => w.Pos == next);
                                HeroInventory.Weapons.Add(weapon);
                                EquipedWeapon = HeroInventory.Weapons.Last();
                                WriteNewPosition(' ', next);
                                break;
                            default:
                                var enemy = FloorSession.Enemies.FirstOrDefault(e => e.Pos == next);
                                Attack(enemy);
                                break;
                        }  
                    }
                    break;
                case Actions.Left:
                    if (Pos.x - 1 >= 1)
                    {
                        var next = (Pos.x - 1, Pos.y );
                        switch (Map[Pos.y, Pos.x - 1])
                        {
                            case ' ':
                                WriteNewPosition(' ', (Pos.x, Pos.y));
                                Pos.x -= 1;
                                WriteNewPosition(Icon, (Pos.x, Pos.y), ConsoleColor.Yellow);
                                break;
                            case '|':
                                break;
                            case '!':
                                var weapon = FloorSession.Weapons.FirstOrDefault(w => w.Pos == next);
                                HeroInventory.Weapons.Add(weapon);
                                EquipedWeapon = HeroInventory.Weapons.Last();
                                WriteNewPosition(' ', next);
                                break;
                            default:
                                var enemy = FloorSession.Enemies.FirstOrDefault(e => e.Pos == next);
                                Attack(enemy);
                                break;
                        }

                    }
                    break;
                case Actions.Right:
                    if (Pos.x + 1 < WIDTH)
                    {
                        var next = (Pos.x + 1, Pos.y);
                        switch (Map[Pos.y, Pos.x + 1])
                        {
                            case ' ':
                                WriteNewPosition(' ', (Pos.x, Pos.y));
                                Pos.x += 1;
                                WriteNewPosition(Icon, (Pos.x, Pos.y), ConsoleColor.Yellow);
                                break;
                            case '|':
                                break;
                            case '!':
                                var weapon = FloorSession.Weapons.FirstOrDefault(w => w.Pos == next);
                                HeroInventory.Weapons.Add(weapon);
                                EquipedWeapon = HeroInventory.Weapons.Last();
                                WriteNewPosition(' ', next);
                                break;
                            default:
                                var enemy = FloorSession.Enemies.FirstOrDefault(e => e.Pos == next);
                                Attack(enemy);
                                break;
                        }
                    }
                    break;
                default:
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
                FloorSession.Enemies.Remove(enemy);
            }
            if (EquipedWeapon is IPostSpecial postCast)
                postCast.PostCast(this, enemy);

        }
    }
}
