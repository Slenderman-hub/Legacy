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
            Item item = null;
            int gold = 0;

            var result = Random.Shared.Next(1, 101);

            if (result == 100 && Random.Shared.Next(1,101) == 100)
            {
                var mimic = new Mimic(Pos.x, Pos.y);
                FloorSession.WriteNewPosition('M', mimic.Pos,mimic.IconColor);
                FloorSession.Entities.Add(mimic);
                mimic.Attack(GameSession.Hero);
                GameSession.Logger.Log($"Это не сундук!", ConsoleColor.DarkRed);
                return false;
            }
            else if(result >= 90)
            {
                item = new GrindStone();
                
                
            }
            else if (result >= 70)
            {
                item = new HealingPotion();
            }
            else if (result <= 50)
            {
                gold = result / 2;
            }
            else
            {
                gold = 10;
            }

            if(item != null)
            {
                GameSession.Hero.HeroInventory.Items.Add(item);
                GameSession.Logger.Log($"Вы нашли [{item.Name}] ", ConsoleColor.Yellow);
            }
            if(gold != 0)
            {
                GameSession.Hero.Gold += gold;
                GameSession.Logger.Log($"Вы нашли [{gold}] золота ", ConsoleColor.Yellow);
            }
            return true;
        }
    }
}
