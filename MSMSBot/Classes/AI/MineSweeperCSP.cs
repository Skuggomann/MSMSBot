using aima.search.csp;
using MSMSBot.Classes.Window_Interaction;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MSMSBot.Classes.AI
{
    class MineSweeperCSP : CSP
    {
        private MineSweeperCSP instance = null;

        public MineSweeperCSP(ArrayList variables, Constraint constraints, Domain domains)
            : base(variables, constraints, domains)
		{
			//super ;
		}

        public MineSweeperCSP()
        {
            //wat
        }

        public CSP getMap(ScreenGateway.Square[,] board)
        {

            ArrayList variables = new ArrayList();
            for (int col = 0; col < 9; col++)
            {
                for (int row = 0; row < 9; row++)
                {

                    if ((nextToNumber(row, col, board) && isUnknown(row,col,board)) || isBomb(row,col,board))
                        variables.Add("point"+ row + "" + col);
                }
            }


            Domain domains = new Domain(variables);
            for (int i = 0; i < variables.Count; i++)
            {
                string variable = variables[i].ToString();
                domains.addToDomain(variable, "0");
                domains.addToDomain(variable, "1");

                Debug.WriteLine(variable);
            }



            Constraint mineSweeperConstraints = new MineSweeperConstraints(board);

            instance = new MineSweeperCSP(variables, mineSweeperConstraints, domains);
            return instance;
        }

        private bool nextToNumber(int row, int col, ScreenGateway.Square[,] board)
        {
            //NW
            if (isNumber(row - 1, col - 1, board))
                return true;
            //N
            if (isNumber(row - 1, col, board))
                return true;
            //NE
            if (isNumber(row - 1, col + 1, board))
                return true;
            //E
            if (isNumber(row, col + 1, board))
                return true;
            //SE
            if (isNumber(row + 1, col + 1, board))
                return true;
            //S
            if (isNumber(row + 1, col, board))
                return true;
            //SW
            if (isNumber(row + 1, col - 1, board))
                return true;
            //W
            if (isNumber(row, col - 1, board))
                return true;

            return false;
        }
        private bool isNumber(int row, int col, ScreenGateway.Square[,] board)
        {
            if (row < 0 || row >= 9 || col < 0 || col >= 9)
                return false;

            if( board[row,col] == ScreenGateway.Square.One      ||
                board[row,col] == ScreenGateway.Square.Two      ||
                board[row,col] == ScreenGateway.Square.Three    ||
                board[row,col] == ScreenGateway.Square.Four     ||
                board[row,col] == ScreenGateway.Square.Five     ||
                board[row,col] == ScreenGateway.Square.Six      ||
                board[row,col] == ScreenGateway.Square.Seven    ||
                board[row,col] == ScreenGateway.Square.Eight)
                return true;
            return false;
        }
        private bool isUnknown(int row, int col, ScreenGateway.Square[,] board)
        {
            if (row < 0 || row >= 9 || col < 0 || col >= 9)
                return false;

            if (board[row, col] == ScreenGateway.Square.Unknown)
                return true;
            return false;
        }

        private bool isBomb(int row, int col, ScreenGateway.Square[,] board)
        {
            if (row < 0 || row >= 9 || col < 0 || col >= 9)
                return false;

            if (board[row, col] == ScreenGateway.Square.Bomb)
                return true;
            return false;
        }
    }
}
