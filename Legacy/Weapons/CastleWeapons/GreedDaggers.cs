using Legacy.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Weapons.CastleWeapons
{
    public class GreedDaggers : Weapon, IPostSpecial
    {
        public GreedDaggers()
        {
            Name = "Кинжалы Жадности";
            Damage = 1;
            Description = "За такие деньги, можно и мать родную прирезать! Жаль только, что тяжесть греха тянет вас в родное пекло";
            Special = "За каждый удар противника, вы получаете [Урон * 20] золота, и урон кинжалов растет на [+1]. За каждое использование вам наносится [1x] урона оружия";
            InventoryColor = ConsoleColor.DarkYellow;

        }

        public void PostCast(Hero hero, Enemy enemy)
        {
            hero.Gold += (int)Damage * 20;
            hero.Health -= Damage;
            Damage++;
        }

    }
}
