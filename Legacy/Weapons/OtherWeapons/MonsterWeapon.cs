
using Legacy.Enemies;

namespace Legacy.Weapons
{
    public class MonsterWeapon : Weapon , IPreSpecial
    {
        public MonsterWeapon(decimal damage) 
        {
            Name = "Чудовищное оружие";
            Damage = damage;
            Description = "Вам настолько понравилось оружие противника, что вы решили его прихватить? Похвально";
            Special = "Очень воняет";
            InventoryColor = ConsoleColor.DarkGreen;
        }

        public void PreCast(Hero hero, Enemy enemy)
        {
            //TODO 
        }
    }
}
