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
        private static Bitmap Clicked;
        private static Bitmap UnClicked;
        private static Bitmap Flag;

        private static Bitmap Nr1;
        private static Bitmap Nr2;
        private static Bitmap Nr3;
        private static Bitmap Nr4;


        static ScreenGateway()
        { 
            // Init all Bitmaps
            Clicked = Properties.Resources.TestImage;
            UnClicked = Properties.Resources.TestImage;




        }

        static void GetBoardLayout()
        {
            //private static System.Drawing.Image img = ScreenReader.CaptureWindow();
        
        }






        // Changes the pixel format of a Bitmap file.
        private static Bitmap Reformat(Bitmap Original)
        {
            return Original.Clone(new System.Drawing.Rectangle(0, 0, Original.Width, Original.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }
    }
}
