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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addReduceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addLuceneIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDesignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cloneViewsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractViewsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importViewsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvMain = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.rtSource = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fontsAndColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tvResults = new System.Windows.Forms.TreeView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.txtParams = new System.Windows.Forms.ToolStripTextBox();
            this.cmdRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdCommit = new System.Windows.Forms.ToolStripButton();
            this.cmdResults = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstServer = new System.Windows.Forms.ToolStripTextBox();
            this.cmdOpen = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.addDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addReduceToolStripMenuItem,
            this.addViewToolStripMenuItem,
            this.addLuceneIndexToolStripMenuItem,
            this.addDesignToolStripMenuItem,
            this.addDatabaseToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.ctxSeparator,
            this.refreshToolStripMenuItem,
            this.ctxSeparator2,
            this.cloneViewsToolStripMenuItem,
            this.extractViewsToolStripMenuItem,
            this.importViewsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(169, 258);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // addReduceToolStripMenuItem
            // 
            this.addReduceToolStripMenuItem.Name = "addReduceToolStripMenuItem";
            this.addReduceToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.addReduceToolStripMenuItem.Text = "Add Reduce";
            this.addReduceToolStripMenuItem.Click += new System.EventHandler(this.addReduceToolStripMenuItem_Click);
            // 
            // addViewToolStripMenuItem
            // 
            this.addViewToolStripMenuItem.Name = "addViewToolStripMenuItem";
            this.addViewToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.addViewToolStripMenuItem.Text = "Add View";
            this.addViewToolStripMenuItem.Click += new System.EventHandler(this.addViewToolStripMenuItem_Click);
            // 
            // addLuceneIndexToolStripMenuItem
            // 
            this.addLuceneIndexToolStripMenuItem.Name = "addLuceneIndexToolStripMenuItem";
            this.addLuceneIndexToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.addLuceneIndexToolStripMenuItem.Text = "Add Lucene Index";
            this.addLuceneIndexToolStripMenuItem.Click += new System.EventHandler(this.addLuceneIndexToolStripMenuItem_Click);
            // 
            // addDesignToolStripMenuItem
            // 
            this.addDesignToolStripMenuItem.Name = "addDesignToolStripMenuItem";
            this.addDesignToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.addDesignToolStripMenuItem.Text = "Add Design";
            this.addDesignToolStripMenuItem.Click += new System.EventHandler(this.addDesignToolStripMenuItem_Click);
            // 
            // ctxSeparator
            // 
            this.ctxSeparator.Name = "ctxSeparator";
            this.ctxSeparator.Size = new System.Drawing.Size(165, 6);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // ctxSeparator2
            // 
            this.ctxSeparator2.Name = "ctxSeparator2";
            this.ctxSeparator2.Size = new System.Drawing.Size(165, 6);
            // 
            // cloneViewsToolStripMenuItem
            // 
            this.cloneViewsToolStripMenuItem.Name = "cloneViewsToolStripMenuItem";
            this.cloneViewsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.cloneViewsToolStripMenuItem.Text = "Clone Views";
            this.cloneViewsToolStripMenuItem.Click += new System.EventHandler(this.cloneViewsToolStripMenuItem_Click);
            // 
            // extractViewsToolStripMenuItem
            // 
            this.extractViewsToolStripMenuItem.Name = "extractViewsToolStripMenuItem";
            this.extractViewsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.extractViewsToolStripMenuItem.Text = "Extract Views";
            this.extractViewsToolStripMenuItem.Click += new System.EventHandler(this.extractViewsToolStripMenuItem_Click);
            // 
            // importViewsToolStripMenuItem
            // 
            this.importViewsToolStripMenuItem.Name = "importViewsToolStripMenuItem";
            this.importViewsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.importViewsToolStripMenuItem.Text = "Import Views";
            this.importViewsToolStripMenuItem.Click += new System.EventHandler(this.importViewsToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "database.bmp");
            this.imageList1.Images.SetKeyName(1, "DataSet_TableView.bmp");
            this.imageList1.Images.SetKeyName(2, "Table.bmp");
            this.imageList1.Images.SetKeyName(3, "FormulaEvaluator.bmp");
            this.imageList1.Images.SetKeyName(4, "Textbox.bmp");
            this.imageList1.Images.SetKeyName(5, "Webcontrol_Sitemapdatasrc.bmp");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1017, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1017, 486);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(1017, 558);
            this.toolStripContainer1.TabIndex = 5;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1017, 486);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.TabIndex = 2;
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
            this.tvMain.Size = new System.Drawing.Size(235, 486);
            this.tvMain.TabIndex = 0;
            this.tvMain.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvMain_BeforeExpand);
            this.tvMain.DoubleClick += new System.EventHandler(this.tvMain_DoubleClick);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.rtSource);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tvResults);
            this.splitContainer2.Panel2Collapsed = true;
            this.splitContainer2.Size = new System.Drawing.Size(778, 486);
            this.splitContainer2.SplitterDistance = 243;
            this.splitContainer2.TabIndex = 0;
            // 
            // rtSource
            // 
            this.rtSource.AcceptsTab = true;
            this.rtSource.ContextMenuStrip = this.contextMenuStrip3;
            this.rtSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtSource.Location = new System.Drawing.Point(0, 0);
            this.rtSource.Name = "rtSource";
            this.rtSource.Size = new System.Drawing.Size(778, 486);
            this.rtSource.TabIndex = 0;
            this.rtSource.Text = "";
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontsAndColorsToolStripMenuItem});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(164, 26);
            // 
            // fontsAndColorsToolStripMenuItem
            // 
            this.fontsAndColorsToolStripMenuItem.Name = "fontsAndColorsToolStripMenuItem";
            this.fontsAndColorsToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.fontsAndColorsToolStripMenuItem.Text = "Fonts and Colors";
            this.fontsAndColorsToolStripMenuItem.Click += new System.EventHandler(this.fontsAndColorsToolStripMenuItem_Click);
            // 
            // tvResults
            // 
            this.tvResults.ContextMenuStrip = this.contextMenuStrip2;
            this.tvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvResults.Location = new System.Drawing.Point(0, 0);
            this.tvResults.Name = "tvResults";
            this.tvResults.Size = new System.Drawing.Size(150, 46);
            this.tvResults.TabIndex = 0;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.collapseAllToolStripMenuItem,
            this.expandAllToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(137, 48);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.collapseAllToolStripMenuItem.Text = "Collapse All";
            this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.expandAllToolStripMenuItem.Text = "Expand All";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.txtParams,
            this.cmdRun,
            this.toolStripSeparator2,
            this.cmdCommit,
            this.cmdResults});
            this.toolStrip2.Location = new System.Drawing.Point(3, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(415, 25);
            this.toolStrip2.TabIndex = 4;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(101, 22);
            this.toolStripLabel2.Text = "Query Parameters";
            // 
            // txtParams
            // 
            this.txtParams.Name = "txtParams";
            this.txtParams.Size = new System.Drawing.Size(200, 25);
            this.txtParams.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtParams_KeyUp);
            // 
            // cmdRun
            // 
            this.cmdRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdRun.Image = ((System.Drawing.Image)(resources.GetObject("cmdRun.Image")));
            this.cmdRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdRun.Name = "cmdRun";
            this.cmdRun.Size = new System.Drawing.Size(23, 22);
            this.cmdRun.Text = "Run Query";
            this.cmdRun.Click += new System.EventHandler(this.cmdRun_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdCommit
            // 
            this.cmdCommit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdCommit.Image = ((System.Drawing.Image)(resources.GetObject("cmdCommit.Image")));
            this.cmdCommit.ImageTransparentColor = System.Drawing.Color.Black;
            this.cmdCommit.Name = "cmdCommit";
            this.cmdCommit.Size = new System.Drawing.Size(23, 22);
            this.cmdCommit.Text = "Save";
            this.cmdCommit.Click += new System.EventHandler(this.cmdCommit_Click);
            // 
            // cmdResults
            // 
            this.cmdResults.CheckOnClick = true;
            this.cmdResults.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cmdResults.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdResults.Name = "cmdResults";
            this.cmdResults.Size = new System.Drawing.Size(48, 22);
            this.cmdResults.Text = "Results";
            this.cmdResults.CheckedChanged += new System.EventHandler(this.cmdResults_CheckedChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tstServer,
            this.cmdOpen});
            this.toolStrip1.Location = new System.Drawing.Point(3, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(476, 25);
            this.toolStrip1.TabIndex = 3;
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
            this.tstServer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstServer_KeyUp);
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Javascript Files|*.js|All Files|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Javascript Files|*.js|All Files|*.*";
            // 
            // addDatabaseToolStripMenuItem
            // 
            this.addDatabaseToolStripMenuItem.Name = "addDatabaseToolStripMenuItem";
            this.addDatabaseToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.addDatabaseToolStripMenuItem.Text = "Add Database";
            this.addDatabaseToolStripMenuItem.Click += new System.EventHandler(this.addDatabaseToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 558);
            this.Controls.Add(this.toolStripContainer1);
            this.KeyPreview = true;
            this.Name = "FrmMain";
            this.Text = "LoveSeat";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addReduceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator ctxSeparator;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addDesignToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem addLuceneIndexToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tstServer;
        private System.Windows.Forms.ToolStripButton cmdOpen;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvMain;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView tvResults;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox txtParams;
        private System.Windows.Forms.ToolStripButton cmdRun;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton cmdCommit;
        private System.Windows.Forms.ToolStripButton cmdResults;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator ctxSeparator2;
        private System.Windows.Forms.ToolStripMenuItem cloneViewsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractViewsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importViewsToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem fontsAndColorsToolStripMenuItem;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.RichTextBox rtSource;
        private System.Windows.Forms.ToolStripMenuItem addDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}

