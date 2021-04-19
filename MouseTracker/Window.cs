using System;
using Common;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MouseTracker
{
    public class Window
    {
        public IntPtr hWnd;
        public string className;
        public string text;
        public User32.RECT rect;
        public string styles;
        public string extendedStyles;
        public string itemType;
        public IntPtr hMenu;
        public string[] itemStrings;
        public string[] itemStyles;
        public IntPtr parent, child, owner, next, previous;

        public Rectangle frame => new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);

        public static void DrawFrame(object win)
        {
            if (win is Window)
            {
                ((Window)win).DrawFrame();
            }
        }

        public void DrawFrame()
        {
            if (Thread.CurrentThread.Name != null) Thread.Sleep(200);

            ControlPaint.DrawReversibleFrame(this.frame, Color.Red, FrameStyle.Thick);
        }
    }
}