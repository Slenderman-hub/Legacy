using Legacy.Weapons;
using Legacy;

namespace Legacy.Enemies
{
    public class Mimic : Enemy
    {
        public Mimic(int x, int y) : base(x, y)
        {
            Icon = 'M';
            Name = "Мимик";
            Description = "Хищное растение, что любит поживится искателями сокровищ, прикидываясь сундуком";
            Type = "Растение";
            Health = 50;
            Damage = 5;
            Stagger = 1;
        }
        protected override void Attack(Hero hero)
        {
            if (hero.EquipedWeapon is not Fist)
            {
                this.LootWeapons.Add(hero.EquipedWeapon);
                hero.HeroInventory.Weapons.Remove(hero.EquipedWeapon);
                hero.EquipedWeapon = hero.HeroInventory.Weapons[0];
            }
            hero.Health -= Damage;
            if (hero.Health <= 0)
            {
                FloorSession.WriteNewPosition('X', (hero.Pos.x, hero.Pos.y), ConsoleColor.Red);
            }

        }
    }
}
