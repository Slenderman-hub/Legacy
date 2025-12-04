using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Legacy.Weapons.OtherWeapons
{
    public class FakeWoodenSword : Weapon
    {
        public FakeWoodenSword()
        {

            Name = "Деревянный меч";
            Damage = 0.1m;
            Description = "Такой палкой, только крапиву бить! Никак не чудовищ, что кишат кругом, но быть может, Искатель Смерти найдет ему применение?";
            Special = "Such a cringe";
            InventoryColor = ConsoleColor.White;

        }
    }
}
