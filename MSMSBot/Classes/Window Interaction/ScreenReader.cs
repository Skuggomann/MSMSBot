using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MSMSBot.Classes.Window_Interaction
{
    class ScreenReader
    {
        private static ScreenCapture sc = new ScreenCapture();
        static ScreenReader()
        { 
            
        }

        public static void CaptureWindow(MainWindow W)
        {
            // Testfunction

            
            // capture entire screen
            //Image img = sc.CaptureScreen();
            Image img = sc.CaptureWindow(Handle());

            // display image in a Picture control named imageDisplay
            //W.imageDisplay.Source = GetImageStream(cropWindowFrame(img));
            W.ImageDisplay.Source = GetImageStream(CaptureWindow());

            // capture this window, and save it
            //sc.CaptureWindowToFile(Handle(), "C:\\HSScreen.bmp", ImageFormat.Bmp);
        }

        public static Image CaptureWindow()
        {
            return sc.CaptureWindow(Handle());
        }

        public static Image CaptureBoard()
        {
            System.Drawing.Image GameScreen = ScreenReader.CaptureWindow();

            int Top = 81;
            int Bottom = 40;
            int Left = 38;
            int Right = 37;

            Rectangle r = new Rectangle(Left, Top, GameScreen.Width - Left - Right, GameScreen.Height - Top - Bottom);

            return cropImage(GameScreen, r);
        }

        public static BitmapSource GetImageStream(Image myImage)
        {
            var bitmap = new Bitmap(myImage);
            IntPtr bmpPt = bitmap.GetHbitmap();
            BitmapSource bitmapSource =
             System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                   bmpPt,
                   IntPtr.Zero,
                   Int32Rect.Empty,
                   BitmapSizeOptions.FromEmptyOptions());

            //freeze bitmapSource and clear memory to avoid memory leaks
            bitmapSource.Freeze();
            DeleteObject(bmpPt);

            return bitmapSource;
        }
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr value);


        public static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        private static Image cropWindowFrame(Image img)
        {
            int BorderTop = 25;
            int Border = 3;

            Rectangle cropArea = new Rectangle(Border, BorderTop, img.Width - (2 * Border), img.Height - (Border + BorderTop));

            return cropImage(img, cropArea);
        }

        private static IntPtr Handle()
        {
            // Get HS handle:
            Process[] procs = Process.GetProcessesByName("Minesweeper");            
            return procs[0].MainWindowHandle;
        }
    }
}
