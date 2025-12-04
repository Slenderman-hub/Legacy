using Legacy.Enemies;
using Legacy.Weapons.OtherWeapons;

namespace Legacy.Items
{
    public class GrindStone : Item
    {
        public GrindStone()
        {
            Name = "Точильный камень";
            Description = "В наши дни, такие камни на дороге не валяются! Самое то, чтобы наточить свою шпагу на [1.5] единиц урона, и [2х] кратно противнику";
            InventoryColor = ConsoleColor.Gray;
        }
        public override void UseOnEnemy(Enemy enemy)
        {
            enemy.LootWeapons.Add(new MonsterWeapon(enemy.Damage));
            enemy.Damage *= 2;
            enemy.Stagger -= 5;
            enemy.IconColor = ConsoleColor.DarkRed;
        }

        public override void UseOnHero(Hero hero) => hero.EquippedWeapon.Damage += 1.5m;

    }

}
