using Legacy.Enemies;
using Legacy.Weapons.OtherWeapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Items
{
    public class CursedSoul : Item
    {
        public string enemyName = "Неизвестно";
        public decimal healingEffect;
        public CursedSoul()
        {
            Name = $"Проклятая душа [{enemyName}]";
            Description = $"Эта душа никогда не обретёт покой... Как и вы. Восстанавливает [{healingEffect}] О.З, но ваша душа МОЖЕТ расколоться. Если случится раскол, ваше максимальное здоровье сократится вдвое";
            InventoryColor = ConsoleColor.DarkMagenta;
        }
        public override void UseOnEnemy(Enemy enemy)
        {
            base.UseOnEnemy(enemy);
            int result = Random.Shared.Next(0, 10);
            if (result == 9)
                enemy.Health -= enemy.Health / 2;
            else
                enemy.Health += healingEffect;
        }

        public override void UseOnHero(Hero hero)
        {
            base.UseOnHero(hero);
            int result = Random.Shared.Next(0, 10);
            if (result == 9)
            {
                hero.MaxHealth = hero.MaxHealth / 2;
                if (hero.Health > hero.MaxHealth)
                    hero.Health = hero.MaxHealth;
            }
            else
            {
                hero.Health += healingEffect;
                if (hero.Health > hero.MaxHealth)
                    hero.Health = hero.MaxHealth;
            }
        }
    }
}
