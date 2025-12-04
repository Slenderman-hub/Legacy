using Legacy.Enemies;

namespace Legacy.Weapons.OtherWeapons
{
    public class Fist : Weapon , IPostSpecial
    {
        public Fist() 
        {
            Name = "Кулак";
            Damage = 100;
            Description = "Все проблемы можно решить кулаками";
            Special = "Лечит на [1] здоровья, после убийства противника";

        }

        public void PostCast(Hero hero, Enemy enemy)
        {
            if(enemy.Health <= 0)
            {
                hero.Health += 1;
                if (hero.Health > hero.MaxHealth)
                    hero.Health = hero.MaxHealth;
            }

        }
    }
}
