using System.Xml.Linq;

namespace Legacy.Weapons
{
    public class Weapon(string name, decimal damage)
    {
        public char Icon { get; protected set; } = '!';
        public (int x, int y) Pos;
        public string Name { get; protected set; } = name;
        public string Description { get; protected set; } = string.Empty;
        public string Special { get; protected set; } = string.Empty;
        public decimal Damage { get; set; } = damage;
    }
}