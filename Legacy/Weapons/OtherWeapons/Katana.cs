using Legacy;
using Legacy.Enemies;
using static System.Net.Mime.MediaTypeNames;

namespace Legacy.Weapons.OtherWeapons
{
    public class Katana : Weapon, IPostSpecial
    {
        public Katana() 
        {
            Name = "Катана";
            Damage = 6.66m;
            Description = "Только настоящий самурай может совладать с отвественностью, что несет этот клинок";
            Special = "Прорубает противника насквозь, тем самым меняя вас местами. Накладывает герою [4] ошеломления, после использования";

        }
        public void PostCast(Hero hero, Enemy enemy)
        {
            if(enemy.Health > 0)
            {
                var temp = hero.Pos;
                hero.Pos = enemy.Pos;
                enemy.Pos = temp;
                hero.Stagger += 4;
                FloorSession.WriteNewPosition(hero.Icon, hero.Pos, ConsoleColor.Cyan);
                FloorSession.WriteNewPosition(enemy.Icon, enemy.Pos);
            }
        }
    }
}
