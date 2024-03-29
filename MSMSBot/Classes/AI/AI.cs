﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSMSBot.Classes.Window_Interaction;

using aima.search.csp;
using System.Diagnostics;
namespace MSMSBot.Classes.AI
{
    // The first atempt, we did not get it to work
    class AI
    {
        CSP drasl;
        Square[,] m_board;
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
                        case Square.One:
                        case Square.Two:
                        case Square.Three:
                        case Square.Four:
                        case Square.Five:
                        case Square.Six:
                        case Square.Seven:
                        case Square.Eight:
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
            m_board[row, col] = Square.Bomb;
            drasl = new MineSweeperCSP().getMap(m_board);
            Assignment result = drasl.backTrackingSearch();
            if(result != null)
                Debug.WriteLine(result.ToString());
            m_board[row, col] = Square.Unknown;
            if (result == null)
                return true;
            return false;
        }

        public void makeMove(int row, int col)
        {
            Debug.WriteLine("I want to click point"+row+col);
            //ScreenGateway.ClickSquere(row, col);
        }

        public void nextMove()
        {
            getMoves();
        }
        public bool isUnknown(int row, int col)
        {
            if (row < 0 || row >= 9 || col < 0 || col >= 9)
                return false;

            if (m_board[row, col] == Square.Unknown)
                return true;
            return false;
        }
    }
}
