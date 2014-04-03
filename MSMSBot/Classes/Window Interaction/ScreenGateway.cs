using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MSMSBot.Classes.Window_Interaction
{
    // Enum for better keeping track of what number stands for what squere.
    public enum Square : int { Empty = 0, One, Two, Three, Four, Five, Six, Seven, Eight, Unknown, Bomb };


    // This class is the interface used to interact with the minesweeper program.
    static class ScreenGateway
    {
        private static int BlockHeight = 18;
        private static int HalfBlock = BlockHeight / 2;

        // Create filter, this is required becouse AForge cannot do searches on
        private static AForge.Imaging.Filters.Grayscale GrayscaleFilter = new AForge.Imaging.Filters.GrayscaleBT709();

        // Bitmaps of each image used in the image recongition
        private static Bitmap Clicked;
        private static Bitmap UnClicked;
        private static Bitmap Flag;

        private static Bitmap Nr1, Nr2, Nr3, Nr4, Nr5, Nr6, Nr7, Nr8;

        private static List<Point> BombLocations = new List<Point>();

        // height and with of the board (this is cells as in 9x9 for easy)
        public static int H;
        public static int W;

        private static System.Drawing.Bitmap GameBoard;
        private static ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0.9f); // The image recognition function

        /*
         * //Depricated:
        public static int Height
        {
            set
            {
                H = value;
            }
        }
        public static int Width
        {
            set
            {
                W = value;
            }
        }
        */

        

        static ScreenGateway()
        { 
            // Init all Bitmaps
            Clicked = GrayscaleFilter.Apply(Properties.Resources.Clicked);
            UnClicked = GrayscaleFilter.Apply(Properties.Resources.UnClicked);
            Flag = GrayscaleFilter.Apply(Properties.Resources.Flag);
            Nr1 = GrayscaleFilter.Apply(Properties.Resources.Nr1);
            Nr2 = GrayscaleFilter.Apply(Properties.Resources.Nr2);
            Nr3 = GrayscaleFilter.Apply(Properties.Resources.Nr3);
            Nr4 = GrayscaleFilter.Apply(Properties.Resources.Nr4);
            Nr5 = GrayscaleFilter.Apply(Properties.Resources.Nr5);
            Nr6 = GrayscaleFilter.Apply(Properties.Resources.Nr6);
            Nr7 = GrayscaleFilter.Apply(Properties.Resources.Nr7);
            Nr8 = GrayscaleFilter.Apply(Properties.Resources.Nr8);

            // Get rows and colums:
            GameBoard = GrayscaleFilter.Apply(new Bitmap(ScreenReader.CaptureBoard()));

            H = (GameBoard.Height + 1) / BlockHeight;
            W = (GameBoard.Width + 1) / BlockHeight;

            Debug.WriteLine("[" + H + " x " + W + "]", "Board dimensions");
        }

        // This function returnes a double array of integers representing the state of minesweeper
        public static Square[,] GetBoardLayout()
        {
            // Capture board and apply filter
            GameBoard = GrayscaleFilter.Apply(new Bitmap(ScreenReader.CaptureBoard()));

            Square[,] board = new Square[H, W];  


            // This part goes through every cell and searches if it contains an image, if none are found it is classifyed as unknown(unclicked squere)
            for (int h = 0; h < H; h++)
            {
                for (int w = 0; w < W; w++)
                {
                    // Seaches each box for a match
                    if (ImageSearch(Clicked, w, h) == 1)
                    {
                        board[h, w] = Square.Empty;
                    }
                    else if (ImageSearch(Nr1, w, h) == 1)
                    {
                        board[h, w] = Square.One;
                    }
                    else if (ImageSearch(Nr2, w, h) == 1)
                    {
                        board[h, w] = Square.Two;
                    }
                    else if (ImageSearch(Nr3, w, h) == 1)
                    {
                        board[h, w] = Square.Three;
                    }
                    else if (ImageSearch(Nr4, w, h) == 1)
                    {
                        board[h, w] = Square.Four;
                    }
                    else if (ImageSearch(Nr5, w, h) == 1)
                    {
                        board[h, w] = Square.Five;
                    }
                    else if (ImageSearch(Nr6, w, h) == 1)
                    {
                        board[h, w] = Square.Six;
                    }
                    else if (ImageSearch(Nr7, w, h) == 1)
                    {
                        board[h, w] = Square.Seven;
                    }
                    else if (ImageSearch(Nr8, w, h) == 1)
                    {
                        board[h, w] = Square.Eight;
                    }
                    else
                    {
                        board[h, w] = Square.Unknown;
                        //throw new Exception("No matches found in squere: (" + x + ", " + y + ")");
                    }
                }
            }
            //Debug.WriteLine("Nr of things found: " + found, "TestImageRecognition()");


            // Bomb locations are remembered and added after the board is set.
            foreach (Point p in BombLocations)
            {
                board[p.X, p.Y] = Square.Bomb; 
            }

            return board;
        }


        // Helper function
        static int ImageSearch(Bitmap Template, int X, int Y)
        {
            TemplateMatch[] matchings = tm.ProcessImage(GameBoard, Template, GetRectangle(X, Y));

            if (matchings.Length > 1)
            {
                return 1;
                //throw new Exception("More than one match found in the same squere!"); //TODO: er þetta ekki bara alt í lagi því þetta er sama myndin
            }

            return matchings.Length;
        }



        // Returns a rectangle object for a spesific cordinate
        static Rectangle GetRectangle(Point Cord)
        {
            return GetRectangle(Cord.X, Cord.Y);
        }
        static Rectangle GetRectangle(int X, int Y)
        {
            //TODO: chekka ef það þarf að bæta við + X, til að rétta 1pixel offsett.
            return new Rectangle(BlockHeight * X, BlockHeight * Y, BlockHeight, BlockHeight);
        }


        // Clicks a squere on the minesweeper map
        public static void ClickSquere(Point Cord, bool LeftClick = true)
        {
            ClickSquere(Cord.X, Cord.Y, LeftClick);
        }
        public static void ClickSquere(int Row, int Column, bool LeftClick = true)
        {
            Rectangle temp = GetRectangle(Column, Row);
            temp.X += 30 + HalfBlock;
            temp.Y += 30 + HalfBlock;

            ScreenClicker.ClickOnPoint(ScreenReader.Handle(), new Point(temp.X, temp.Y), LeftClick);
        }


        // Adds the cordinates of a squere to the list containgin all cordinates of bombs
        public static void MarkSquereAsBomb(Point bomb)
        {
            BombLocations.Add(bomb);
        }
        public static void MarkSquereAsBomb(int Row, int Column)
        {
            MarkSquereAsBomb(new Point(Row, Column));
        }


        // Changes the pixel format of a Bitmap file.
        private static Bitmap Reformat(Bitmap Original)
        {
            return Original.Clone(new System.Drawing.Rectangle(0, 0, Original.Width, Original.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }


        // Converts a Square array into a string. (for debugging purpouses)
        public static String BoardToString(Square[,] arr)
        {
            string s = "";
            
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    s += "[" + (int)arr[i,j] + "] ";
                }
                s += "\n";
            }

            return s;
        }


    }
}
