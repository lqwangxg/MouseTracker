namespace MouseTracker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.StartTracking = new System.Windows.Forms.ToolStripButton();
            this.StopTracking = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CleanData = new System.Windows.Forms.ToolStripButton();
            this.InsertRow = new System.Windows.Forms.ToolStripButton();
            this.RemoveRows = new System.Windows.Forms.ToolStripButton();
            this.ShowDataGridView2 = new System.Windows.Forms.ToolStripButton();
            this.HideDataGridView2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Simulate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.About = new System.Windows.Forms.ToolStripButton();
            this.mouseTrackerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectedRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAllRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Action = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HWnd = new System.Windows.Forms.DataGridViewLinkColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mouseTrackerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartTracking,
            this.StopTracking,
            this.toolStripSeparator1,
            this.CleanData,
            this.InsertRow,
            this.RemoveRows,
            this.ShowDataGridView2,
            this.HideDataGridView2,
            this.toolStripSeparator2,
            this.Simulate,
            this.toolStripSeparator3,
            this.About});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(804, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // StartTracking
            // 
            this.StartTracking.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StartTracking.Image = ((System.Drawing.Image)(resources.GetObject("StartTracking.Image")));
            this.StartTracking.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartTracking.Name = "StartTracking";
            this.StartTracking.Size = new System.Drawing.Size(23, 22);
            this.StartTracking.Text = "Start Tracking";
            this.StartTracking.ToolTipText = "Start tracking";
            this.StartTracking.Click += new System.EventHandler(this.StartTracking_Click);
            // 
            // StopTracking
            // 
            this.StopTracking.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopTracking.Image = ((System.Drawing.Image)(resources.GetObject("StopTracking.Image")));
            this.StopTracking.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopTracking.Name = "StopTracking";
            this.StopTracking.Size = new System.Drawing.Size(23, 22);
            this.StopTracking.Text = "Stop Tracking";
            this.StopTracking.ToolTipText = "Stop tracking";
            this.StopTracking.Click += new System.EventHandler(this.StopTracking_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // CleanData
            // 
            this.CleanData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CleanData.Image = ((System.Drawing.Image)(resources.GetObject("CleanData.Image")));
            this.CleanData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CleanData.Name = "CleanData";
            this.CleanData.Size = new System.Drawing.Size(23, 22);
            this.CleanData.Text = "Clean Data";
            this.CleanData.ToolTipText = "Clean data";
            this.CleanData.Click += new System.EventHandler(this.CleanData_Click);
            // 
            // InsertRow
            // 
            this.InsertRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.InsertRow.Image = ((System.Drawing.Image)(resources.GetObject("InsertRow.Image")));
            this.InsertRow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InsertRow.Name = "InsertRow";
            this.InsertRow.Size = new System.Drawing.Size(23, 22);
            this.InsertRow.Text = "Insert Row";
            this.InsertRow.ToolTipText = "Insert a new row";
            this.InsertRow.Click += new System.EventHandler(this.InsertRow_Click);
            // 
            // RemoveRows
            // 
            this.RemoveRows.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RemoveRows.Image = ((System.Drawing.Image)(resources.GetObject("RemoveRows.Image")));
            this.RemoveRows.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveRows.Name = "RemoveRows";
            this.RemoveRows.Size = new System.Drawing.Size(23, 22);
            this.RemoveRows.Text = "Remove Rows";
            this.RemoveRows.ToolTipText = "Remove selected rows";
            this.RemoveRows.Click += new System.EventHandler(this.RemoveRows_Click);
            // 
            // ShowDataGridView2
            // 
            this.ShowDataGridView2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ShowDataGridView2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ShowDataGridView2.Image = ((System.Drawing.Image)(resources.GetObject("ShowDataGridView2.Image")));
            this.ShowDataGridView2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ShowDataGridView2.Name = "ShowDataGridView2";
            this.ShowDataGridView2.Size = new System.Drawing.Size(23, 22);
            this.ShowDataGridView2.Text = "Show Window Properties";
            this.ShowDataGridView2.ToolTipText = "Show window properties";
            this.ShowDataGridView2.Click += new System.EventHandler(this.ShowDataGridView2_Click);
            // 
            // HideDataGridView2
            // 
            this.HideDataGridView2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.HideDataGridView2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.HideDataGridView2.Image = ((System.Drawing.Image)(resources.GetObject("HideDataGridView2.Image")));
            this.HideDataGridView2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.HideDataGridView2.Name = "HideDataGridView2";
            this.HideDataGridView2.Size = new System.Drawing.Size(23, 22);
            this.HideDataGridView2.Text = "Hide Window Properties";
            this.HideDataGridView2.ToolTipText = "Hide window properties";
            this.HideDataGridView2.Click += new System.EventHandler(this.HideDataGridView2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // Simulate
            // 
            this.Simulate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Simulate.Image = ((System.Drawing.Image)(resources.GetObject("Simulate.Image")));
            this.Simulate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Simulate.Name = "Simulate";
            this.Simulate.Size = new System.Drawing.Size(23, 22);
            this.Simulate.Text = "Simulate mouse action";
            this.Simulate.Click += new System.EventHandler(this.Simulate_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // About
            // 
            this.About.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.About.Image = ((System.Drawing.Image)(resources.GetObject("About.Image")));
            this.About.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.About.Name = "About";
            this.About.Size = new System.Drawing.Size(23, 22);
            this.About.Text = "About";
            this.About.Click += new System.EventHandler(this.About_Click);
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.985075F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Action,
            this.X,
            this.Y,
            this.HWnd});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.985075F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.985075F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersWidth = 24;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(317, 357);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseClick);
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView2);
            this.splitContainer1.Size = new System.Drawing.Size(804, 357);
            this.splitContainer1.SplitterDistance = 317;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 5;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Key,
            this.Value});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(484, 357);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            this.dataGridView2.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseClick);
            this.dataGridView2.RowHeightChanged += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView2_RowHeightChanged);
            this.dataGridView2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseClick);
            // 
            // Key
            // 
            this.Key.HeaderText = "Name";
            this.Key.Name = "Key";
            this.Key.ReadOnly = true;
            this.Key.Width = 120;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.Width = 360;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.copySelectedRowsToolStripMenuItem,
            this.copyAllRowsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(184, 70);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // copySelectedRowsToolStripMenuItem
            // 
            this.copySelectedRowsToolStripMenuItem.Name = "copySelectedRowsToolStripMenuItem";
            this.copySelectedRowsToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.copySelectedRowsToolStripMenuItem.Text = "Copy selected row(s)";
            this.copySelectedRowsToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // copyAllRowsToolStripMenuItem
            // 
            this.copyAllRowsToolStripMenuItem.Name = "copyAllRowsToolStripMenuItem";
            this.copyAllRowsToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.copyAllRowsToolStripMenuItem.Text = "Copy all rows";
            this.copyAllRowsToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // Action
            // 
            this.Action.DataPropertyName = "name";
            this.Action.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Action.DisplayStyleForCurrentCellOnly = true;
            this.Action.HeaderText = "Mouse Action";
            this.Action.Items.AddRange(new object[] {
            "",
            "Move",
            "Left Button Down",
            "Right Button Down",
            "Left Button Up",
            "Right Button Up"});
            this.Action.Name = "Action";
            this.Action.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Action.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // X
            // 
            this.X.DataPropertyName = "x";
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.Width = 50;
            // 
            // Y
            // 
            this.Y.DataPropertyName = "x";
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.Width = 50;
            // 
            // HWnd
            // 
            this.HWnd.DataPropertyName = "hWnd";
            this.HWnd.HeaderText = "HWnd";
            this.HWnd.Name = "HWnd";
            this.HWnd.Width = 90;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 382);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "MouseTracker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mouseTrackerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton CleanData;
        private System.Windows.Forms.ToolStripButton StartTracking;
        private System.Windows.Forms.ToolStripButton StopTracking;
        private System.Windows.Forms.BindingSource mouseTrackerBindingSource;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripButton InsertRow;
        private System.Windows.Forms.ToolStripButton RemoveRows;
        public System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ToolStripButton ShowDataGridView2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton HideDataGridView2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton Simulate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton About;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySelectedRowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAllRowsToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewComboBoxColumn Action;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewLinkColumn HWnd;
    }
}

