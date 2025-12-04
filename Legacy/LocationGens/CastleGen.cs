using Legacy.Enemies;
using Legacy.Items;
using Legacy.Weapons;
using Legacy;


namespace Legacy.LocationGens
{
    static class CastleGen 
    {
        public static List<(int x, int y)> FreeCells = new List<(int x, int y)>();
        public static void InitializeCastle()
        {
            FloorSession.Entities = new List<MapEntity>();

            Stack<(int x, int y)> stack = new Stack<(int x, int y)>();

            FloorSession.Map[1, 1] = '&';
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
            InitializePattern();
            FreeCells = DeadEnds.ToList();

            InitializePortals();
            InitializeEnemies();
            InitializeWeapons();
            InitializeСhests();
            
        }

        private static void InitializePattern()
        {
            switch (Random.Shared.Next(0,101))
            {
                case <= 30:
                    CastleGenPatterns.SquareHalls();
                    break;
                case <= 50:
                    CastleGenPatterns.Square();
                    CastleGenPatterns.SquareHalls();
                    break;
                case <= 60 :
                    CastleGenPatterns.Square();
                    break;
                case <= 70:
                    CastleGenPatterns.ZigZag();
                    break;
                case <= 80:
                    CastleGenPatterns.Target();
                    break;
                default:
                    CastleGenPatterns.Square();
                    break;
            }
            CastleGenPatterns.WallFix();
        }

        private static void InitializeСhests()
        {
            int i = 0;
            while (i != GameSession.MAP_HEIGHT)
            {
                i++;
                var spawn = FreeCells[Random.Shared.Next(FreeCells.Count)];
                FreeCells.Remove(spawn);
                if (spawn.x != 1 && spawn.y != 1)
                {
                        var gs = new Chest() { Pos = spawn };
                    FloorSession.Map[spawn.y, spawn.x] = gs.Icon;
                    FloorSession.Entities.Add(gs);
                }
            }
        }
        private static void InitializePortals()
        {
            int i = 0;
            while (i != 1)
            {
                var spawn = FreeCells[Random.Shared.Next(FreeCells.Count)];
                

                if (spawn.x != 1 && spawn.y != 1)
                {
                    i++;
                    FreeCells.Remove(spawn);
                    var gs = new Portal(GameSession.Location, GameSession.Level + 1);
                    gs.Pos = spawn;
                    FloorSession.Map[spawn.y, spawn.x] = gs.Icon;
                    FloorSession.Entities.Add(gs);
                }
            }
        }
        private static void InitializeWeapons()
        {
            int lvlMod = GameSession.Level % 10;
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
                            FloorSession.Map[spawn.y, spawn.x] = cs.Icon;
                            FloorSession.Entities.Add(cs);
                            break;
                        case 2:
                            var c = new Claymore() { Pos = spawn };
                            FloorSession.Map[spawn.y, spawn.x] = c.Icon;
                            FloorSession.Entities.Add(c);
                            break;
                        case 3:
                            var k = new Katana() { Pos = spawn};
                            FloorSession.Map[spawn.y, spawn.x] = k.Icon;
                            FloorSession.Entities.Add(k);
                            break;
                        case 4:
                            var ob = new Oathblade() { Pos = spawn };
                            FloorSession.Map[spawn.y, spawn.x] = ob.Icon;
                            FloorSession.Entities.Add(ob);
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
            int lvlMod = GameSession.Level == 13 ? 2 : GameSession.Level % 10;
            int i = 0;
            while (i != GameSession.MAP_HEIGHT * lvlMod)
            {
                var spawn = FreeCells[Random.Shared.Next(FreeCells.Count)];
                if (spawn.x >= GameSession.MAP_WIDTH / 8 || spawn.y >= GameSession.MAP_HEIGHT / 8)
                {
                    i++;
                    FreeCells.Remove(spawn);
                    switch (Random.Shared.Next(1))
                    {
                        case 0:
                            var knight = new Knight(spawn.x, spawn.y);
                            FloorSession.Map[spawn.y, spawn.x] = knight.Icon;
                            FloorSession.Entities.Add(knight);
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
}

