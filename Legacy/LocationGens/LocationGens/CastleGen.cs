using Legacy.Enemies;
using Legacy.Enemies.CastleEnemies;
using Legacy.Weapons;
using Legacy.Weapons.CastleWeapons;
using Legacy.Weapons.OtherWeapons;

namespace Legacy.LocationGens.LocationGens
{
    public class CastleGen : Gen
    {
        protected override void GenerateWeapons()
        {
            base.GenerateWeapons();

            for (int i = 0; i < 1; i++)
            {
                var spawn = FreeCells[Random.Shared.Next(FreeCells.Count)];
                FreeCells.Remove(spawn);
                switch (Random.Shared.Next(6))
                {
                    case 0:
                        var c = new Claymore() { Pos = spawn };
                        FloorSession.Map[spawn.y, spawn.x] = c.Icon;
                        FloorSession.Entities.Add(c);
                        break;
                    case 1:
                        var cS = new ClericStaff() { Pos = spawn };
                        FloorSession.Map[spawn.y, spawn.x] = cS.Icon;
                        FloorSession.Entities.Add(cS);
                        break;
                    case 2:
                        var gS = new GardenShears() { Pos = spawn};
                        FloorSession.Map[spawn.y, spawn.x] = gS.Icon;
                        FloorSession.Entities.Add(gS);
                        break;
                    case 3:
                        var gD = new GreedDaggers() { Pos = spawn };
                        FloorSession.Map[spawn.y, spawn.x] = gD.Icon;
                        FloorSession.Entities.Add(gD);
                        break;
                    case 4:
                        var pR = new PrinceRapier() { Pos = spawn };
                        FloorSession.Map[spawn.y, spawn.x] = pR.Icon;
                        FloorSession.Entities.Add(pR);
                        break;
                    case 5:
                        var rH = new RoyalHammer() { Pos = spawn };
                        FloorSession.Map[spawn.y, spawn.x] = rH.Icon;
                        FloorSession.Entities.Add(rH);
                        break;
                }
            }
        }
        protected override void GenerateEnemies()
        {
            for (int i = 0; i < GameSession.MAP_HEIGHT; i++)
            {
                var spawn = FreeCells[Random.Shared.Next(FreeCells.Count)];
                FreeCells.Remove(spawn);
                switch (Random.Shared.Next(0, 101))
                {
                    case <= 80:
                        var knight = new Knight(spawn.x, spawn.y);
                        FloorSession.Map[spawn.y, spawn.x] = knight.Icon;
                        FloorSession.Entities.Add(knight);
                        break;
                    case <= 100:
                        var rogue = new Rogue(spawn.x, spawn.y);
                        FloorSession.Map[spawn.y, spawn.x] = rogue.Icon;
                        FloorSession.Entities.Add(rogue);
                        break;
                    default:

                        break;
                }
            }
        }

        protected override void GeneratePattern()
        {
            switch (Random.Shared.Next(0, 101))
            {
                case <= 30:
                    CastleGenPatterns.SquareHalls();
                    break;
                case <= 50:
                    CastleGenPatterns.SquareHalls();
                    break;
                case <= 70:
                    CastleGenPatterns.ZigZag();
                    break;
                case <= 80:
                    CastleGenPatterns.ZigZag();
                    break;
                case <= 100:
                    
                    break;
                default:
                    CastleGenPatterns.SquareHalls();
                    break;
            }
        }
    }
}
