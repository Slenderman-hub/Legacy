using Legacy;
using Legacy.Enemies;
using static Legacy.GameSession;
using static Legacy.MapGen;


namespace Legacy.LocationGens
{
    static class CastleGen
    {
        public static void InitializeCastle(int level)
        {
            Stack<(int x, int y)> stack = new Stack<(int x, int y)>();
            int startX = 1;
            int startY = 1;

            Map[startY, startX] = 'I';

            var DeadEnds = new HashSet<(int x, int y)>();
            stack.Push((startX, startY));

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
            var spawnEnds = DeadEnds.ToList();
            int i = 0;
            while (i != HEIGHT * 2)
            {
                var spawn = spawnEnds[Random.Shared.Next(spawnEnds.Count)];
                if (spawn.x >= WIDTH / 8 || spawn.y >= HEIGHT / 8)
                {
                    i++;
                    switch (Random.Shared.Next(1))
                    {
                        case 0:
                            var knight = new Knight(spawn.x, spawn.y);
                            Map[spawn.y, spawn.x] = knight.Icon;
                            MapGen.Enemies.Add(knight);
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

