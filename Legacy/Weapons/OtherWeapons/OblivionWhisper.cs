using Legacy.Enemies;
using System;

namespace Legacy.Weapons.OtherWeapons
{
    public class OblivionWhisper : Weapon, IPreSpecial
    {
        private List<string> _currentStack = new List<string>();
        public OblivionWhisper()
        {
            Name = "Шёпот Забвения";
            Damage = 2;
            Description = "На вид, очень скромный кинжал, но на деле является очень мощным инструментом в руках опытного Искателя Смерти. Знания это сила, с которой нужно считатся";
            Special = "Улучшается на [1] единицу урона, в обмен за 1 страницу из бестиария. Каждый обмен происходит при ударе кинжалом по врагу. ";
            InventoryColor = ConsoleColor.Red;

        }
        public void PreCast(Hero hero, Enemy enemy)
        {
            if(GameSession.Hero.HeroInventory.Bestiary.Creatures.Count > 0)
            {

                var keys = GameSession.Hero.HeroInventory.Bestiary.Creatures.ToList();
                foreach (var item in keys)
                {
                    if (!_currentStack.Contains(item.Key))
                    {
                        Damage++;
                        _currentStack.Add(item.Key);
                        GameSession.Logger.Log($"Страницу [{item.Key}] настигает забвение", ConsoleColor.DarkMagenta);
                        GameSession.Hero.HeroInventory.Bestiary.Creatures.Remove(item.Key);
                        break;

                    }
                }
            }
        }
    }
}
