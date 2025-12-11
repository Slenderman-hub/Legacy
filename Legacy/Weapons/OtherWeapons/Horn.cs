using Legacy.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Weapons.OtherWeapons
{
    internal class Horn : Weapon, IPassiveSpecial,IPreSpecial
    {
        private bool _buff = false;
        private Weapon buffedWeapon = null;
        public int currentLevel;
        public Horn()
        {
            Name = "Горн";
            Damage = 1.5m;
            Description = "Высококачественный звук, что изливается из его жерла, наполняет вас решимостью. Решимостью отыскать Смерть!";
            Special = "[Раз за этаж] При использовании, случайное оружие из вашего инвентаря повышает свой урон [2х] кратно. Использование горна, накладывает [8] ошеломления";
            InventoryColor = ConsoleColor.Green;

        }
        public void PassiveCast(Hero hero)
        {
            if (_buff)
            {
                if (currentLevel != GameSession.Level)
                {
                    if (buffedWeapon == null)
                        return;
                    buffedWeapon.Damage -= buffedWeapon.Damage / 2;
                    GameSession.Logger.Log($"Решимость [{buffedWeapon.Name}] меркнет",ConsoleColor.Gray);
                    buffedWeapon = null;
                    _buff = false;
                }
            }
            

        }

        public void PreCast(Hero hero, Enemy enemy)
        {
            if (currentLevel != GameSession.Level)
            {
                currentLevel = GameSession.Level;
                _buff = true;
                buffedWeapon = hero.HeroInventory.Weapons[Random.Shared.Next(0, hero.HeroInventory.Weapons.Count)];
                buffedWeapon.Damage += buffedWeapon.Damage ;
                GameSession.Logger.Log($"[{buffedWeapon.Name}] изливается решимостью",ConsoleColor.DarkMagenta);
            }
            hero.Stagger += 8;
            FloorSession.WriteNewPosition(hero.Icon, hero.Pos, ConsoleColor.Cyan);
        }
    }
}
