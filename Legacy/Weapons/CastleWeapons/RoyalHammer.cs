using Legacy.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Weapons.CastleWeapons
{
    public class RoyalHammer : Weapon, IPassiveSpecial, IPreSpecial
    {
        private bool buff = false;
        public RoyalHammer()
        {
            Name = "Королевский молот";
            Damage = 5;
            Description = "Его вид, сотрясает своим величием, как и удар. Искатель Смерти будет его таскать, только если совсем отчаялся или сошел с ума (что более вероятно)";
            Special = "Если экипирован, то каждый второй ход накладывает вам [1] ошеломление. При использовании, накладывает дополнительно [1] ошеломление противнику";
            IconColor = ConsoleColor.Red;
        }
        public void PassiveCast(Hero hero)
        {
            if(hero.EquippedWeapon == this)
            {
                if (buff)
                {

                    buff = false;
                    hero.Stagger += 1;
                }
                else
                {
                    buff = true;
                }
            }

        }

        public void PreCast(Hero hero, Enemy enemy)
        {
            enemy.Stagger++;
        }
    }
}
