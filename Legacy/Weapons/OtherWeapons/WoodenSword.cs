using Legacy.Enemies;
using Legacy.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy.Weapons.OtherWeapons
{
    public class WoodenSword : Weapon, IPreSpecial
    {
        private Excalibur _lastState = new Excalibur();
        public WoodenSword()
        {
            
            Name = "Деревянный меч";
            Damage = 0.1m;
            Description = "Такой палкой, только крапиву бить! Никак не чудовищ, что кишат кругом, но быть может, Искатель Смерти найдет ему применение?";
            Special = "Such a cringe";
            InventoryColor = ConsoleColor.White;

            _lastState._lastState = this;

        }
        public void PreCast(Hero hero, Enemy enemy)
        {
            if (hero.Health == hero.MaxHealth)
            {
                GameSession.Logger.Log($"Стойкость сердца наполяет вашу десницу",ConsoleColor.DarkYellow);
                hero.HeroInventory.Weapons.Remove(this);
                hero.HeroInventory.Weapons.Add(_lastState);
                hero.EquippedWeapon = _lastState;

            }
            
        }
    }
    public class Excalibur : Weapon , IPreSpecial
    {
        public WoodenSword _lastState = null;
        public Excalibur()
        {
            Name = "Экскалибур";
            Damage = 10;
            Description = "Давным-давно, один принц, не смог вытащить меч из камня, или наоборот? Никто не помнит! Но, это проклятое место, помнит всё...";
            Special = "Когда ваше  здоровье соотвествует максимальному, Экскалибур сохраняет свою форму";
            InventoryColor = ConsoleColor.Magenta;
        }

        public void PreCast(Hero hero, Enemy enemy)
        {
            if (hero.Health != hero.MaxHealth)
            {
                hero.HeroInventory.Weapons.Remove(this);
                hero.HeroInventory.Weapons.Add(_lastState);
                hero.EquippedWeapon = _lastState;

            }
        }
    }
   
}
