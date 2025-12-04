using Legacy.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Weapons.OtherWeapons
{
    public class Glaive : Weapon, IPreSpecial
    {
        public Glaive()
        {
            Name = "Глефа";
            Damage = 3.4m;
            Description = "Держать дистанцию это важный принцип воинского подмастерья. Впрочем, есть ли до него дело Искателю Смерти?";
            Special = "Каждый удар, добавляет вам [-2], к харктеристики ошеломления";
            InventoryColor = ConsoleColor.Cyan;

        }
        public void PreCast(Hero hero, Enemy enemy)
        {
            hero.Stagger -= 2;
        }
    }
}
