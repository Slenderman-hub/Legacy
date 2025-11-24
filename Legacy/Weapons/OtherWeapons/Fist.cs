using Legacy.Enemies;

namespace Legacy.Weapons
{
    public class Fist : Weapon , IPostSpecial
    {
        public Fist() : base("Кулак", 1)
        {
            Description = "Все проблемы можно решить кулаками";
            Special = "Лечит на [2] здоровья, после убийства противника";

        }

        public void PostCast(Hero hero, Enemy enemy)
        {
            if(enemy.Health <= 0)
            {
                hero.Health += 2;
                if (hero.Health > hero.MaxHealth)
                    hero.Health = hero.MaxHealth;
            }

        }
    }
}
