using Legacy.Enemies;
using System;
namespace Legacy.Items
{
    public class HealingPotion : Item 
    {
        public HealingPotion() 
        {
            Name = "7# Подвиг Лекаря Шмякс";
            Description = "Зловонный отвар , который на вкус, как толпа бегемотов. Исцеляет [5] О.З . Советуем пить, только при смертельных случаях.";
            InventoryColor = ConsoleColor.Red;
        }

        public override void UseOnEnemy(Enemy enemy)
        {
            base.UseOnEnemy(enemy);
            if (enemy.Type == "Нежить")
            {
                enemy.Health = 1;

            }
            else
            {
                enemy.Health *= 2;
                enemy.IconColor = ConsoleColor.DarkGreen;
                enemy.LootItems.Add(new HealingPotion() { Name = "6# Подвиг Аптекаря Шмуньк", InventoryColor = ConsoleColor.DarkRed});
                enemy.LootItems.Add(new HealingPotion() { Name = "5# Подвиг Философа Чпеньк", InventoryColor = ConsoleColor.DarkRed });
                FloorSession.WriteNewPosition(enemy.Icon, enemy.Pos, enemy.IconColor);

            }
            
        }

        public override void UseOnHero(Hero hero)
        {
            base.UseOnHero(hero);

            if (hero.Health < 2)
            {
                hero.MaxHealth += 5;
                hero.Health = hero.MaxHealth;
            }
            else
            {
                hero.Health += 5;
                if (hero.Health > hero.MaxHealth)
                    hero.Health = hero.MaxHealth;
            }
            
        }


    }
}
