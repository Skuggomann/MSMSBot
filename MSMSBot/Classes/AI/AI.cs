using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSMSBot.Classes.Window_Interaction;

using aima.search.csp;
namespace MSMSBot.Classes.AI
{
    class AI
    {
        CSP drasl;
        ScreenGateway.Square[,] m_board;
        public AI()
        {
            m_board = ScreenGateway.GetBoardLayout();
            drasl = MineSweeperCSP.getMap(m_board);
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
                            if(m_board[row - 1, col - 1] == ScreenGateway.Square.Unknown);
                            //---Check if legal.
                            if (legalMove(row - 1, col - 1))
                                makeMove(row - 1, col - 1);
                                return;
                            //N
                            if (m_board[row - 1, col] == ScreenGateway.Square.Unknown);
                            //---Check if legal. 
                            if (legalMove(row - 1, col))
                                makeMove(row - 1, col);
                                return;          
                            //NE
                            if (m_board[row - 1, col + 1] == ScreenGateway.Square.Unknown) ;
                            //---Check if legal.
                            if (legalMove(row - 1, col + 1))
                                makeMove(row - 1, col + 1);
                                return;  
                            //E
                            if (m_board[row, col + 1] == ScreenGateway.Square.Unknown) ;
                            //---Check if legal.  
                            if (legalMove(row, col + 1))
                                makeMove(row, col + 1);
                                return;
                            //SE
                            if (m_board[row + 1, col + 1] == ScreenGateway.Square.Unknown) ;
                            //---Check if legal.  
                            if (legalMove(row + 1, col + 1))
                                makeMove(row + 1, col + 1);
                                return;
                            //S
                            if (m_board[row + 1, col] == ScreenGateway.Square.Unknown) ;
                            //---Check if legal.  
                            if (legalMove(row + 1, col))
                                makeMove(row + 1, col);
                                return;
                            //SW
                            if (m_board[row + 1, col - 1] == ScreenGateway.Square.Unknown) ;
                            //---Check if legal.  
                            if (legalMove(row + 1, col - 1))
                                makeMove(row + 1, col - 1);
                                return;
                            //W
                            if (m_board[row, col - 1] == ScreenGateway.Square.Unknown) ;
                            //---Check if legal. 
                            if (legalMove(row, col - 1))
                                makeMove(row, col - 1);
                                return; 
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public bool legalMove(int row, int col)
        {
            m_board[row, col] = ScreenGateway.Square.Bomb;
            drasl = MineSweeperCSP.getMap(m_board);
            Assignment result = drasl.backTrackingSearch();
            m_board[row, col] = ScreenGateway.Square.Unknown;
            if (result == null)
                return true;
            return false;
        }
        public void makeMove(int row, int col)
        {

        }

        public void nextMove();
        {
            
        }
    }
}
