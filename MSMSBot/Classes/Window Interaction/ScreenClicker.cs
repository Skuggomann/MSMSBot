using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSMSBot.Classes.Window_Interaction
{
    // This class does all the screen clikcing for us.
    // Constructed with the help of http://stackoverflow.com/questions/10355286/programmatically-mouse-click-in-another-window but i alos expanded on it.

    class ScreenClicker
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [Flags]
        private enum MouseEventFlags : uint
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010,
            WHEEL = 0x00000800,
            XDOWN = 0x00000080,
            XUP = 0x00000100
        }

        static ScreenClicker()
        {

        }


        public static void ClickOnPoint(IntPtr WindowHandle, Point ClientPoint)
        {
            ClickOnPoint(WindowHandle, ClientPoint, true);
        }

        
        public static void ClickOnPoint(IntPtr WindowHandle, Point ClientPoint, bool LeftClick)
        {
            var oldPos = Cursor.Position;

            // get screen coordinates
            ClientToScreen(WindowHandle, ref ClientPoint);

            // move the cursor to the desired cordinates
            Cursor.Position = new Point(ClientPoint.X, ClientPoint.Y);

            // Set focus to the window (to bring it into the forground and to accept clicks)
            SetForegroundWindow(WindowHandle);
            System.Threading.Thread.Sleep(5); // 5ms delay between sending the focus command and sending the mouse click
             
            // Make a left or a right lick
            if (LeftClick)
            {
                mouse_event((int)MouseEventFlags.LEFTDOWN, 0, 0, 0, UIntPtr.Zero); // left mouse button down
                mouse_event((int)MouseEventFlags.LEFTUP, 0, 0, 0, UIntPtr.Zero); // left mouse button up 
            }
            else
            {
                mouse_event((int)MouseEventFlags.RIGHTDOWN, 0, 0, 0, UIntPtr.Zero);
                mouse_event((int)MouseEventFlags.RIGHTUP, 0, 0, 0, UIntPtr.Zero);
            }            
             
            // return mouse to old position
            System.Threading.Thread.Sleep(50);
            Cursor.Position = oldPos;
        }


    }
}
