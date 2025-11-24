using Legacy.Enemies;

namespace Legacy
{
    public interface IUseOnHero
    {
        public void UseOnHero(Hero hero);
    }
    public interface IUseOnEnemy
    {
        public void UseOnEnemy(Enemy enemy);
    }

}
