using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Weapons.OtherWeapons
{
    internal class HeroSword : Weapon, IPassiveSpecial
    {
        public int curentLevel;
        public HeroSword()
        {
            Name = "Меч Героя";
            Damage = 2;
            Description = "Герой не боится даже Смерти, а вот Смерть их еще как... Впрочем, это все слухи";
            Special = "Улучшается на [1] единицу урона, каждый переход на следующий этаж";
            InventoryColor = ConsoleColor.DarkBlue;

        }
        public void PassiveCast(Hero hero)
        {
            if (curentLevel != GameSession.Level)
            {
                Damage++;
                curentLevel = GameSession.Level;

            }
        }
    }

}
