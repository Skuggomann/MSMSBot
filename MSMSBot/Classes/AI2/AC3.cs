using MSMSBot.Classes.Window_Interaction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMSBot.Classes.AI2
{
    class AC3
    {
        public struct Variable
        {
            public Square Value;
            public List<Square> Domain;
            public Point index;

            public Variable(Square square, List<Square> Domain, Point i)
            {
                // TODO: Complete member initialization
                this.Value = square;
                this.Domain = Domain;
                this.index = i;
            }            
        }

        //private enum Square : int { Empty = 0, One, Two, Three, Four, Five, Six, Seven, Eight, Unknown, Bomb};

        List<Square> DomainBoth = new List<Square>();
        List<Square> DomainBomb = new List<Square>();
        List<Square> DomainEmpty = new List<Square>();

        public AC3()
        {
            DomainBoth.Add(Square.Bomb);
            DomainBoth.Add(Square.Empty);
               
            DomainBomb.Add(Square.Bomb);

            DomainEmpty.Add(Square.Empty);
        }

        

        public bool dostuff()
        {
            Square[,] Board = ScreenGateway.GetBoardLayout();
            Variable[,] Assigned = new Variable[ScreenGateway.H, ScreenGateway.W];
            
            for (int h = 0; h < ScreenGateway.H; h++)
            {
                for (int w = 0; w < ScreenGateway.W; w++)
                {

                    if (Board[h,w] == Square.Unknown)
                    {
                        Assigned[h, w] = new Variable(Square.Unknown, DomainBoth, new Point(h,w));
                    }
                    else if (Board[h, w] == Square.Bomb)
                    {
                        Assigned[h, w] = new Variable(Square.Bomb, DomainBomb, new Point(h, w));
                    }
                    else
                    {
                        Assigned[h, w] = new Variable(Board[h, w], DomainEmpty, new Point(h, w));
                    }    
                }
            }


            Debug.WriteLine(BoardToString(Assigned));

            // Add a bomb here to test if that spot does not contain a bomb (if CSP returns null)

            var answer = CSPBACKTRACKING(Assigned);
            if (answer != null)
            {
                Debug.WriteLine(BoardToString(answer)); 
            }
            else
            {
                Debug.WriteLine("No answere found."); 
            }
            



            return false;
        }

        public Variable[,] CSPBACKTRACKING(Variable[,] A)
        {
            //If assignment A is complete then return A
            if (assignmentCompliet(A))
            {
                return A;
            }

            //2. Run AC3 and update var-domains accordingly
            //3. If a variable has an empty domain then return failure


            //4. X  select a variable not in A
            Variable X = new Variable(Square.Empty,DomainEmpty, new Point(-1,-1));
            foreach (var S in A)
            {
                if (S.Value == Square.Unknown)
                {
                    X = S;
                    Debug.WriteLine("4. S.Value: " + S.Value + " in position: " + S.index.ToString()); 
                    break;
                }
            }

            //5. D  select an ordering on the domain of X
            //6. For each value v in D do
            foreach (Square v in X.Domain)
            {
                //a. Add (Xv) to A
                A[X.index.X, X.index.Y].Value = v;

                Debug.WriteLine("6.a. X.Value: " + A[X.index.X, X.index.Y].Value + " in position: " + A[X.index.X, X.index.Y].index.ToString()); 
                //b. var-domains  forwardChecking(var-domains, X, v, A)
                Debug.WriteLine("Starting Forward Chekking");
                A = forwardChecking(X, v, A);
                Debug.WriteLine("Forward Chekking Ended");

                //c. If no variable has an empty domain then
                if (noDomainsEmpty(A))
                {
                    Debug.WriteLine("c. No domains found empty.");

                    //(i) result  CSP-BACKTRACKING(A, var-domains)
                    Debug.WriteLine("c. Starting recursion.");
                    Variable[,] result = CSPBACKTRACKING(A);
                    //(ii) If result  failure then return result
                    if (result != null)
                    {
                        return result;
                    }
                    //return A; //TODO: remove
                }
                else
                {
                    Debug.WriteLine("c.Empty domain found.");
                }
                
                //d. Remove (Xv) from A
                A[X.index.X, X.index.Y].Value = Square.Unknown;
                Debug.WriteLine("6.d. X.Value reset");
            }

            
            //7. Return failure
            return null;
            //return A;
        }

        private Variable[,] forwardChecking(Variable X, Square v, Variable[,] A)
        {
            /*
            //For each variable Y not in A do:
            foreach (var S in A)
            {
                if (S.Value == Square.Unknown)
                {
                    //For every constraint C relating Y to the variables in A do:
                    //Remove all values from Y’s domain that do not satisfy C
                }
            }*/


            

            for (int i = -1; i < 1; i++)
            {
                for (int j = -1; j < 1; j++)
                {
                    try
                    {
                        A = Constraint1(X.index.X + i, X.index.Y + j, A);
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }
                }
            }
            
            // Do constraint2 (total bombs == bombs on board)


            return A;


        }

        // This is the constraint that removes bomb from empty spaces that violate the constrait of being close to a number that alreddy has that ammount of bombs
        private Variable[,] Constraint1(int x, int y, Variable[,] A)
        {
            int number = (int)A[x, y].Value;
            if (number > 0 && number < 9)
            {
                if (BombsAround(x, y, A) == number)
                {
                    for (int i = -1; i < 1; i++)
                    {
                        for (int j = -1; j < 1; j++)
                        {
                            if (A[x + i, y + j].Value == Square.Unknown)
                            {
                                try
                                {
                                    A[x + i, y + j].Domain = DomainEmpty;
                                }
                                catch (IndexOutOfRangeException e)
                                {

                                }
                            }
                        }
                    }
                }
            }
            return A;
        }

        // Counts how many bombs are around 
        private int BombsAround(int row, int col, Variable[,] A)
        {
            int bombs = 0;

            for (int i = -1; i < 1; i++)
            {
                for (int j = -1; j < 1; j++)
                {
                    try
                    {
                        if (A[row + i, col + j].Value == Square.Bomb)
                        {
                            ++bombs;
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {

                    }
                }
            }
            return bombs;
        }




        private void selectVariableNotInA(Variable[,] Board, ref Variable X)
        {
            foreach (var S in Board)
            {
                if (S.Value == Square.Unknown)
                {
                    X = S;
                    break;
                }
            }
            //throw new NullReferenceException();
            //return new Point(-1,-1);
        }

        private Boolean assignmentCompliet(Variable[,] Board)
        {
            int CountUnknown = 0;
            foreach (var S in Board)
	        {
                if (S.Value == Square.Unknown)
                {
                    ++CountUnknown;
                }
	        }
            return CountUnknown == 0;
        }
        private Boolean noDomainsEmpty(Variable[,] Board)
        {
            foreach (var S in Board)
            {
                if (S.Domain.ToArray().Length == 0)
                {
                    return false;
                }
            }
            return true;
        }
        // Converts a Variable array into a string.
        public static String BoardToString(Variable[,] arr)
        {
            string s = "";

            for (int i = 0; i < ScreenGateway.H; i++)
            {
                for (int j = 0; j < ScreenGateway.W; j++)
                {
                    s += "[" + (int)arr[i, j].Value + "] ";
                }
                s += "\n";
            }

            return s;
        }
    }
}
