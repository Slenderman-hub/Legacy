using Legacy.Enemies;
namespace Legacy.Items
{
    public class HealingPotion : Item , IUseOnEnemy, IUseOnHero
    {
        public HealingPotion() : base("Семь Потов Лекаря Шмякс")
        {
            Description = "Зловонный отвар , исцеляющий [5] О.З . Советуем пить, только при смертельных случаях";
        }

        public void UseOnEnemy(Enemy enemy)
        {
            if (enemy.Type == "Нежить")
                enemy.Health = 1;
        }

        public void UseOnHero(Hero hero)
        {
            if(hero.Health < 2)
            {
                hero.MaxHealth += 5;
                hero.Health = hero.MaxHealth;
            }
            else
            {
                hero.Health += 5;
                if (hero.Health > hero.MaxHealth)
                    hero.Health = hero.MaxHealth;
            }
        }


    }
}
