using Legacy.Interfaces;

namespace Legacy.Weapons
{
    public class Fist : Weapon, IVampirism
    {
        public Fist() : base("Кулак", 1)
        {
            Description = "Все проблемы можно решить кулаками \n\n\n  * [Лечит на 2 здоровья, после убийства противника]";
        }

        public void Heal(Hero hero)
        {
            hero.Health += 2;
            if (hero.Health > hero.MaxHealth)
                hero.Health = hero.MaxHealth;
        }
    }
}
