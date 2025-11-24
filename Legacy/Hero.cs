using Items;
using Legacy.Enemies;
using Legacy.Interfaces;
using Legacy.Weapons;
using static Legacy.FloorSession;
using static Legacy.GameSession;

namespace Legacy
{
    public class Hero 
    {
        public (int x, int y) Pos = (1, 1);
        public Inventory HeroInventory = new Inventory();
        public int Stagger = 0;
        public Weapon EquipedWeapon;

        public decimal Health { get; set; } = 10;
        public decimal MaxHealth = 10;
        public Hero()
        {
            HeroInventory.Weapons.Add(new Weapons.Fist());
            EquipedWeapon = HeroInventory.Weapons[0];
        }
        public void Action(Actions action)
        {
            if(Stagger < 0)
            {
                Stagger++;
                return;
            }
                
            switch (action)
            {
                case Actions.Up:
                    if (Pos.y - 1 >= 1)
                    {
                        switch (Map[Pos.y - 1, Pos.x])
                        {
                            case ' ':
                                WriteNewPosition(' ', (Pos.x, Pos.y));
                                Pos.y -= 1;
                                WriteNewPosition('I', (Pos.x, Pos.y),ConsoleColor.Yellow);
                                break;
                            case '|':
                                break;
                            default:
                                var Enemy = FloorSession.Enemies.FirstOrDefault(e => e.Pos == (Pos.x, Pos.y - 1));
                                Attack(Enemy);
                                break;
                        }
                    }
                    break;
                case Actions.Down:
                    if (Pos.y + 1 < HEIGHT - 1)
                    {
                        switch(Map[Pos.y + 1, Pos.x])
                        {
                            case ' ':
                                WriteNewPosition(' ', (Pos.x, Pos.y));
                                Pos.y += 1;
                                WriteNewPosition('I', (Pos.x, Pos.y), ConsoleColor.Yellow);
                                break;
                            case '|':
                                break;
                            default:
                                var Enemy = FloorSession.Enemies.FirstOrDefault(e => e.Pos == (Pos.x , Pos.y + 1));
                                Attack(Enemy);
                                break;
                        }  
                    }
                    break;
                case Actions.Left:
                    if (Pos.x - 1 >= 1)
                    {
                        switch(Map[Pos.y, Pos.x - 1])
                        {
                            case ' ':
                                WriteNewPosition(' ', (Pos.x, Pos.y));
                                Pos.x -= 1;
                                WriteNewPosition('I', (Pos.x, Pos.y), ConsoleColor.Yellow);
                                break;
                            case '|':
                                break;
                            default:
                                var Enemy = FloorSession.Enemies.FirstOrDefault(e => e.Pos == (Pos.x - 1, Pos.y));
                                Attack(Enemy);
                                break;
                        }

                    }
                    break;
                case Actions.Right:
                    if (Pos.x + 1 < WIDTH)
                    {
                        switch(Map[Pos.y, Pos.x + 1])
                        {
                            case ' ':
                                WriteNewPosition(' ', (Pos.x, Pos.y));
                                Pos.x += 1;
                                WriteNewPosition('I', (Pos.x, Pos.y), ConsoleColor.Yellow);
                                break;
                            case '|':
                                break;
                            default:
                                var Enemy = FloorSession.Enemies.FirstOrDefault(e => e.Pos == (Pos.x + 1 , Pos.y));
                                Attack(Enemy);
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
            enemy.Health -= EquipedWeapon.Damage;
            enemy.Stagger += 1;
            if(enemy.Health <= 0)
            {
                WriteNewPosition(' ', (enemy.Pos.x, enemy.Pos.y));
                FloorSession.Enemies.Remove(enemy);
                if (EquipedWeapon is IVampirism w)
                    w.Heal(this);
            }
            
        }
    }
}
