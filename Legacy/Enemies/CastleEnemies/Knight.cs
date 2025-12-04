using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Enemies
{
    public class Knight : Enemy
    {
        public Knight(int x, int y) : base(x, y)
        {
            Icon = 'K';
            Name = "Рыцарь";
            Description = "Стальное сердце закаляет от ударов, а боевой дух не позволяет находится долго в ошеломлении. Творческий кризис распространен среди рыцарей, поэтому уникальными навыками, они не обладают";
            Type = "Нежить";
            Health = 7;
            Damage = 3;
            Stagger = 0;
        }
        
    }
}
