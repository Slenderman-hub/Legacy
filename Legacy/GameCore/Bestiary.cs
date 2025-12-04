using Legacy.Enemies;
using System.Reflection;
using System.Text.Json;

namespace Legacy
{
    public class Bestiary
    {
        private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LegacyData", "Bestiary.json");
        public Dictionary<string, Enemy> Creatures { get; set; } = new Dictionary<string, Enemy>();
        public Bestiary() => Load();
        private void Load()
        {
            if (!File.Exists(_path))
                return;

            string json = File.ReadAllText(_path);
            Creatures = JsonSerializer.Deserialize<Dictionary<string, Enemy>>(json);

        }
        public void Save()
        {
            string json = JsonSerializer.Serialize(Creatures);

            if (!Directory.Exists(Path.GetDirectoryName(_path)))
                Directory.CreateDirectory(Path.GetDirectoryName(_path));

            File.WriteAllText(_path, json);
        }
    }
}
