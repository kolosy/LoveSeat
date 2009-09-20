namespace LoveSeat
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvMain = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addReduceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDesignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.rtSource = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstServer = new System.Windows.Forms.ToolStripTextBox();
            this.cmdOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdCommit = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtSource);
            this.splitContainer1.Size = new System.Drawing.Size(706, 495);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvMain
            // 
            this.tvMain.ContextMenuStrip = this.contextMenuStrip1;
            this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMain.ImageIndex = 0;
            this.tvMain.ImageList = this.imageList1;
            this.tvMain.Location = new System.Drawing.Point(0, 0);
            this.tvMain.Name = "tvMain";
            this.tvMain.PathSeparator = "/";
            this.tvMain.SelectedImageIndex = 0;
            this.tvMain.Size = new System.Drawing.Size(235, 495);
            this.tvMain.TabIndex = 0;
            this.tvMain.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvMain_BeforeExpand);
            this.tvMain.DoubleClick += new System.EventHandler(this.tvMain_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addReduceToolStripMenuItem,
            this.addViewToolStripMenuItem,
            this.addDesignToolStripMenuItem,
            this.ctxSeparator,
            this.refreshToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(139, 98);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // addReduceToolStripMenuItem
            // 
            this.addReduceToolStripMenuItem.Name = "addReduceToolStripMenuItem";
            this.addReduceToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.addReduceToolStripMenuItem.Text = "Add Reduce";
            this.addReduceToolStripMenuItem.Click += new System.EventHandler(this.addReduceToolStripMenuItem_Click);
            // 
            // addViewToolStripMenuItem
            // 
            this.addViewToolStripMenuItem.Name = "addViewToolStripMenuItem";
            this.addViewToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.addViewToolStripMenuItem.Text = "Add View";
            this.addViewToolStripMenuItem.Click += new System.EventHandler(this.addViewToolStripMenuItem_Click);
            // 
            // addDesignToolStripMenuItem
            // 
            this.addDesignToolStripMenuItem.Name = "addDesignToolStripMenuItem";
            this.addDesignToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.addDesignToolStripMenuItem.Text = "Add Design";
            this.addDesignToolStripMenuItem.Click += new System.EventHandler(this.addDesignToolStripMenuItem_Click);
            // 
            // ctxSeparator
            // 
            this.ctxSeparator.Name = "ctxSeparator";
            this.ctxSeparator.Size = new System.Drawing.Size(135, 6);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "database.bmp");
            this.imageList1.Images.SetKeyName(1, "DataSet_TableView.bmp");
            this.imageList1.Images.SetKeyName(2, "Table.bmp");
            this.imageList1.Images.SetKeyName(3, "FormulaEvaluator.bmp");
            // 
            // rtSource
            // 
            this.rtSource.AcceptsTab = true;
            this.rtSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtSource.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtSource.Location = new System.Drawing.Point(0, 0);
            this.rtSource.Name = "rtSource";
            this.rtSource.Size = new System.Drawing.Size(467, 495);
            this.rtSource.TabIndex = 0;
            this.rtSource.TabStop = false;
            this.rtSource.Text = "";
            this.rtSource.WordWrap = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tstServer,
            this.cmdOpen,
            this.toolStripSeparator1,
            this.cmdCommit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(706, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel1.Text = "Server";
            // 
            // tstServer
            // 
            this.tstServer.Name = "tstServer";
            this.tstServer.Size = new System.Drawing.Size(400, 25);
            // 
            // cmdOpen
            // 
            this.cmdOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdOpen.Image = ((System.Drawing.Image)(resources.GetObject("cmdOpen.Image")));
            this.cmdOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.Size = new System.Drawing.Size(23, 22);
            this.cmdOpen.Text = "Connect to Server";
            this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdCommit
            // 
            this.cmdCommit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdCommit.Image = ((System.Drawing.Image)(resources.GetObject("cmdCommit.Image")));
            this.cmdCommit.ImageTransparentColor = System.Drawing.Color.Black;
            this.cmdCommit.Name = "cmdCommit";
            this.cmdCommit.Size = new System.Drawing.Size(23, 22);
            this.cmdCommit.Text = "Commit";
            this.cmdCommit.Click += new System.EventHandler(this.cmdCommit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 526);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(706, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 548);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Couch Surfer";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvMain;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdCommit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton cmdOpen;
        private System.Windows.Forms.RichTextBox rtSource;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addReduceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator ctxSeparator;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addDesignToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripTextBox tstServer;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

