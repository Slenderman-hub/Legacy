using System.Xml.Linq;

namespace Legacy.Weapons
{
    public class Weapon(string name, int damage)
    {
        public string Name { get; protected set; } = name;
        public string Description { get; protected set; } = string.Empty;
        public int Damage { get; set; } = damage;
    }
}