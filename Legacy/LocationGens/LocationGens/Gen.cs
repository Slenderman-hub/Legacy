using Legacy.Items;
using Legacy.Weapons;
using Legacy.Weapons.CastleWeapons;
using Legacy.Weapons.OtherWeapons;

namespace Legacy.LocationGens
{
    public abstract class Gen
    {
        protected List<(int x, int y)> FreeCells = new List<(int x, int y)>();
        public void GenerateLocation()
        {
            FloorSession.Entities = new List<MapEntity>();
            GenerateCore();
            GeneratePattern();
            WallFix();
            GeneratePortal();
            GenerateEnemies();
            GenerateWeapons();
            GenerateСhests();


        }


        protected virtual void GenerateCore()
        {
            Stack<(int x, int y)> stack = new Stack<(int x, int y)>();
            FloorSession.Map[1, 1] = '&';
            GameSession.Hero.Pos = (1, 1);
            stack.Push((1, 1));

            var DeadEnds = new HashSet<(int x, int y)>();
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                var neighbors = GetUnvisitedNeighbors(current.x, current.y);

                if (neighbors.Count > 0)
                {
                    stack.Push(current);
                    var next = neighbors[Random.Shared.Next(neighbors.Count)];

                    int wallX = (current.x + next.x) / 2;
                    int wallY = (current.y + next.y) / 2;

                    FloorSession.Map[wallY, wallX] = ' ';


                    FloorSession.Map[next.y, next.x] = ' ';


                    stack.Push(next);
                }
                else
                {
                    DeadEnds.Add((current.x, current.y));
                }
            }
            FreeCells = DeadEnds.ToList();
            FreeCells.Remove((1, 1));

        List<(int x, int y)> GetUnvisitedNeighbors(int x, int y)
        {
            List<(int x, int y)> neighbors = new List<(int x, int y)>();
            if (x - 2 >= 1 && FloorSession.Map[y, x - 2] == '|')
                neighbors.Add((x - 2, y));
            if (x + 2 < GameSession.MAP_WIDTH - 1 && FloorSession.Map[y, x + 2] == '|')
                neighbors.Add((x + 2, y));
            if (y - 2 >= 1 && FloorSession.Map[y - 2, x] == '|')
                neighbors.Add((x, y - 2));
            if (y + 2 < GameSession.MAP_HEIGHT - 1 && FloorSession.Map[y + 2, x] == '|')
                neighbors.Add((x, y + 2));

            return neighbors;
        }
    }
        protected abstract void GeneratePattern();
        public static void WallFix()
        {
            for (int y = 0; y < GameSession.MAP_HEIGHT; y++)
            {
                for (int x = 0; x < GameSession.MAP_WIDTH; x++)
                {
                    char wall = '|';
                    if (x == 0)
                        FloorSession.Map[y, x] = wall;
                    if (y == 0)
                        FloorSession.Map[y, x] = wall;
                    if (x == GameSession.MAP_WIDTH - 1)
                        FloorSession.Map[y, x] = wall;
                    if (y == GameSession.MAP_HEIGHT - 1)
                        FloorSession.Map[y, x] = wall;

                }
            }
            for (int i = 0; i < GameSession.MAP_HEIGHT; i++)
            {
                for (int j = 0; j < GameSession.MAP_WIDTH; j++)
                {
                    if (FloorSession.Map[i, j] == '|')
                    {
                        bool top = i > 0 && FloorSession.Map[i - 1, j] != ' ';
                        bool bottom = i < GameSession.MAP_HEIGHT - 1 && FloorSession.Map[i + 1, j] != ' ';
                        bool left = j > 0 && FloorSession.Map[i, j - 1] != ' ';
                        bool right = j < GameSession.MAP_WIDTH - 1 && FloorSession.Map[i, j + 1] != ' ';

                        FloorSession.Map[i, j] = GetWallChar(top, bottom, left, right);
                    }
                    else
                        FloorSession.Map[i, j] = FloorSession.Map[i, j];
                }
            }
            char GetWallChar(bool top, bool bottom, bool left, bool right)
            {
                if (top && bottom && !left && !right) return '│';

                if (!top && !bottom && left && right) return '─';

                if (top && right && !bottom && !left) return '└';
                if (top && left && !bottom && !right) return '┘';
                if (bottom && right && !top && !left) return '┌';
                if (bottom && left && !top && !right) return '┐';

                if (top && bottom && right && !left) return '├';
                if (top && bottom && left && !right) return '┤';
                if (bottom && left && right && !top) return '┬';
                if (top && left && right && !bottom) return '┴';

                if (top && bottom && left && right) return '┼';

                if (top && !bottom && !left && !right) return '┴';
                if (bottom && !top && !left && !right) return '┬';
                if (left && !right && !top && !bottom) return '┤';
                if (right && !left && !top && !bottom) return '├';

                return '|';

            }
        }
        protected virtual void GeneratePortal()
        {

                var spawn = FreeCells[Random.Shared.Next(FreeCells.Count)];
                    FreeCells.Remove(spawn);
                    var gs = new Portal(GameSession.Location, GameSession.Level + 1);
                    gs.Pos = spawn;
                    FloorSession.Map[spawn.y, spawn.x] = gs.Icon;
                    FloorSession.Entities.Add(gs);
        }
        protected abstract void GenerateEnemies();

        protected virtual void GenerateWeapons()
        {
            var spawn = FreeCells[Random.Shared.Next(FreeCells.Count)];
            FreeCells.Remove(spawn);
            switch (Random.Shared.Next(11))
            {
                case 0:
                    var k = new Katana() { Pos = spawn };
                    FloorSession.Map[spawn.y, spawn.x] = k.Icon;
                    FloorSession.Entities.Add(k);
                    break;
                case 1:
                    var oB = new Oathblade() { Pos = spawn };
                    FloorSession.Map[spawn.y, spawn.x] = oB.Icon;
                    FloorSession.Entities.Add(oB);
                    break;
                case 2:
                    var hS = new HeroSword() { Pos = spawn, curentLevel = GameSession.Level };
                    FloorSession.Map[spawn.y, spawn.x] = hS.Icon;
                    FloorSession.Entities.Add(hS);
                    break;
                case 3:
                    var wS = new WoodenSword() { Pos = spawn };
                    FloorSession.Map[spawn.y, spawn.x] = wS.Icon;
                    FloorSession.Entities.Add(wS);
                    break;
                case 4:
                    var g = new Glaive() { Pos = spawn };
                    FloorSession.Map[spawn.y, spawn.x] = g.Icon;
                    FloorSession.Entities.Add(g);
                    break;
                case 5:
                    var fWS = new FakeWoodenSword() { Pos = spawn };
                    FloorSession.Map[spawn.y, spawn.x] = fWS.Icon;
                    FloorSession.Entities.Add(fWS);
                    break;
                case 6:
                    var eS = new EnvyScythe() { Pos = spawn, currentLevel = 0 };
                    FloorSession.Map[spawn.y, spawn.x] = eS.Icon;
                    FloorSession.Entities.Add(eS);
                    break;
                case 7:
                    var dS = new DeathScythe() { Pos = spawn };
                    FloorSession.Map[spawn.y, spawn.x] = dS.Icon;
                    FloorSession.Entities.Add(dS);
                    break;
                case 8:
                    var h = new Horn() { Pos = spawn, currentLevel = 0 };
                    FloorSession.Map[spawn.y, spawn.x] = h.Icon;
                    FloorSession.Entities.Add(h);
                    break;
                case 9:
                    var sL = new SoulLantern() { Pos = spawn };
                    FloorSession.Map[spawn.y, spawn.x] = sL.Icon;
                    FloorSession.Entities.Add(sL);
                    break;
                default:
                    break;
            }
        }
        protected virtual void GenerateСhests()
        {
            for (int i = 0; i < GameSession.MAP_HEIGHT; i++)
            {
                var spawn = FreeCells[Random.Shared.Next(FreeCells.Count)];
                FreeCells.Remove(spawn);
                var gs = new Chest() { Pos = spawn };
                FloorSession.Map[spawn.y, spawn.x] = gs.Icon;
                FloorSession.Entities.Add(gs);
            }
        }

        
    }
    
}
