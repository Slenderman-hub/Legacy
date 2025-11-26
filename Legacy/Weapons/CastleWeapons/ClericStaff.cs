
using Legacy.Enemies;

namespace Legacy.Weapons
{
    public class ClericStaff : Weapon, IPreSpecial
    {
        public ClericStaff() 
        {
            Name = "Церковный скипетр";
            Damage = 3;
            Description = "Будет служить вам ВЕРОЙ и правдой. Вы будете в это ВЕРИТЬ?";
            Special = "Наносит [2х] кратный урон по врагам типа нежить и накладывает им дополнительное 1 ошеломление. Если, противник не является ей, вы получаете 3 ошеломления и снимаете 3 ошеломления с противника";
        }
        public void PreCast(Hero hero, Enemy enemy)
        {
            if(enemy.Type == "Нежить")
            {
                enemy.Health -= 3;
                enemy.Stagger += 1;
            }
            else
            {
                hero.Stagger += 3;
                enemy.Stagger -= 3;
            }


        }
    }
}
