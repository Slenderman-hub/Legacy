namespace Legacy.LocationGens
{
    public static class CastleGenPatterns
    {
        public static void Square()
        {
            for (int y = GameSession.MAP_HEIGHT / 4; y < GameSession.MAP_HEIGHT / 4 * 3; y++)
            {
                for (int x = GameSession.MAP_WIDTH / 4; x < GameSession.MAP_WIDTH / 4 * 3; x++)
                {
                    FloorSession.Map[y, x] = ' ';
                }
            }
            
        }
        public static void SquareHalls()
        {
            int gridSize = 6; 
            int squareSize = GameSession.MAP_HEIGHT / (gridSize * 2); 

            for (int gridY = 0; gridY < gridSize; gridY++)
            {
                for (int gridX = 0; gridX < gridSize; gridX++)
                {
                    if (gridY == 1 && gridX == 1) continue;

                    int startY = GameSession.MAP_HEIGHT / (gridSize + 1) * (gridY + 1) - squareSize / 2;
                    int startX = GameSession.MAP_WIDTH / (gridSize + 1) * (gridX + 1) - squareSize / 2;

                    for (int y = startY; y < startY + squareSize; y++)
                    {
                        for (int x = startX; x < startX + squareSize; x++)
                        {
                            FloorSession.Map[y, x] = ' ';
                        }
                    }
                }
            }
        }
        public static void Target()
        {
            int centerSize = GameSession.MAP_HEIGHT / 3;
            int armWidth = GameSession.MAP_HEIGHT / 8;

            // Вертикальная полоса
            for (int y = 0; y < GameSession.MAP_HEIGHT; y++)
            {
                for (int x = GameSession.MAP_WIDTH / 2 - armWidth / 2;
                     x < GameSession.MAP_WIDTH / 2 + armWidth / 2; x++)
                {
                    FloorSession.Map[y, x] = ' ';
                }
            }

            // Горизонтальная полоса
            for (int y = GameSession.MAP_HEIGHT / 2 - armWidth / 2;y < GameSession.MAP_HEIGHT / 2 + armWidth / 2; y++)
            {
                for (int x = 0; x < GameSession.MAP_WIDTH; x++)
                {
                    FloorSession.Map[y, x] = ' ';
                }
            }

            // Центральный квадрат на пересечении
            int centerStartY = GameSession.MAP_HEIGHT / 2 - centerSize / 2;
            int centerStartX = GameSession.MAP_WIDTH / 2 - centerSize / 2;
            for (int y = centerStartY; y < centerStartY + centerSize; y++)
            {
                for (int x = centerStartX; x < centerStartX + centerSize; x++)
                {
                    FloorSession.Map[y, x] = ' ';
                }
            }
        }
        public static void ZigZag()
        {
            int waveHeight = GameSession.MAP_HEIGHT / 8;
            int waveWidth = GameSession.MAP_WIDTH / 20;

            for (int wave = 0; wave < 8; wave++)
            {
                int baseY = wave * waveHeight * 2;

                for (int y = baseY; y < baseY + waveHeight; y++)
                {
                    if (y >= GameSession.MAP_HEIGHT) break;

                    for (int x = 0; x < GameSession.MAP_WIDTH; x++)
                    {
                        int offset = (int)(Math.Sin(x / (double)waveWidth) * waveHeight / 3);
                        if (y >= baseY + offset && y < baseY + waveHeight / 2 + offset)
                        {
                            FloorSession.Map[y, x] = ' ';
                        }
                    }
                }
            }


        }
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
                    if(x == GameSession.MAP_WIDTH - 1)
                        FloorSession.Map[y, x] = wall;
                    if (y == GameSession.MAP_HEIGHT - 1)
                        FloorSession.Map[y, x] = wall;

                }
            }
        }
    }
}
