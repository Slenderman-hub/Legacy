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
            Description = "Стальное сердце закаляет от ударов, а боевой дух не позволяет находится долго в ошеломлении.";
            Type = "Нежить";
            Health = 10;
            Damage = 2;
            Stagger = -1;
        }
        
    }
}
