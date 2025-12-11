
using Legacy.Enemies;

namespace Legacy.Items
{
    public class Item : IUseOnEnemy, IUseOnHero
    {
        public string Name { get; protected set; } = string.Empty;
        public string Description { get; protected set; } = string.Empty;
        public string Special { get; protected set; } = string.Empty;
        public ConsoleColor InventoryColor { get; protected set; } = ConsoleColor.White;

        
        public virtual void Consume()
        {
            GameSession.Hero.EquippedItem = null;
            GameSession.Hero.HeroInventory.Items.Remove(this);
        }

        public virtual void UseOnEnemy(Enemy enemy)
        {
            Consume();
        }

        public virtual void UseOnHero(Hero hero)
        {
            Consume();
        }
    }
}
