using Legacy.Enemies;
using Legacy.Weapons.OtherWeapons;
using System;
using System.Reflection.PortableExecutable;

namespace Legacy.Items
{
    public class GrindStone : Item
    {
        public GrindStone()
        {
            Name = "Точильный камень";
            Description = "В наши дни, такие камни на дороге не валяются! Самое то, чтобы наточить свою шпагу на [0.5] единиц урона, и [2х] кратно противнику";
            InventoryColor = ConsoleColor.DarkYellow;
        }
        public override void UseOnEnemy(Enemy enemy)
        {
            enemy.LootWeapons.Add(new MonsterWeapon(enemy.Damage));
            enemy.Damage *= 2;
            enemy.Stagger -= 5;
            enemy.IconColor = ConsoleColor.DarkRed;
            GameSession.Logger.Log($"[{enemy.Name}] жаждет яростной битвы", ConsoleColor.DarkRed);
        }

        public override void UseOnHero(Hero hero) 
        {
            hero.EquippedWeapon.Damage += 0.5m;
            GameSession.Logger.Log($"[{hero.EquippedWeapon.Name}] наточен до блеска", ConsoleColor.DarkGreen);
        } 
            

    }

}
