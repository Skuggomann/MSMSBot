using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMSBot.Classes.Window_Interaction
{
    static class ScreenGateway
    {
        private static int BlockHeight = 18;


        private static Bitmap Clicked;
        private static Bitmap UnClicked;
        private static Bitmap Flag;

        private static Bitmap Nr1;
        private static Bitmap Nr2;
        private static Bitmap Nr3;


        public static int H;
        public static int W;


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

        public enum Squere : int { Empty = 0, One, Two, Three, Four, Five, Six, Seven, Eight, Bomb};


        static ScreenGateway()
        { 
            // Init all Bitmaps
            Clicked = Properties.Resources.Clicked;
            UnClicked = Properties.Resources.UnClicked;
            Flag = Properties.Resources.Flag;
            Nr1 = Properties.Resources.Nr1;
            Nr2 = Properties.Resources.Nr2;
            Nr3 = Properties.Resources.Nr3;
        }

        static Char[][] GetBoardLayout()
        {
            
            


            return null;
        }











        // Returns a rectangle object for a spesific cordinate
        static void GetRectangle(Point Cord)
        {
            //TODO: this

        }


        // Clicks a squere on the minesweeper map
        static void ClickSquere(bool RightClick, Point Cord)
        {
           //TODO: this

        }

        // Changes the pixel format of a Bitmap file.
        private static Bitmap Reformat(Bitmap Original)
        {
            return Original.Clone(new System.Drawing.Rectangle(0, 0, Original.Width, Original.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }
    }
}
