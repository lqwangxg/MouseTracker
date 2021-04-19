/*
 * FileName: Form1.cs
 * Author: Dianyang Wu
 * Implementation Date: 10/21/2009
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;

using Common;
using System.Collections;

namespace MouseTracker
{
    public delegate void MouseActionEventHandler(object sender, MouseActionEventArgs e);

    public partial class Form1 : Form
    {
        private IntPtr mouseHookID = IntPtr.Zero;
        private IntPtr keyboardHookID = IntPtr.Zero;
        public int index;
        Tool tool;
        public Dictionary<IntPtr, Window> windows;
        int refTimeMs;
        public bool isNewRow;

        DataGridView rightClickedGrid;
        int rightClickedRowIndex;
        int rightClickedColumnIndex;

        private const int WM_KEYDOWN = 0x0100;
        public const int WH_KEYBOARD_LL = 13;
        public const int WH_MOUSE_LL = 14;
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);
        
        delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        HookProc MouseHookProc;
        HookProc KeyboardHookProc;

        public event MouseActionEventHandler MouseAction;

        protected virtual void OnMouseAction(MouseActionEventArgs e)
        {
            if (MouseAction != null)
            {
                // Invokes the delegates. 
                MouseAction(this, e);
            }
        }

        public Form1()
        {
            InitializeComponent();
            tool = new Tool();
            
            windows = new Dictionary<IntPtr, Window>();
            CleanData_Click(null, null);
            dataGridView2.Rows.Add(15);
            ShowDataGridView2.Available = false;
            this.Action.Items.Clear();
            this.Action.Items.AddRange(Enum.GetNames(typeof(MouseMessages))); 
            MouseHookProc = new HookProc(Mouse_HookCallback);
            KeyboardHookProc = new HookProc(KeyBoard_HookCallback);
            SetHook(WH_KEYBOARD_LL);

            MouseActionProcess mouseActionProcess = new MouseActionProcess();
            // set local mouse event handler
            MouseAction += new MouseActionEventHandler(mouseActionProcess.MouseAction);
        }

        /// <summary>
        ///  Set global low-level keyboard and mouse event hook
        /// </summary>
        /// <param name="idHook"></param>
        public void SetHook(int idHook)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                switch (idHook)
                {
                    case WH_MOUSE_LL:
                        if (mouseHookID != IntPtr.Zero) User32.UnhookWindowsHookEx(mouseHookID);
                        // Call GC.Coolect() to avoid system calling it in hook
                        GC.Collect();
                        // Mouse event hook
                        mouseHookID = SetWindowsHookEx(idHook, MouseHookProc, Kernel32.GetModuleHandle(curModule.ModuleName), 0);
                        break;

                    case WH_KEYBOARD_LL:
                        if (keyboardHookID != IntPtr.Zero) User32.UnhookWindowsHookEx(keyboardHookID);
                        // // Call GC.Coolect() to avoid system calling it in hook
                        GC.Collect();
                        // Keyboard event hook
                        keyboardHookID = SetWindowsHookEx(idHook, KeyboardHookProc, Kernel32.GetModuleHandle(curModule.ModuleName), 0);
                        break;
                }
            }
        }

        /// <summary>
        /// The list keeps suspended windows
        /// </summary>
        List<IntPtr> suspendHWnds = new List<IntPtr>();
        /// <summary>
        /// Keyboard event hook callback
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private IntPtr KeyBoard_HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                switch ((Keys)vkCode)
                {
                    case Keys.F1:  
                    // Suspend the window under the cursor                         
                        if (!StartTracking.Enabled) break;

                        POINT point;
                        point.x = System.Windows.Forms.Cursor.Position.X;
                        point.y = System.Windows.Forms.Cursor.Position.Y;
                        IntPtr hWnd = new Tool().getOwner(point);
                        if (hWnd == this.Handle) return new IntPtr(1);

                        uint thrdId = User32.GetWindowThreadProcessId(hWnd, IntPtr.Zero);
                        IntPtr hThrd = Kernel32.OpenThread(Kernel32.ThreadAccess.SUSPEND_RESUME, true, thrdId);
                        
                        //  Ensure a window to be  suspended only once. 
                        if (!suspendHWnds.Contains(hWnd)) Kernel32.SuspendThread(hThrd);
                        bool b = Kernel32.CloseHandle(hThrd);
                        suspendHWnds.Add(hWnd);

                	// Exits the global low level keyboard hook,
                	// calls GC.Collect() actively, and then reenters a new hook
                        SetHook(WH_KEYBOARD_LL);
                        return new IntPtr(1);

                    case Keys.F2: 
                    // Resume all resumed windows                            
                        ResumeWindows();

			        // Exits the global low level keyboard hook,
                	// calls GC.Collect() actively, and then reenters a new hook
                        SetHook(WH_KEYBOARD_LL);
                        return new IntPtr(1);
                }
            }
            return User32.CallNextHookEx(keyboardHookID, nCode, wParam, lParam);
        }

        /// <summary>
        ///  Mouse event hook callback
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private IntPtr Mouse_HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                if (nCode < 0) return User32.CallNextHookEx(mouseHookID, nCode, wParam, lParam);

                POINT point = ((MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT))).pt;

                MouseMessages mouseMessage = 0;
                switch ((MouseMessages)wParam)
                {
                    case MouseMessages.WM_MOUSEMOVE:
                    case MouseMessages.WM_LBUTTONDOWN:
                    case MouseMessages.WM_RBUTTONDOWN:
                    case MouseMessages.WM_LBUTTONUP:
                    case MouseMessages.WM_RBUTTONUP:
                        mouseMessage = (MouseMessages)wParam;
                        break;

                    default:
                        // Do not process other events.
                        return User32.CallNextHookEx(mouseHookID, nCode, wParam, lParam);
                }

                IntPtr hWnd = IntPtr.Zero;
                if (mouseMessage != MouseMessages.WM_MOUSEMOVE)
                { // over form1 
                    IntPtr hOwner = tool.getRootOwner(point, out hWnd);
                    if (hOwner == Handle)
                    {
                        // The event happend on Form1, treat it as a mouse move event.
                        mouseMessage = MouseMessages.WM_MOUSEMOVE;
                        // Allow buttons on Form1 to receive clicking immediately.
                        this.Activate();
                    }
                }

                int nowMs = DateTime.Now.Millisecond;
                if (mouseMessage == MouseMessages.WM_MOUSEMOVE &&
                    nowMs - refTimeMs < 200 && nowMs > refTimeMs)
                {
                    // Filter out the mouse move events which happend in 200 ms.
                    return User32.CallNextHookEx(mouseHookID, nCode, (IntPtr) mouseMessage, lParam);
                }

                // Prepare parementers and raise a local mouse event 
                MouseActionEventArgs e = new MouseActionEventArgs(mouseMessage, point, hWnd);
                OnMouseAction(e);

                // Change reference time
                refTimeMs = DateTime.Now.Millisecond;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error in private IntPtr Mouse_HookCallback(int nCode, IntPtr wParam, IntPtr lParam): " + e.Message);
            }
            return User32.CallNextHookEx(mouseHookID, nCode, wParam, lParam);
        }

        /// <summary>
        /// Resume all the suspended windows
        /// </summary>
        private void ResumeWindows()
        {
            foreach (IntPtr hWnd in suspendHWnds)
            {
                uint thrdId = User32.GetWindowThreadProcessId((IntPtr)hWnd, IntPtr.Zero);
                IntPtr hThrd = Kernel32.OpenThread(Kernel32.ThreadAccess.SUSPEND_RESUME, true, thrdId);
                while (Kernel32.ResumeThread(hThrd) > 0);
                bool b = Kernel32.CloseHandle(hThrd);
            }
            suspendHWnds.Clear();
        }

        /// <summary>
        /// Do Dispose Job Before Form Closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { User32.UnhookWindowsHookEx(mouseHookID); }
            catch { }
            try { User32.UnhookWindowsHookEx(keyboardHookID); }
            catch { }

            ResumeWindows();
        }

        /// <summary>
        /// Start Event Tracking.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartTracking_Click(object sender, EventArgs e)
        {
            if (index >= dataGridView1.Rows.Count)
            {
                MessageBox.Show("Click the CleanData button to clean the data area first.");
                return;
            }

            ResumeWindows();
            if (keyboardHookID != IntPtr.Zero) User32.UnhookWindowsHookEx(keyboardHookID);
            keyboardHookID = IntPtr.Zero;

            SetHook(WH_MOUSE_LL);

            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();

            StartTracking.Enabled = false;
            StopTracking.Enabled = true;
            Simulate.Enabled = false;

            isNewRow = true;
            refTimeMs = DateTime.Now.Millisecond;
        }

        /// <summary>
        /// Stop Event Tracking.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void StopTracking_Click(object sender, EventArgs e)
        {
            if (mouseHookID != IntPtr.Zero) User32.UnhookWindowsHookEx(mouseHookID);
            mouseHookID = IntPtr.Zero;

            SetHook(WH_KEYBOARD_LL);

            StartTracking.Enabled = true;
            StopTracking.Enabled = false;
            Simulate.Enabled = true;

            if (MouseActionProcess.lastWin != null)
            {
                ControlPaint.DrawReversibleFrame(((Window)MouseActionProcess.lastWin).frame,
                    Color.Black, FrameStyle.Thick);
                MouseActionProcess.lastWin = null;
            }
        }

        /// <summary>
        /// Clear All Data Tracked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CleanData_Click(object sender, EventArgs e)
        {
            StopTracking_Click(null, null);

            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(20);
            windows.Clear();
            index = 0;

            dataGridView2.Rows.Clear();
        }
        /// <summary>
        /// Insert a new Event Data Row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertRow_Click(object sender, EventArgs e)
        {
            if (mouseHookID != IntPtr.Zero) SetHook(WH_MOUSE_LL);

            if (StopTracking.Enabled) return;

            dataGridView1.Rows.Insert(dataGridView1.CurrentRow.Index, 1);
            if (dataGridView1.CurrentRow.Index <= index + 1) index++;

        }

        /// <summary>
        /// Delete the selected Event data rows.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveRows_Click(object sender, EventArgs e)
        {
            if (mouseHookID != IntPtr.Zero) SetHook(WH_MOUSE_LL);

            if (StopTracking.Enabled) return;

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                try
                {
                    int i = row.Index;
                    dataGridView1.Rows.RemoveAt(row.Index);
                    if (i < index) index--;
                }
                catch { }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (mouseHookID != IntPtr.Zero) SetHook(WH_MOUSE_LL);
            if (e.ColumnIndex != 3) return;

            try
            {
                string str = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                showWindowProperties((IntPtr)long.Parse(str));
            }
            catch { }
        }

        /// <summary>
        /// Show window information in dataGridView2
        /// </summary>
        /// <param name="hWnd"></param>
        public void showWindowProperties(IntPtr hWnd)
        {
            dataGridView2.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dataGridView2.Rows.Clear();
            dataGridView2.Rows.Add(10);

            Window win = windows[hWnd];
            if (win == null) return;

            int n = 0;
            if (win.extendedStyles.Length > 0) n++;
            if (win.hMenu != IntPtr.Zero) n++;
            if (win.itemType != null) n++;
            if (win.itemStrings != null) n += win.itemStrings.Length;
            if (win.itemStyles != null) n += win.itemStyles.Length;
            if (n > 0) dataGridView2.Rows.Add(n);

            int i = 0;
            dataGridView2.Rows[i].Cells[0].Value = "HWnd";
            dataGridView2.Rows[i++].Cells[1].Value = ((long)hWnd).ToString("D8") + " / " + ((long)hWnd).ToString("X8");

            dataGridView2.Rows[i].Cells[0].Value = "Class Name";
            dataGridView2.Rows[i++].Cells[1].Value = win.className;

            dataGridView2.Rows[i].Cells[0].Value = "Text ";
            dataGridView2.Rows[i++].Cells[1].Value = win.text;

            dataGridView2.Rows[i].Cells[0].Value = "Location";
            dataGridView2.Rows[i++].Cells[1].Value = string.Format("{0}, {1}, {2}, {3}", win.rect.X, win.rect.Y, win.rect.Right, win.rect.Bottom);

            dataGridView2.Rows[i].Cells[0].Value = "Size";
            dataGridView2.Rows[i++].Cells[1].Value = string.Format("{0} x {1}", win.rect.Width, win.rect.Height);

            dataGridView2.Rows[i].Cells[0].Value = "Window Style";
            dataGridView2.Rows[i++].Cells[1].Value = win.styles;

            if (win.extendedStyles.Length > 0)
            {
                dataGridView2.Rows[i].Cells[0].Value = "Extended Window Style";
                dataGridView2.Rows[i++].Cells[1].Value = win.extendedStyles;
            }

            dataGridView2.Rows[i].Cells[1].Style.ForeColor =
            dataGridView2.Rows[i + 1].Cells[1].Style.ForeColor =
            dataGridView2.Rows[i + 2].Cells[1].Style.ForeColor =
            dataGridView2.Rows[i + 3].Cells[1].Style.ForeColor =
            dataGridView2.Rows[i + 4].Cells[1].Style.ForeColor = Color.Blue;

            if (win.owner != IntPtr.Zero) dataGridView2.Rows[i].Cells[1].Value = ((long)win.owner).ToString("D8") + " / " + ((long)win.owner).ToString("X8");
            dataGridView2.Rows[i++].Cells[0].Value = "Owner HWnd";

            if (win.parent != IntPtr.Zero) dataGridView2.Rows[i].Cells[1].Value = ((long)win.parent).ToString("D8") + " / " + ((long)win.parent).ToString("X8");
            dataGridView2.Rows[i++].Cells[0].Value = "Parent HWnd";

            if (win.child != IntPtr.Zero) dataGridView2.Rows[i].Cells[1].Value = ((long)win.child).ToString("D8") + " / " + ((long)win.child).ToString("X8");
            dataGridView2.Rows[i++].Cells[0].Value = "Child HWnd";

            if (win.previous != IntPtr.Zero) dataGridView2.Rows[i].Cells[1].Value = ((long)win.previous).ToString("D8") + " / " + ((long)win.previous).ToString("X8");
            dataGridView2.Rows[i++].Cells[0].Value = "Previous HWnd";

            if (win.next != IntPtr.Zero) dataGridView2.Rows[i].Cells[1].Value = ((long)win.next).ToString("D8") + " / " + ((long)win.next).ToString("X8");
            dataGridView2.Rows[i++].Cells[0].Value = "Next HWnd";

            if (win.hMenu != IntPtr.Zero)
            {
                dataGridView2.Rows[i].Cells[0].Value = "HMenu";
                dataGridView2.Rows[i++].Cells[1].Value = ((long)win.hMenu).ToString("D8") + " / " + ((long)win.hMenu).ToString("X8"); ;
            }

            if (win.itemType != null)
            {
                dataGridView2.Rows[i].Cells[0].Value = "Type of Items";
                dataGridView2.Rows[i++].Cells[1].Value = win.itemType;
            }

            if (win.itemStrings != null)
            {
                for (int j = 0; j < win.itemStrings.Length; j++)
                {
                    dataGridView2.Rows[i].Cells[0].Value = "Item String " + j.ToString();
                    dataGridView2.Rows[i++].Cells[1].Value = win.itemStrings[j];

                    if (win.itemStyles == null) continue;
                    dataGridView2.Rows[i].Cells[0].Value = "Item Style " + j.ToString();
                    dataGridView2.Rows[i++].Cells[1].Value = win.itemStyles[j];
                }
            }

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                row.Cells[0].ToolTipText = row.Cells[0].Value.ToString();
                try 
                {
                    if (row.Cells[1].Value != null) row.Cells[1].ToolTipText = row.Cells[1].Value.ToString();
                }
                catch { }
            }
        }

        private void HideDataGridView2_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = true;
            HideDataGridView2.Available = false;
            ShowDataGridView2.Available = true;

            Size size = this.Size;
            size.Width -= dataGridView2.Size.Width;
            this.Size = size;

            if (mouseHookID != IntPtr.Zero) SetHook(WH_MOUSE_LL);
        }

        private void ShowDataGridView2_Click(object sender, EventArgs e)
        {
            Size size = this.Size;
            size.Width += dataGridView2.Size.Width;
            this.Size = size;

            splitContainer1.Panel2Collapsed = false;
            HideDataGridView2.Available = true;
            ShowDataGridView2.Available = false;

            if (mouseHookID != IntPtr.Zero) SetHook(WH_MOUSE_LL);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (mouseHookID != IntPtr.Zero) SetHook(WH_MOUSE_LL);

            if (e.ColumnIndex != 1) return;
            if (e.RowIndex == -1) return;

            if (dataGridView2.Rows[e.RowIndex].Cells[1].Value == null) return;
            switch (dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString())
            {
                case "Owner HWnd":
                case "Parent HWnd":
                case "Child HWnd":
                case "Previous HWnd":
                case "Next HWnd":
                    try
                    {
                        string str = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        IntPtr hWnd = (IntPtr)long.Parse(str.Remove(str.IndexOf(" ")));
                        if (!windows.ContainsKey(hWnd))
                        {
                            POINT p;
                            p.x = p.y = 0;
                            Window win = tool.getWindow(hWnd, p);
                            if (win != null) windows.Add(hWnd, win);
                        }
                        showWindowProperties(hWnd);
                    }
                    catch { }
                    break;
            }
        }

        /// <summary>
        /// Simulate mouse actions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Simulate_Click(object sender, EventArgs e)
        {
            ICollection rows; // = (dataGridView1.SelectedRows.Count > 0) ? dataGridView1.SelectedRows : dataGridView1.Rows;
            if(dataGridView1.SelectedRows.Count > 0)
            {
                rows = dataGridView1.SelectedRows;
            }
            else
            {
                rows = dataGridView1.Rows;
            }

            //POINT point;
            //point.x = point.y = 0;
            IntPtr hWnd = IntPtr.Zero;
            foreach (DataGridViewRow row in rows)
            {
                Thread.Sleep(40);
                try
                {
                    MacroEventArgs me = (MacroEventArgs)row.DataBoundItem;
                    
                    int x = int.Parse(row.Cells[1].Value.ToString());
                    int y = int.Parse(row.Cells[2].Value.ToString());
                    //point.x = x;
                    //point.y = y;
                    hWnd = windows[(IntPtr)int.Parse(row.Cells[3].Value.ToString())].owner;

                    //Enum.GetName(typeof(MouseMessages), MouseMessages.WM_MOUSEMOVE)
                    MouseMessages mm;
                    Enum.TryParse<MouseMessages>(row.Cells[0].Value.ToString(), out mm);
                    MouseEventFlags flag = MouseEventFlags.MOVE;
                    switch (mm)
                    {
                        case MouseMessages.WM_MOUSEMOVE:
                            User32.SetCursorPos(x, y);
                            continue;

                        case MouseMessages.WM_LBUTTONDOWN:
                            flag = MouseEventFlags.LEFTDOWN;
                            break;

                        case MouseMessages.WM_RBUTTONDOWN:
                            flag = MouseEventFlags.RIGHTDOWN;
                            break;

                        case MouseMessages.WM_LBUTTONUP:
                            flag = MouseEventFlags.LEFTUP;
                            break;

                        case MouseMessages.WM_RBUTTONUP:
                            flag = MouseEventFlags.RIGHTUP;
                            break;
                    }

                    // Move the cursor 
                    User32.SetCursorPos(x, y);
                    // Click a mouse button
                    User32.mouse_event(flag, 0, 0, 0, 0);

                }
                catch
                {
                    continue;
                }
            }
        }

        private void dataGridView2_RowHeightChanged(object sender, DataGridViewRowEventArgs e)
        {
            dataGridView2.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            if (mouseHookID != IntPtr.Zero) SetHook(WH_MOUSE_LL);
        }

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Author: Dianyang Wu\r\n\r\nDate: 10/08/2009", "About");
            if (mouseHookID != IntPtr.Zero) SetHook(WH_MOUSE_LL);
        }

        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (mouseHookID != IntPtr.Zero) SetHook(WH_MOUSE_LL);

            if (e.Button != MouseButtons.Right) return;

            rightClickedGrid = (DataGridView)sender;
            rightClickedRowIndex = e.RowIndex;
            rightClickedColumnIndex = e.ColumnIndex;

            copyToolStripMenuItem.Enabled = true;
            copySelectedRowsToolStripMenuItem.Enabled = true;
            copyAllRowsToolStripMenuItem.Enabled = true;

            if (rightClickedRowIndex == -1 && rightClickedColumnIndex == -1)
            {
                copyToolStripMenuItem.Enabled = false;
                copySelectedRowsToolStripMenuItem.Enabled = false;
            }
            else if (rightClickedColumnIndex == -1)
            {
                copyToolStripMenuItem.Enabled = false;
            }
            else if (rightClickedRowIndex == -1)
            {
                copySelectedRowsToolStripMenuItem.Enabled = false;
                copyAllRowsToolStripMenuItem.Enabled = false;
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseHookID != IntPtr.Zero) SetHook(WH_MOUSE_LL);

            StringBuilder sb = new StringBuilder();
            string result; 
            switch (((ToolStripMenuItem)sender).Text)
            {
                case "Copy":
                    if (rightClickedRowIndex != -1)
                    {
                        try {
                            Clipboard.SetText(rightClickedGrid.Rows[rightClickedRowIndex].Cells[rightClickedColumnIndex].Value.ToString());
                        } catch {
                            Clipboard.SetText(" ");
                        }
                        break;
                    } 

                    // copy a column
                    foreach (DataGridViewRow row in rightClickedGrid.Rows)
                    {
                            DataGridViewCell cell = row.Cells[rightClickedColumnIndex];
                            if (cell.Value != null) sb.Append(cell.Value.ToString());
                            if (cell.ColumnIndex < rightClickedGrid.ColumnCount - 1) sb.Append("\t");
                    }

                    result = Regex.Replace(sb.ToString(), @"\t+$", "");
                    if (result.Length > 0) Clipboard.SetText(result);

                    break;

                case "Copy selected row(s)":
                    foreach (DataGridViewRow row in rightClickedGrid.Rows)
                    {
                        string str = "\r\n";
                        bool selected = false;
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value != null) str += cell.Value.ToString();
                            if (cell.ColumnIndex < rightClickedGrid.ColumnCount - 1) str += "\t";
                            if (cell.Selected) selected = true;
                        }
                        if (selected) sb.Append(str);
                    }

                    if (sb.Length > 0) Clipboard.SetText(sb.Remove(0, 2).ToString());
                    break;

                case "Copy all rows":
                    foreach (DataGridViewRow row in rightClickedGrid.Rows)
                    {
                        sb.AppendLine();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value != null) sb.Append(cell.Value.ToString());
                            if (cell.ColumnIndex < rightClickedGrid.ColumnCount - 1) sb.Append("\t");
                        }
                    }

                    result = Regex.Replace(sb.ToString(), @"(\r\n\t+)+$", "");
                    if (result.Length > 0) Clipboard.SetText(result.Remove(0, 2));
                    break;
            }
        }

        private void dataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (mouseHookID != IntPtr.Zero) SetHook(WH_MOUSE_LL);

            if (e.Button != MouseButtons.Right) return;
            contextMenuStrip1.Show((Control)sender, new Point(e.X, e.Y));
        }

    }


}