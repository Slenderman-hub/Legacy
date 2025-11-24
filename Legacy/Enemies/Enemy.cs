using static Legacy.GameSession;
using static Legacy.FloorSession;

namespace Legacy.Enemies
{
    public class Enemy
    {
        public int Stagger = 0;
        public (int x, int y) Pos;
        public char Icon = ' ';
        public string Name = string.Empty;
        public string Description = string.Empty;
        public decimal Health;
        public decimal Damage;
        public Enemy(int x, int y)
        {
            Pos = (x, y);
        }
        public virtual void Action(Actions action)
        {
            if (Stagger < 0)
            {
                Stagger++;
                return;
            }
            switch (action)
            {
                case Actions.Up:
                    if (Pos.y - 1 >= 1)
                    {
                        switch (Map[Pos.y - 1, Pos.x])
                        {
                            case ' ':
                                WriteNewPosition(' ', (Pos.x, Pos.y));
                                Pos.y -= 1;
                                WriteNewPosition(Icon, (Pos.x, Pos.y));
                                break;
                            case '|':
                                break;
                            case 'I':
                                Attack(GameSession.Hero);
                                break;
                            default:
                                
                                break;
                        }
                    }
                    break;
                case Actions.Down:
                    if (Pos.y + 1 < HEIGHT - 1)
                    {
                        switch (Map[Pos.y + 1, Pos.x])
                        {
                            case ' ':
                                WriteNewPosition(' ', (Pos.x, Pos.y));
                                Pos.y += 1;
                                WriteNewPosition(Icon, (Pos.x, Pos.y));
                                break;
                            case '|':
                                break;
                            case 'I':
                                Attack(GameSession.Hero);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case Actions.Left:
                    if (Pos.x - 1 >= 1)
                    {
                        switch (Map[Pos.y, Pos.x - 1])
                        {
                            case ' ':
                                WriteNewPosition(' ', (Pos.x, Pos.y));
                                Pos.x -= 1;
                                WriteNewPosition(Icon, (Pos.x, Pos.y));
                                break;
                            case '|':
                                break;
                            case 'I':
                                Attack(GameSession.Hero);
                                break;
                            default:
                                break;
                        }

                    }
                    break;
                case Actions.Right:
                    if (Pos.x + 1 < WIDTH)
                    {
                        switch (Map[Pos.y, Pos.x + 1])
                        {
                            case ' ':
                                WriteNewPosition(' ', (Pos.x, Pos.y));
                                Pos.x += 1;
                                WriteNewPosition(Icon, (Pos.x, Pos.y));
                                break;
                            case '|':
                                break;
                            case 'I':
                                Attack(GameSession.Hero);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        protected virtual void Attack(Hero hero)
        {
            hero.Health -= Damage;
            hero.Stagger += 1;
            if (hero.Health <= 0)
            {
               WriteNewPosition('X', (hero.Pos.x, hero.Pos.y),ConsoleColor.Red);
            }
        }
    }
    
}
