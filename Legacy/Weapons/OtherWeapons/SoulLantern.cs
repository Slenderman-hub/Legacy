using Legacy.Enemies;
using Legacy.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Weapons.OtherWeapons
{
    public class SoulLantern : Weapon, IPreSpecial
    {
        private List<Enemy> _currentStack = new List<Enemy>();
        public SoulLantern()
        {
            Name = "Фонарь Души";
            Damage = 3;
            Description = "Коллекционирование душ, это прямая дорога в ад, но для Искателей Смерти, этот путь спасения уже закрыт веками. К сожалению вы профан в душевладении, поэтому все души, которые вы получаете - прокляты";
            Special = "При использовании, вы выссасываете из врага проклятую душу. Позже, вы можете ее поглотить и исцелится.";
            InventoryColor = ConsoleColor.DarkCyan;
        }
        public void PreCast(Hero hero, Enemy enemy)
        {
            if (_currentStack.Contains(enemy))
                return;
            hero.HeroInventory.Items.Add(new CursedSoul()
            {
                healingEffect = enemy.Health,
                enemyName = enemy.Name
            });
        }
    }
}
