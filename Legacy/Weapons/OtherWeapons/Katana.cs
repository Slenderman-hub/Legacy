using Legacy;
using Legacy.Enemies;


namespace Legacy.Weapons
{
    public class Katana : Weapon, IPostSpecial
    {
        public Katana() 
        {
            Name = "Катана";
            Damage = 6.66m;
            Description = "Только настоящий самурай может совладать с отвественностью, что несет этот клинок";
            Special = "Прорубает противника насквозь, тем самым меняя вас местами. Накладывает герою [4] ошеломления, после использования";
            InventoryColor = ConsoleColor.Cyan;

        }
        public void PostCast(Hero hero, Enemy enemy)
        {
            if(enemy.Health > 0)
            {
                FloorSession.SwapPosition(hero, enemy);
                hero.Stagger += 4;
                FloorSession.WriteNewPosition(hero.Icon, hero.Pos, ConsoleColor.Cyan);
                FloorSession.WriteNewPosition(enemy.Icon, enemy.Pos, enemy.IconColor);
            }
        }
    }
}
