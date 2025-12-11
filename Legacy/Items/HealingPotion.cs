using Legacy.Enemies;
using System;
namespace Legacy.Items
{
    public class HealingPotion : Item 
    {
        int healingEffect = 5;
        public HealingPotion() 
        {
            Name = "7# Подвиг Лекаря Шмякс";
            Description = $"Зловонный отвар , который на вкус, как толпа бегемотов. Исцеляет [{healingEffect}] О.З . Говорят, что те, кто пили этот отвар при смертельных случаях - расцветали.";
            InventoryColor = ConsoleColor.Green;
        }

        public override void UseOnEnemy(Enemy enemy)
        {
            base.UseOnEnemy(enemy);
            if (enemy.Type == "Нежить")
            {
                enemy.Health = 1;
                GameSession.Logger.Log($"Здоровье нежити, опустилось до минимального значения");

            }
            else
            {
                enemy.Health *= 2;
                enemy.IconColor = ConsoleColor.DarkGreen;
                enemy.LootItems.Add(new HealingPotion() { Name = "6# Подвиг Аптекаря Шмуньк", InventoryColor = ConsoleColor.DarkRed});
                enemy.LootItems.Add(new HealingPotion() { Name = "5# Подвиг Философа Чпеньк", InventoryColor = ConsoleColor.DarkRed });
                FloorSession.WriteNewPosition(enemy.Icon, enemy.Pos, enemy.IconColor);
                GameSession.Logger.Log($"[{enemy.Name}] расцвёл и стал кратно живучее");

            }
            
        }

        public override void UseOnHero(Hero hero)
        {
            base.UseOnHero(hero);

            if (hero.Health < 2)
            {
                hero.MaxHealth += healingEffect;
                hero.Health = hero.MaxHealth;
                GameSession.Logger.Log($"Ваша душа цветёт и прорастает на дополнительные [{healingEffect}] О.З", ConsoleColor.Green);
            }
            else
            {
                hero.Health += healingEffect;
                if (hero.Health > hero.MaxHealth)
                    hero.Health = hero.MaxHealth;
                GameSession.Logger.Log($"Вам стало лучше, хоть отвар и так себе...",ConsoleColor.DarkGreen);
            }
            
        }


    }
}
