using Legacy.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Weapons.CastleWeapons
{
    public class GardenShears : Weapon, IPostSpecial
    {
        public GardenShears()
        {
            Name = "Садовые ножницы";
            Damage = 2.8m;
            Description = "Не самая полезная вещь, что можно найти в этом замке. Но думаю, Искателям Растений оно понравится!";
            Special = "За убийство врага, дает 10 золота. За убийства врага типа [Растение], дает 500 золота.";
            InventoryColor = ConsoleColor.Yellow;
        }

        public void PostCast(Hero hero, Enemy enemy)
        {
            if(enemy.Health < 0)
            {
                GameSession.Hero.Gold += 10;
                if(enemy.Type == "Растение")
                {
                    GameSession.Hero.Gold += 500;
                }

            }
        }

    }
}
