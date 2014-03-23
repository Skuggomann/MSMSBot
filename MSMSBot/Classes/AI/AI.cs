using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSMSBot.Classes.Window_Interaction;

using aima.search.csp;
using System.Diagnostics;
namespace MSMSBot.Classes.AI
{
    class AI
    {
        CSP drasl;
        ScreenGateway.Square[,] m_board;
        public AI()
        {
            m_board = ScreenGateway.GetBoardLayout();
            drasl = new MineSweeperCSP();
        }

        public Assignment Backtrack()
        {
            return drasl.backTrackingSearch();
        }

        public void getMoves()
        {
            for (int col = 0; col < 9; col++)
            {
                for (int row = 0; row < 9; row++)
                {
                    switch (m_board[row,col])
                    {
                        case ScreenGateway.Square.One:
                        case ScreenGateway.Square.Two:
                        case ScreenGateway.Square.Three:
                        case ScreenGateway.Square.Four:
                        case ScreenGateway.Square.Five:
                        case ScreenGateway.Square.Six:
                        case ScreenGateway.Square.Seven:
                        case ScreenGateway.Square.Eight:
                            //NW
                            if (isUnknown(row - 1, col - 1))
                            {
                                //---Check if legal.
                                if (legalMove(row - 1, col - 1))
                                    makeMove(row - 1, col - 1);
                                return;
                            }
                            //N
                            if (isUnknown(row - 1, col))
                            {
                                //---Check if legal. 
                                if (legalMove(row - 1, col))
                                    makeMove(row - 1, col);
                                return;
                            }
                            //NE
                            if (isUnknown(row - 1, col + 1))
                            {
                                //---Check if legal.
                                if (legalMove(row - 1, col + 1))
                                    makeMove(row - 1, col + 1);
                                return;
                            }
                            //E
                            if (isUnknown(row, col + 1))
                            {
                                //---Check if legal.  
                                if (legalMove(row, col + 1))
                                    makeMove(row, col + 1);
                                return;
                            }
                            //SE
                            if (isUnknown(row + 1, col + 1))
                            {
                                //---Check if legal.  
                                if (legalMove(row + 1, col + 1))
                                    makeMove(row + 1, col + 1);
                                return;
                            }
                            //S
                            if (isUnknown(row + 1, col))
                            {
                                //---Check if legal.  
                                if (legalMove(row + 1, col))
                                    makeMove(row + 1, col);
                                return;
                            }
                            //SW
                            if (isUnknown(row + 1, col - 1))
                            {
                                //---Check if legal.  
                                if (legalMove(row + 1, col - 1))
                                    makeMove(row + 1, col - 1);
                                return;
                            }
                            //W
                            if (isUnknown(row, col - 1))
                            {
                                //---Check if legal. 
                                if (legalMove(row, col - 1))
                                    makeMove(row, col - 1);
                                return;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public bool legalMove(int row, int col)
        {
            if (row < 0 || row >= 9 || col < 0 || col >= 9)
                return false;
            m_board[row, col] = ScreenGateway.Square.Bomb;
            drasl = new MineSweeperCSP().getMap(m_board);
            Assignment result = drasl.backTrackingSearch();
            if(result != null)
                Debug.WriteLine(result.ToString());
            m_board[row, col] = ScreenGateway.Square.Unknown;
            if (result == null)
                return true;
            return false;
        }

        public void makeMove(int row, int col)
        {
            ScreenGateway.ClickSquere(row, col);
        }

        public void nextMove()
        {
            getMoves();
        }
        public bool isUnknown(int row, int col)
        {
            if (row < 0 || row >= 9 || col < 0 || col >= 9)
                return false;

            if (m_board[row, col] == ScreenGateway.Square.Unknown)
                return true;
            return false;
        }
    }
}
