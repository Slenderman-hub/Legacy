using Legacy.Enemies;
using Legacy.Items;
using Legacy.Weapons;

namespace Legacy
{
    public class Inventory
    {
        public List<Weapon> Weapons = new List<Weapon>();
        public List<Item> Items = new List<Item>();
        public Bestiary  Bestiary= new Bestiary();
    }
}
