using System.Media;

namespace Legacy.Music
{
    static public class MusicPlayer
    {
        public static readonly string DIR_PATH = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Music\\";
        public static SoundPlayer Player;
        public static void Location(GameSession.Locations location)
        {
            if (location is GameSession.Locations.Castle)
                Play($"Castle{Random.Shared.Next(1, 3)}.wav");

            if (location is GameSession.Locations.Forest)
                Play( $"Forest{Random.Shared.Next(1, 3)}.wav");

        }
        public static void Boss(GameSession.Locations location)
        {
            if (location is GameSession.Locations.Castle)
                Play("CastleBoss.wav");
            if (location is GameSession.Locations.Forest)
                Play("ForestBoss.wav");
        }
        public static void GameOver() => Play("GameOver.wav");
        public static void Title() => Play("Title.wav");

        public static void Stop()
        {
            Player.Stop();
        }
        public static void Play(string fileName)
        {
            Player = new SoundPlayer(DIR_PATH + fileName);
            Player.PlayLooping();
        }
    }
}
