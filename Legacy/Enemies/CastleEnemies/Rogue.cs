using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Enemies.CastleEnemies
{
    public class Rogue : Enemy
    {
        public Rogue(int x, int y) : base(x, y)
        {
            Icon = 'R';
            Name = "Плут";
            Description = "Ранее желающий поживиться наследием Лича, пройдоха-плут, ныне скованный проклятием, Страж его владений. Вдвойне быстр в своих действиях, но тем не менее силенок пережить больше двух ударов, у него врятли найдется. При каждом ударе, выбивает из вас 10 золота, чтобы покрыть свой микрозайм";
            Type = "Нежить";
            Health = 1;
            Damage = 4;
            Stagger = 0;
            IconColor = ConsoleColor.DarkYellow;
        }
        public override void Action(GameSession.Actions action)
        {
            base.Action(action);
            base.Action(action);
        }
        public override void Attack(Hero hero)
        {
            base.Attack(hero);
            hero.Gold -= 10;
            if (hero.Gold < 0)
                hero.Gold = 0;
        }
        public override void OnDeath()
        {
            base.OnDeath();
            if (Random.Shared.Next(2) == 0)
                GameSession.Hero.Gold += 20;

        }
    }
}
