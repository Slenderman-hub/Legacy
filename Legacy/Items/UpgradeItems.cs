using Legacy.Enemies;
using Legacy.Weapons.OtherWeapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Items
{
    public class GrindStone : Item, IUseOnEnemy, IUseOnHero
    {
        public GrindStone() : base("Точильный камень")
        {
            Description = "В наши дни, такие камни на дороге не валяются! Самое то, чтобы наточить свою шпагу на [1.5] единиц урона, и [2х] кратно противнику";
        }
        public void UseOnEnemy(Enemy enemy)
        {
            enemy.LootWeapons.Add(new MonsterWeapon(enemy.Damage));
            enemy.Damage *= 2;
        }

        public void UseOnHero(Hero hero) => hero.EquipedWeapon.Damage += 1.5m;

    }

}
