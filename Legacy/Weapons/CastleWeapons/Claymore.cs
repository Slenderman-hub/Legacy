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
        public Claymore() : base("Клеймор", 3)
        {
            Description = "Классический инструмент, для традиционной резни";
            Special = "После убийства противника, следующий удар нанесет [3х] кратный урон";

        }

        public void PostCast(Hero hero, Enemy enemy)
        {
            if (this.Damage == 9)
                this.Damage = 3;
            if(enemy.Health <= 0)
                this.Damage *= 3;
        }

    }
}
