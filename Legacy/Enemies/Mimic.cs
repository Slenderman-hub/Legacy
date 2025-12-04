using Legacy.Weapons.OtherWeapons;

namespace Legacy.Enemies
{
    public class Mimic : Enemy
    {
        public Mimic(int x, int y) : base(x, y)
        {
            Icon = 'M';
            Name = "Мимик";
            Description = "Хищное растение, что любит поживится искателями смерти, прикидываясь сундуком";
            Type = "Растение";
            Health = 50;
            Damage = 10;
            Stagger = 0;
        }
        public override void Attack(Hero hero)
        {
            if (hero.EquippedWeapon is not Fist)
            {
                this.LootWeapons.Add(hero.EquippedWeapon);
                hero.HeroInventory.Weapons.Remove(hero.EquippedWeapon);
                hero.EquippedWeapon = hero.HeroInventory.Weapons[0];

            }
            else
            {
                base.Attack(hero);
            }

        }
    }
}
