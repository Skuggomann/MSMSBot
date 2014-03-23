using aima.search.csp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSMSBot.Classes.Window_Interaction;
namespace MSMSBot.Classes.AI
{
    class MineSweeperConstraints : Constraint
    {
        ScreenGateway.Square[,] m_board;
        public MineSweeperConstraints(ScreenGateway.Square[,] board)
        {
            m_board = board;
        }

        public bool isSatisfiedWith(Assignment assignment, string variable, object value)
        {
            Assignment a2 = assignment.copy();
            a2.setAssignment(variable, value);
            /*
            for (int col = 0; col < 9; col++)
            {
                for (int row = 0; row < 9; row++)
                {
                    string p = getPoint(row, col);

                    switch (m_board[row,col])
                    {
                        case ScreenGateway.Square.Empty:
                            if (Bomb(p, a2))
                                return false;
                            if (BombsAround(row, col, a2) != 0)
                                return false;
                            break;
                        case ScreenGateway.Square.One:
                            if (Bomb(p, a2))
                                return false;
                            if (BombsAround(row, col, a2) != 1)
                                return false;
                            break;
                        case ScreenGateway.Square.Two:
                            if (Bomb(p, a2))
                                return false;
                            if (BombsAround(row, col, a2) != 2)
                                return false;
                            break;
                        case ScreenGateway.Square.Three:
                            if (Bomb(p, a2))
                                return false;
                            if (BombsAround(row, col, a2) != 3)
                                return false;
                            break;
                        case ScreenGateway.Square.Four:
                            if (Bomb(p, a2))
                                return false;
                            if (BombsAround(row, col, a2) != 4)
                                return false;
                            break;
                        case ScreenGateway.Square.Five:
                            if (Bomb(p, a2))
                                return false;
                            if (BombsAround(row, col, a2) != 5)
                                return false;
                            break;
                        case ScreenGateway.Square.Six:
                            if (Bomb(p, a2))
                                return false;
                            if (BombsAround(row, col, a2) != 6)
                                return false;
                            break;
                        case ScreenGateway.Square.Seven:
                            if (Bomb(p, a2))
                                return false;
                            if (BombsAround(row, col, a2) != 7)
                                return false;
                            break;
                        case ScreenGateway.Square.Eight:
                            if (Bomb(p, a2))
                                return false;
                            if (BombsAround(row, col, a2) != 8)
                                return false;
                            break;
                        case ScreenGateway.Square.Unknown:
                            break;
                        case ScreenGateway.Square.Bomb:
                            if (!Bomb(p, a2))
                                return false;
                            break;
                        default:
                            break;
                    }
                }
            }


            foreach (string p in a2.getVariables())
            {
                switch (getSquare(p))
                {
                    case ScreenGateway.Square.Empty:
                        if (Bomb(p, a2))
                            return false;
                        if (BombsAround(p, a2) != 0)
                            return false;
                        break;
                    case ScreenGateway.Square.One:
                        if (Bomb(p, a2))
                            return false;
                        if (BombsAround(p, a2) <= 1)
                            return false;
                        break;
                    case ScreenGateway.Square.Two:
                        if (Bomb(p, a2))
                            return false;
                        if (BombsAround(p, a2) <= 2)
                            return false;
                        break;
                    case ScreenGateway.Square.Three:
                        if (Bomb(p, a2))
                            return false;
                        if (BombsAround(p, a2) <= 3)
                            return false;
                        break;
                    case ScreenGateway.Square.Four:
                        if (Bomb(p, a2))
                            return false;
                        if (BombsAround(p, a2) <= 4)
                            return false;
                        break;
                    case ScreenGateway.Square.Five:
                        if (Bomb(p, a2))
                            return false;
                        if (BombsAround(p, a2) <= 5)
                            return false;
                        break;
                    case ScreenGateway.Square.Six:
                        if (Bomb(p, a2))
                            return false;
                        if (BombsAround(p, a2) <= 6)
                            return false;
                        break;
                    case ScreenGateway.Square.Seven:
                        if (Bomb(p, a2))
                            return false;
                        if (BombsAround(p, a2) <= 7)
                            return false;
                        break;
                    case ScreenGateway.Square.Eight:
                        if (Bomb(p, a2))
                            return false;
                        if (BombsAround(p, a2) <= 8)
                            return false;
                        break;
                    case ScreenGateway.Square.Unknown:
                        break;
                    case ScreenGateway.Square.Bomb:
                        if (!Bomb(p, a2))
                            return false;
                        break;
                    default:
                        break;
                }
            }*/
            string p = variable;
            switch (getSquare(p))
            {
                case ScreenGateway.Square.Empty:
                    if (Bomb(p, a2))
                        return false;
                    if (BombsAround(p, a2) != 0)
                        return false;
                    break;
                case ScreenGateway.Square.One:
                    if (Bomb(p, a2))
                        return false;
                    if (BombsAround(p, a2) <= 1)
                        return false;
                    break;
                case ScreenGateway.Square.Two:
                    if (Bomb(p, a2))
                        return false;
                    if (BombsAround(p, a2) <= 2)
                        return false;
                    break;
                case ScreenGateway.Square.Three:
                    if (Bomb(p, a2))
                        return false;
                    if (BombsAround(p, a2) <= 3)
                        return false;
                    break;
                case ScreenGateway.Square.Four:
                    if (Bomb(p, a2))
                        return false;
                    if (BombsAround(p, a2) <= 4)
                        return false;
                    break;
                case ScreenGateway.Square.Five:
                    if (Bomb(p, a2))
                        return false;
                    if (BombsAround(p, a2) <= 5)
                        return false;
                    break;
                case ScreenGateway.Square.Six:
                    if (Bomb(p, a2))
                        return false;
                    if (BombsAround(p, a2) <= 6)
                        return false;
                    break;
                case ScreenGateway.Square.Seven:
                    if (Bomb(p, a2))
                        return false;
                    if (BombsAround(p, a2) <= 7)
                        return false;
                    break;
                case ScreenGateway.Square.Eight:
                    if (Bomb(p, a2))
                        return false;
                    if (BombsAround(p, a2) <= 8)
                        return false;
                    break;
                case ScreenGateway.Square.Unknown:
                    break;
                case ScreenGateway.Square.Bomb:
                    if (!Bomb(p, a2))
                        return false;
                    break;
                default:
                    break;
            }

            return true;
        }
        private bool Bomb(string p, Assignment a2)
        {
            if (a2.hasAssignmentFor(p))
                if (int.Parse(a2.getAssignment(p).ToString()) == 1)
                    return true;
            return false;
        }

        private int BombsAround(int row, int col, Assignment a2)
        {
            int bombs = 0;
            string currentPoint;
            //NW
            currentPoint = getPoint(row-1, col-1);
            if (Bomb(currentPoint, a2))
                bombs++;
            //N
            currentPoint = getPoint(row - 1, col);
            if (Bomb(currentPoint, a2))
                bombs++;
            //NE
            currentPoint = getPoint(row - 1, col + 1);
            if (Bomb(currentPoint, a2))
                bombs++;
            //E
            currentPoint = getPoint(row, col + 1);
            if (Bomb(currentPoint, a2))
                bombs++;
            //SE
            currentPoint = getPoint(row + 1, col + 1);
            if (Bomb(currentPoint, a2))
                bombs++;
            //S
            currentPoint = getPoint(row + 1, col);
            if (Bomb(currentPoint, a2))
                bombs++;
            //SW
            currentPoint = getPoint(row + 1, col - 1);
            if (Bomb(currentPoint, a2))
                bombs++;
            //W
            currentPoint = getPoint(row, col - 1);
            if (Bomb(currentPoint, a2))
                bombs++;

            return bombs;
        }

        private int BombsAround(string point, Assignment a2)
        {
            int row = int.Parse(point.Substring(5, 1));
            int col = int.Parse(point.Substring(6, 1));
            int bombs = 0;
            string currentPoint;
            //NW
            currentPoint = getPoint(row - 1, col - 1);
            if (Bomb(currentPoint, a2))
                bombs++;
            //N
            currentPoint = getPoint(row - 1, col);
            if (Bomb(currentPoint, a2))
                bombs++;
            //NE
            currentPoint = getPoint(row - 1, col + 1);
            if (Bomb(currentPoint, a2))
                bombs++;
            //E
            currentPoint = getPoint(row, col + 1);
            if (Bomb(currentPoint, a2))
                bombs++;
            //SE
            currentPoint = getPoint(row + 1, col + 1);
            if (Bomb(currentPoint, a2))
                bombs++;
            //S
            currentPoint = getPoint(row + 1, col);
            if (Bomb(currentPoint, a2))
                bombs++;
            //SW
            currentPoint = getPoint(row + 1, col - 1);
            if (Bomb(currentPoint, a2))
                bombs++;
            //W
            currentPoint = getPoint(row, col - 1);
            if (Bomb(currentPoint, a2))
                bombs++;

            return bombs;
        }

        private string getPoint(int row, int col)
        {
            return "point" + row + "" + col;
        }

        private ScreenGateway.Square getSquare(string point)
        {
            int row = int.Parse(point.Substring(5,1));
            int col = int.Parse(point.Substring(6,1));
            return m_board[row, col];
        }

    }
}
