using Legacy.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Weapons.OtherWeapons
{
    public class EnvyScythe :  Weapon, IPreSpecial
    {
        private int cooldown = 0;
        public int currentLevel;
        public EnvyScythe()
        {
            Name = "Коса Зависти";
            Damage = 0.1m;
            Description = "Отличное решение, для тех, кто грезит примерить на себе чужую маску. Жаль только, что тяжесть греха не позволит вернуть родной лик";
            Special = "[Раз за 2 этажa] При использовании, ваше максимальное здоровье будет иметь значение, соответствующее Очкам Здоровья врагa, а урон косы, теперь соответствует урону врага";
            InventoryColor = ConsoleColor.DarkMagenta;
        }

        public void PreCast(Hero hero, Enemy enemy)
        {
            if (currentLevel != GameSession.Level)
            {
                if(cooldown == 0)
                {
                    Damage = enemy.Damage;
                    hero.MaxHealth = enemy.Health;
                    if (hero.Health > hero.MaxHealth)
                        hero.Health = hero.MaxHealth;
                    Name = $"Коса Зависти [{enemy.Name}]";
                    cooldown++;
                }
                else
                {
                    cooldown--;
                }
                currentLevel = GameSession.Level;


            }
        }
    }
}
