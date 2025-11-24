using Items;
using Legacy.Weapons;
using Legacy;

namespace Legacy
{
    public class Inventory
    {
        public List<Weapon> Weapons = new List<Weapon>();
        public List<Item> Items = new List<Item>();
        public void DisplayInventory()
        {
            //Console.Clear();
            //foreach (var item in Enumerable.Range(0,GameSession.WIDTH))
            //    Console.Write("-");

            //Console.ReadLine();
        }
    }
}
