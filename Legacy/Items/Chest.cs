using Legacy;
using Legacy.Enemies;
namespace Legacy.Items
{
    public class Chest : MapEntity
    {
        public Chest()
        {
            Icon = '#';
            IconColor = ConsoleColor.Yellow;
        }
        public virtual bool Open()
        {
            var result = Random.Shared.Next(1, 101);
            if (result == 100 && Random.Shared.Next(1,101) == 100)
            {
                var mimic = new Mimic(Pos.x, Pos.y);
                FloorSession.WriteNewPosition('M', mimic.Pos,mimic.IconColor);
                FloorSession.Entities.Add(mimic);
                mimic.Attack(GameSession.Hero);

                return false;
            }

            if(result >= 90)
            {
                GameSession.Hero.HeroInventory.Items.Add(new GrindStone());
            }
            else if (result >= 70)
            {
                GameSession.Hero.HeroInventory.Items.Add(new HealingPotion());
            }
            else if (result >= 50)
            {
                GameSession.Hero.Gold += result / 2;
            }

            return true;
        }
    }
}
