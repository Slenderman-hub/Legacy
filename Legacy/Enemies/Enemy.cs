using  Legacy.Items;
using Legacy.Weapons;
using System;
using System.Text.Json.Serialization;
using static Legacy.FloorSession;
using static Legacy.GameSession;

namespace Legacy.Enemies
{
    public class Enemy : MapEntity
    {
        [JsonInclude]
        public string Name = string.Empty;
        [JsonInclude]
        public decimal Health;
        [JsonInclude]
        public decimal Damage;
        [JsonInclude]
        public string Description = string.Empty;
        [JsonInclude]
        public string Type = string.Empty;
        [JsonInclude]
        public string Special = string.Empty;

        public List<Item> LootItems = new List<Item>(0);
        public List<Weapon> LootWeapons = new List<Weapon>(0);


        public (int x, int y) LastHeroPos = (-1,-1);
        public bool HeroNearby = false;

        private int StaggerImmune = 0;
        public int Stagger = 0;

        

        public Enemy(int x, int y)
        {
            Pos = (x, y);
            IconColor = ConsoleColor.White;
        }

        [JsonConstructor]
        public Enemy() { }
        public virtual void Action(Actions action)
        {
            (bool,Actions) HeroNearby = CheckForHeroNearby();
            (bool, Actions) HeroMaybeNearby = CheckForLastHeroPos();

            if (HeroNearby.Item1)
            {
                action = HeroNearby.Item2;
            }
            else if(HeroMaybeNearby.Item1)
            {
                
                action = HeroMaybeNearby.Item2;
            }
            else
            {
                this.HeroNearby = false;
            }



            if (Stagger > 0)
            {
                StaggerImmune++;
                Stagger -= StaggerImmune;
                return;
            }
            else
            {
                if (StaggerImmune > 0)
                {
                    StaggerImmune--;
                }
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
                    case Actions.Nothing:
                        break;
                    default:
                        break;
                }
        }

        protected (bool, Actions) CheckForLastHeroPos()
        {
            if (LastHeroPos == (-1, -1))
                return (false, Actions.Nothing);
            (bool, Actions) result = (false,Actions.Nothing);

            if (LastHeroPos == (Pos.x + 1, Pos.y))
                result =(true, Actions.Right);
            else if (LastHeroPos == (Pos.x - 1, Pos.y))
                result = (true, Actions.Left);
            else if (LastHeroPos == (Pos.x, Pos.y+ 1))
                result = (true, Actions.Down);
            else if (LastHeroPos == (Pos.x, Pos.y - 1))
                result = (true, Actions.Up);

            LastHeroPos = (-1, -1);
            return result;
        }

        protected (bool, Actions) CheckForHeroNearby()
        {
            if (GameSession.Hero.Pos == (Pos.x + 1, Pos.y))
            {
                LastHeroPos = GameSession.Hero.Pos;
                if (HeroNearby)
                    return (true, Actions.Right);
                else
                {
                    HeroNearby = true;
                    return (true, Actions.Nothing);
                }
            }
            if(GameSession.Hero.Pos == (Pos.x - 1, Pos.y))
            {
                LastHeroPos = GameSession.Hero.Pos;
                if (HeroNearby)
                    return (true, Actions.Left);
                else
                {
                    HeroNearby = true;
                    return (true, Actions.Nothing);
                }
            }
            if (GameSession.Hero.Pos == (Pos.x, Pos.y+ 1))
            {
                LastHeroPos = GameSession.Hero.Pos;
                if (HeroNearby)
                    return (true, Actions.Down);
                else
                {
                    HeroNearby = true;
                    return (true, Actions.Nothing);
                }
            }
            if (GameSession.Hero.Pos == (Pos.x, Pos.y -1))
            {
                LastHeroPos = GameSession.Hero.Pos;
                if (HeroNearby)
                    return (true, Actions.Up);
                else
                {
                    HeroNearby = true;
                    return (true, Actions.Nothing);
                }
            }



            return (false, Actions.Nothing);
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
            GameSession.Logger.Log($"[{Name}] наносит вам [{Damage}] урона",ConsoleColor.Red);
        }

        public virtual void OnDeath()
        {
            foreach (Item item in LootItems)
            {
                GameSession.Hero.HeroInventory.Items.Add(item);
                GameSession.Logger.Log($"Вы получили предмет [{item.Name}]");
            }
            foreach (Weapon item in LootWeapons)
            {
                GameSession.Hero.HeroInventory.Weapons.Add(item);
                GameSession.Logger.Log($"Вы получили оружие [{item.Name}]");
            }
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
                Type = enemy.Type,
                IconColor = enemy.IconColor
            };
        }
    }
    
}
