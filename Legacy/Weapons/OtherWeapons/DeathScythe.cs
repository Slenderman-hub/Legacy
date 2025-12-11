using Legacy.Enemies;
using Legacy.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Weapons.OtherWeapons
{
    public class DeathScythe : Weapon, IPassiveSpecial, IPreSpecial
    {
        private bool _buff = false;
        public DeathScythe()
        {
            Name = "Коса Смерти";
            Damage = 1000;
            Description = "Странно, что эту забавную вещь можно найти в таком месте, но с другой стороны, а в каком месте еще она может быть? Только опытный Искатель Смерти, сможет достойно ей распоряжатся";
            Special = "Жуткое, но родное чувство, что за вашей спиной ходит {она}... снижает ваши показатели О.З до [1]. Эффект активируется только, при первом использовании";
            InventoryColor = ConsoleColor.DarkGray;
        }
        public void PassiveCast(Hero hero)
        {
            if (_buff)
            {
                if(hero.Health > 0)
                {
                    hero.MaxHealth = 1;
                    hero.Health = 1;
                }
                
            }
        }

        public void PreCast(Hero hero, Enemy enemy)
        {
            if (!_buff)
            {
                _buff = true;
                GameSession.Logger.Log($"Вы чувствуете {{её}} присутствие...", ConsoleColor.DarkMagenta);
            }
            
        }
    }
}
