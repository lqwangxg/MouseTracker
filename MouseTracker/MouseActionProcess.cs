using Common;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MouseTracker
{

    public class MouseActionProcess
    {
        public static Window lastWin;

        // Local mouse action event handler
        public void MouseAction(object sender, MouseActionEventArgs e)
        {
            Form1 form = (Form1)sender;
            DataGridViewRowCollection rows = form.dataGridView1.Rows;

            rows[form.index].Cells[0].Value = Enum.GetName(typeof(MouseMessages), e.mouseMessage);
            rows[form.index].Cells[1].Value = e.point.x;
            rows[form.index].Cells[2].Value = e.point.y;
            
            switch (e.mouseMessage)
            {
                case MouseMessages.WM_MOUSEMOVE:
                    if (form.isNewRow)
                    {
                        //rows[form.index].Cells[0].Value = "Move";
                        form.isNewRow = false;
                    }

                    // Exits the global low level mouse hook, calls GC.Collect() actively, and then reenters a new hook            
                    form.SetHook(Form1.WH_MOUSE_LL);
                    return;

                /*case MouseMessages.WM_LBUTTONDOWN:
                    rows[form.index].Cells[0].Value = "Left Button Down";
                    break;

                case MouseMessages.WM_RBUTTONDOWN:
                    rows[form.index].Cells[0].Value = "Right Button Down";
                    break;

                case MouseMessages.WM_LBUTTONUP:
                    rows[form.index].Cells[0].Value = "Left Button Up";
                    break;

                case MouseMessages.WM_RBUTTONUP:
                    rows[form.index].Cells[0].Value = "Right Button Up";
                    break;*/
            }

            Window win = new Tool().getWindow(e.hWnd, e.point);
            if (win != null)
            {
                //high light window's frame
                switch (e.mouseMessage)
                {
                    case MouseMessages.WM_LBUTTONDOWN:
                    case MouseMessages.WM_RBUTTONDOWN:
                        win.DrawFrame();
                        lastWin = win;
                        break;

                    case MouseMessages.WM_LBUTTONUP:
                    case MouseMessages.WM_RBUTTONUP:
                        lastWin.DrawFrame();

                        if (lastWin.hWnd != win.hWnd)
                        {
                            //win.DrawFrame();
                            Thread thd = new Thread(Window.DrawFrame);
                            thd.Name = "delay";
                            thd.Start(win);
                        }
                        lastWin = null;

                        break;
                }

                // Exits the global low level mouse hook, calls GC.Collect() actively, and then reenters a new hook
                form.SetHook(Form1.WH_MOUSE_LL);

                if (!form.windows.ContainsKey(win.hWnd)) form.windows.Add(win.hWnd, win);
                rows[form.index].Cells[3].Value = win.hWnd.ToString();

                form.dataGridView1.ClearSelection();
                rows[form.index++].Cells[3].Selected = true;
                form.isNewRow = true;
                if (form.index >= form.dataGridView1.Rows.Count) form.StopTracking_Click(null, null);

                if (!form.splitContainer1.Panel2Collapsed) form.showWindowProperties(win.hWnd);
            }
        }

        public void MouseAction2(object sender, MouseActionEventArgs e)
        {
            Form1 form = (Form1)sender;
            DataGridViewRowCollection rows = form.dataGridView1.Rows;

            rows[form.index].Cells[1].Value = e.point.x;
            rows[form.index].Cells[2].Value = e.point.y;

            switch (e.mouseMessage)
            {
                case MouseMessages.WM_MOUSEMOVE:
                    if (form.isNewRow)
                    {
                        rows[form.index].Cells[0].Value = "Move";
                        form.isNewRow = false;
                    }

                    // Exits the global low level mouse hook, calls GC.Collect() actively, and then reenters a new hook            
                    form.SetHook(Form1.WH_MOUSE_LL);
                    return;

                case MouseMessages.WM_LBUTTONDOWN:
                    rows[form.index].Cells[0].Value = "Left Button Down";
                    break;

                case MouseMessages.WM_RBUTTONDOWN:
                    rows[form.index].Cells[0].Value = "Right Button Down";
                    break;

                case MouseMessages.WM_LBUTTONUP:
                    rows[form.index].Cells[0].Value = "Left Button Up";
                    break;

                case MouseMessages.WM_RBUTTONUP:
                    rows[form.index].Cells[0].Value = "Right Button Up";
                    break;
            }

            Window win = new Tool().getWindow(e.hWnd, e.point);
            if (win != null)
            {
                //high light window's frame
                switch (e.mouseMessage)
                {
                    case MouseMessages.WM_LBUTTONDOWN:
                    case MouseMessages.WM_RBUTTONDOWN:
                        win.DrawFrame();
                        lastWin = win;
                        break;

                    case MouseMessages.WM_LBUTTONUP:
                    case MouseMessages.WM_RBUTTONUP:
                        lastWin.DrawFrame();

                        if (lastWin.hWnd != win.hWnd)
                        {
                            //win.DrawFrame();
                            Thread thd = new Thread(Window.DrawFrame);
                            thd.Name = "delay";
                            thd.Start(win);
                        }
                        lastWin = null;

                        break;
                }

                // Exits the global low level mouse hook, calls GC.Collect() actively, and then reenters a new hook
                form.SetHook(Form1.WH_MOUSE_LL);

                if (!form.windows.ContainsKey(win.hWnd)) form.windows.Add(win.hWnd, win);
                rows[form.index].Cells[3].Value = win.hWnd.ToString();

                form.dataGridView1.ClearSelection();
                rows[form.index++].Cells[3].Selected = true;
                form.isNewRow = true;
                if (form.index >= form.dataGridView1.Rows.Count) form.StopTracking_Click(null, null);

                if (!form.splitContainer1.Panel2Collapsed) form.showWindowProperties(win.hWnd);
            }
        }
    }
}
