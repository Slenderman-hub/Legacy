
using Legacy.Enemies;
namespace Legacy
{
    public interface IPostSpecial
    {
        public void PostCast(Hero hero, Enemy enemy);
    }
    public interface IPreSpecial
    {
        public void PreCast(Hero hero, Enemy enemy);
    }

    public interface IPassiveSpecial
    {
        public void PassiveCast(Hero hero);
    }
}
