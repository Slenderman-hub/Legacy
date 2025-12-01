using Legacy;

namespace Legacy.Items
{
    public class Portal : MapEntity
    {
        public GameSession.Locations Location;
        public int Level;
        public Portal(GameSession.Locations location, int level)
        {
            Icon = '@';
            Location = location;
            Level = level;
            IconColor = ConsoleColor.Cyan;
        }

        public void Teleport()
        {
            FloorSession.FloorIsRunning = false;
            GameSession.Level = Level;
            GameSession.Location = Location;
            GameSession.Hero.MaxHealth += GameSession.Hero.Gold / 100;
            GameSession.Hero.Gold = GameSession.Hero.Gold % 100;
            GameSession.Hero.Pos = (1, 1);

        }
    }
}
