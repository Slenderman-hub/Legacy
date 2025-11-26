using Legacy;

namespace Legacy.Items
{
    public class Portal(GameSession.Locations location,int level) : MapEntity
    {
        public GameSession.Locations Location = location;
        public int Level = level;
        public void Teleport()
        {
            FloorSession.FloorIsRunning = false;
            GameSession.Level = Level;
            GameSession.Location = Location;

        }
    }
}
