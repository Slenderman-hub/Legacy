using Legacy.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Weapons
{
    public class Claymore : Weapon, IPostSpecial
    {
        private bool buff = false;
        public Claymore() 
        {
            Name = "Клеймор";
            Damage = 3;
            Description = "Классический инструмент, для традиционной резни";
            Special = "После убийства противника, следующий удар нанесет [3х] кратный урон";

        }

        public void PostCast(Hero hero, Enemy enemy)
        {
            if (buff)
            {
                Damage /= 3;
                buff = false;
            }
            if(enemy.Health <= 0)
            {
                Damage *= 3;
                buff = true;
            }
                
        }

    }
}
