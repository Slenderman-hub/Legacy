using System.Xml.Linq;

namespace Legacy.Weapons
{
    public class Weapon : MapEntity
    {
        public Weapon()
        {
            Icon = '!';
        }
        public string Name { get; protected set; } = string.Empty;
        public string Description { get; protected set; } = string.Empty;
        public string Special { get; protected set; } = string.Empty;
        public decimal Damage { get; set; }
    }
}