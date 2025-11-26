using Legacy;
using Legacy.Enemies;
namespace Legacy.Items
{
    public class Chest : MapEntity
    {
        public Chest()
        {
            Icon = '#';
        }
        public virtual bool Open()
        {
            var result = Random.Shared.Next(1, 101);
            if (result == 100)
            {
                FloorSession.WriteNewPosition('M', Pos);
                FloorSession.Entities.Add(new Mimic(Pos.x,Pos.y));
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
                GameSession.Hero.Gold += 25;
            }
            else if (result >= 30)
            {
                GameSession.Hero.Gold += 15;
            }
            else
            {
                GameSession.Hero.Gold += 5;
                
            }
            return true;
        }
    }
}
