using System;
using Common;

namespace MouseTracker
{
    public class MouseActionEventArgs : EventArgs
    {
        public MouseMessages mouseMessage;
        public readonly POINT point;
        public readonly IntPtr hWnd;
        public string name
        {
            get
            {
                switch (mouseMessage)
                {
                    case MouseMessages.WM_MOUSEMOVE:
                        return "Move";

                    case MouseMessages.WM_LBUTTONDOWN:
                        return  "Left Button Down";

                    case MouseMessages.WM_RBUTTONDOWN:
                        return  "Right Button Down";

                    case MouseMessages.WM_LBUTTONUP:
                        return "Left Button Up";

                    case MouseMessages.WM_RBUTTONUP:
                        return "Right Button Up";
                }
                return string.Empty;
            }
        }

        public int x => point.x;
        public int y => point.y;

        public MouseActionEventArgs(MouseMessages mouseMessage, POINT point, IntPtr hWnd)
        {
            this.mouseMessage = mouseMessage;
            this.point = point;
            this.hWnd = hWnd;
        }
    }
    public class MacroEventArgs
    {
        public string name;
        public readonly POINT point;
        public readonly IntPtr hWnd;

        public int x => point.x;
        public int y => point.y;
        public MacroEventArgs(MouseActionEventArgs e)
        {
            this.name = e.name;
            this.point = e.point;
            this.hWnd = e.hWnd;
        }
    }
}
