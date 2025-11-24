using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Legacy;
namespace Legacy.Items
{
    public class Chest
    {
        public char Icon { get; protected set; } = '#';
        public (int x, int y) Pos;
        public virtual bool Open()
        {
            var result = Random.Shared.Next(1, 101);
            if (result == 100)
            {
                FloorSession.WriteNewPosition('M', Pos);
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
