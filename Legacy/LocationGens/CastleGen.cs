using Legacy.Enemies;
using Legacy.Items;
using Legacy.Weapons;
using Legacy.Weapons.OtherWeapons;
using static Legacy.GameSession;
using static Legacy.FloorSession;
using static Legacy.MapGen;


namespace Legacy.LocationGens
{
    static class CastleGen 
    {
        
        public static void InitializeCastle()
        {
            FloorSession.Entities = new List<MapEntity>();

            Stack<(int x, int y)> stack = new Stack<(int x, int y)>();

            Map[1, 1] = '&';
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

                    Map[wallY, wallX] = ' ';


                    Map[next.y, next.x] = ' ';


                    stack.Push(next);
                }
                else
                {
                    DeadEnds.Add((current.x, current.y));
                }
            }
            FreeCells = DeadEnds.ToList();
            InitializeEnemies();
            InitializeWeapons();
            InitializeСhests();
            
        }
        private static void InitializeСhests()
        {
            int i = 0;
            while (i != HEIGHT)
            {
                i++;
                var spawn = FreeCells[Random.Shared.Next(FreeCells.Count)];
                FreeCells.Remove(spawn);
                if (spawn.x != 1 && spawn.y != 1)
                {
                        var gs = new Chest() { Pos = spawn };
                        Map[spawn.y, spawn.x] = gs.Icon;
                        Entities.Add(gs);
                }
            }
        }
        private static void InitializeWeapons()
        {
            int lvlMod = Level % 10;
            int i = 0;
            while(i != 2)
            {
               i++;
               var spawn = FreeCells[Random.Shared.Next(FreeCells.Count)];
                FreeCells.Remove(spawn);
                if(spawn.x != 1 && spawn.y != 1)
                {
                    switch (Random.Shared.Next(4 + lvlMod))
                    {
                        case 0:

                        case 1:
                            var cs = new ClericStaff() { Pos = spawn };
                            Map[spawn.y, spawn.x] = cs.Icon;
                            Entities.Add(cs);
                            break;
                        case 2:
                            var c = new Claymore() { Pos = spawn };
                            Map[spawn.y, spawn.x] = c.Icon;
                            Entities.Add(c);
                            break;
                        case 3:
                            var k = new Katana() { Pos = spawn};
                            Map[spawn.y, spawn.x] = k.Icon;
                            Entities.Add(k);
                            break;
                        case 4:
                            var ob = new Oathbladec() { Pos = spawn };
                            Map[spawn.y, spawn.x] = ob.Icon;
                            Entities.Add(ob);
                            break;
                        case 5:
                        case 6:
                        case 7:

                        default:
                            break;
                    }
                }
            
            }

        }
        private static void InitializeEnemies()
        {
            int lvlMod = Level == 13 ? 2 : Level % 10;
            int i = 0;
            while (i != HEIGHT * lvlMod)
            {
                var spawn = FreeCells[Random.Shared.Next(FreeCells.Count)];
                if (spawn.x >= WIDTH / 8 || spawn.y >= HEIGHT / 8)
                {
                    i++;
                    FreeCells.Remove(spawn);
                    switch (Random.Shared.Next(1))
                    {
                        case 0:
                            var knight = new Knight(spawn.x, spawn.y);
                            Map[spawn.y, spawn.x] = knight.Icon;
                            Entities.Add(knight);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static List<(int x, int y)> GetUnvisitedNeighbors(int x, int y)
        {
            List<(int x, int y)> neighbors = new List<(int x, int y)>();
            if (x - 2 >= 1 && Map[y, x - 2] == '|')
                neighbors.Add((x - 2, y));
            if (x + 2 < WIDTH - 1 && Map[y, x + 2] == '|')
                neighbors.Add((x + 2, y));
            if (y - 2 >= 1 && Map[y - 2, x] == '|')
                neighbors.Add((x, y - 2));
            if (y + 2 < HEIGHT - 1 && Map[y + 2, x] == '|')
                neighbors.Add((x, y + 2));

            return neighbors;
        }


    }
}

