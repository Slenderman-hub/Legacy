
using Legacy.Enemies;

namespace Legacy.Weapons.OtherWeapons
{
    public class MonsterWeapon : Weapon , IPreSpecial
    {
        public MonsterWeapon(decimal damage) : base("Чудовищное оружие", damage)
        {
            Description = "Вам настолько понравилось оружие противника, что вы решили его прихватить? Похвально";
            Special = "Очень воняет";
        }

        public void PreCast(Hero hero, Enemy enemy)
        {
            //TODO 
        }
    }
}
