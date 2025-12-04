using  Legacy.Items;
using Legacy.Weapons;
using System.Text.Json.Serialization;
using static Legacy.FloorSession;
using static Legacy.GameSession;

namespace Legacy.Enemies
{
    public class Enemy : MapEntity
    {
        public List<Item> LootItems = new List<Item>(0);
        public List<Weapon> LootWeapons = new List<Weapon>(0);

        public int Stagger = 0;
        [JsonInclude]
        public string Name = string.Empty;
        [JsonInclude]
        public string Description = string.Empty;
        [JsonInclude]
        public decimal Health;
        [JsonInclude]
        public decimal Damage;
        [JsonInclude]
        public string Type = string.Empty;
        public Enemy(int x, int y)
        {
            Pos = (x, y);
            IconColor = ConsoleColor.DarkGray;
        }
        [JsonConstructor]
        public Enemy()
        {

        }
        public virtual void Action(Actions action)
        {
            if (Stagger > 0)
            {
                Stagger -= 2;
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
                        HandleNextTile(Pos.x-1, Pos.y);
                    break;
                case Actions.Right:
                    if (Pos.x + 1 < MAP_WIDTH)
                        HandleNextTile(Pos.x + 1, Pos.y);
                    break;
                default:
                    break;
            }
        }

        protected virtual void HandleNextTile(int x, int y)
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
                case '&':
                    Attack(GameSession.Hero);
                    break;
                default:
                    break;
            }
        }

        public virtual void Attack(Hero hero)
        {
            hero.Health -= Damage;
            hero.Stagger += 1;
            if (hero.Health <= 0)
            {
               WriteNewPosition('X', (hero.Pos.x, hero.Pos.y),ConsoleColor.Red);
            }
        }

        public virtual void OnDeath()
        {
            foreach (Item item in LootItems)
                GameSession.Hero.HeroInventory.Items.Add(item);
            foreach (Weapon item in LootWeapons)
                GameSession.Hero.HeroInventory.Weapons.Add(item);
        }
        public static Enemy Copy(Enemy enemy)
        {
            return  new Enemy(0, 0)
            {
                Name = enemy.Name,
                Icon = enemy.Icon,
                Description = enemy.Description,
                Damage = enemy.Damage,
                Health = enemy.Health,
                Type = enemy.Type
            };
        }
    }
    
}
