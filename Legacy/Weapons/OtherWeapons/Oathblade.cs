using Legacy.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Weapons.OtherWeapons
{
    public class Oathblade : Weapon, IPostSpecial
    {
        public Oathblade() 
        {
            Name = "Клятвенный клинок";
            Damage = 4.5m;
            Description = "Твоя Смерть повязана судьбою";
            Special = "После убийства противника СЛУЧАЙНО будет определено одно из следующих событий: 1)Герой получит [+5] к максимальному здоровью 2) Вы будете ранены на [5] урона";
            InventoryColor = ConsoleColor.DarkRed;
        }
        public void PostCast(Hero hero, Enemy enemy)
        {
            int result = Random.Shared.Next(2);
            if(result == 0)
            {
                hero.MaxHealth += 5;
            }
            else
            {
                hero.Health -= 5;
            }

        }
    }
}
