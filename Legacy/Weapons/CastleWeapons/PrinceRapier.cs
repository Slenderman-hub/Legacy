using Legacy.Enemies;
using Legacy.Interfaces;
using Legacy.Weapons.OtherWeapons;

namespace Legacy.Weapons.CastleWeapons
{
    public class PrinceRapier : Weapon, IPassiveSpecial, IResurrection, IPreSpecial
    {
        private bool trigger = false; 
        private bool buff = true;
        public PrinceRapier()
        {
            Name = "Рапира Принца";
            Damage = 5;
            Description = "Наследник Лича, посеял свою вещицу посреди цитадели. Занятно, но с такими игрушками лучше не баловаться. Опытный Искатель Смерти, войдет в любую роль, лишь бы настичь свою единственную цель";
            Special = "После первого использования, рапира получает благословение, которое в свою очередь отдаляет вас [1] раз от встречи со Смертью, оставляя вас с [10/10] О.З]. Как только рапира благославлена , она уничтожает все ваши прошлые и будущие оружия.";
            InventoryColor = ConsoleColor.White;
        }
        public void PassiveCast(Hero hero)
        {
            if (trigger)
            {
                foreach (var item in hero.HeroInventory.Weapons)
                {
                    if (item != this && item is not Fist)
                        hero.HeroInventory.Weapons.Remove(item);
                }
            }

        }

        public void PreCast(Hero hero, Enemy enemy)
        {
            if(Name == "Рапира Принца")
            {
                Name = "Рапира Принца [Благословена]";
                InventoryColor = ConsoleColor.DarkYellow;
                GameSession.Logger.Log($"Ваша душа чувствует королевские корни");
                trigger = true;
            }
        }

        public bool Resurrect(Hero hero)
        {
            if (trigger)
            {
                if (buff)
                {
                    hero.MaxHealth = 10;
                    hero.Health = 10;
                    Damage = 5.5m;
                    Name = "Рапира Принца [Проклята]";
                    InventoryColor = ConsoleColor.DarkGray;
                    Description = "Ты не можешь обманывать {её} вечно";
                    Special = "Неплохой урон";
                    
                    buff = false;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;

        }
    }
}
