using Legacy.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Weapons.OtherWeapons
{
    public class Oathbladec : Weapon, IPostSpecial
    {
        public Oathbladec() : base("Клятвенный клинок", 4.5m)
        {
            Description = "Твоя смерть повязана судьбою";
            Special = "После убийства противника СЛУЧАЙНО определитсься одно из следующих событий: 1)Герой получит [+10] к максимальному здоровью 2) Вы будете ранены на [10] урона";
        }
        public void PostCast(Hero hero, Enemy enemy)
        {
            int result = Random.Shared.Next(2);
            if(result == 0)
            {
                hero.MaxHealth += 10;
            }
            else
            {
                hero.Health -= 10;
            }

        }
    }
}
