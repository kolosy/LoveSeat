using System;
using System.ComponentModel;
using System.Windows.Forms;
using Divan;
using System.IO;
using System.Security;
using Divan.Lucene;

namespace LoveSeat
{
    public partial class FrmMain : Form
    {
        const string MapTemplate =
@"function (doc) {
    
}";

        const string ReduceTemplate =
@"function (keys, values, rereduce) {
    
}";
        const string IndexTemplate =
@"function (doc) {
    var ret = new Document();

    return ret;
}";

        private const string ConfigFile = "loveseat.config";

        TreeNode _contextNode;
        CouchServer _svr;
        CouchViewDefinitionBase _currentDefinition;
        bool _isMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmMain"/> class.
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = String.Empty;

            var connection = TryLoadConnection();
            if (!String.IsNullOrEmpty(connection))
                tstServer.Text = connection;
        }

        /// <summary>
        /// Handles the Click event of the cmdOpen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cmdOpen_Click(object sender, EventArgs e)
        {
            LoadServer(tstServer.Text);
        }

        /// <summary>
        /// Connects to the server and builds a list of databases.
        /// </summary>
        /// <param name="connection">The connection.</param>
        private void LoadServer(string connection)
        {
            _svr = null;
            _currentDefinition = null;
            _isMap = false;
            tvMain.Nodes.Clear();
            rtSource.Clear();
            
            var parts = connection.Split(':');
            switch (parts.Length)
            {
                case 1:
                    _svr = new CouchServer(connection);
                    break;
                case 2:
                    _svr = new CouchServer(parts[0], Convert.ToInt32(parts[1]));
                    break;
                case 3:
                    _svr = new CouchServer(parts[1].TrimStart('/'), Convert.ToInt32(parts[2]));
                    break;
                default:
                    MessageBox.Show(String.Format("{0} is not a recognized URL", connection), "Can't connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            foreach (var db in _svr.GetDatabaseNames())
            {
                var node = tvMain.Nodes.Add(db, db);
                node.Tag = new CouchDatabase(db, _svr);
                node.Nodes.Add(String.Empty);
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
            }

            toolStripStatusLabel1.Text = "Connected to " + connection;

            SaveConnection(connection);
        }

        private void SaveConnection(string connection)
        {
            try
            {
                if (File.Exists(ConfigFile))
                    File.Delete(ConfigFile);

                File.WriteAllText(ConfigFile, connection);
            }
            catch (UnauthorizedAccessException)
            {
                // we don't have access, or something else. move on.
            }
            catch (SecurityException)
            {
                // we don't have access, or something else. move on.
            }
            catch (IOException)
            {
                // we don't have access, or something else. move on.
            }
        }

        private string TryLoadConnection()
        {
            try
            {
                if (!File.Exists(ConfigFile))
                    return null;

                return File.ReadAllText(ConfigFile);
            }
            catch (UnauthorizedAccessException)
            {
                // we don't have access, or something else. move on.
            }
            catch (SecurityException)
            {
                // we don't have access, or something else. move on.
            }
            catch (IOException)
            {
                // we don't have access, or something else. move on.
            }

            return null;
        }

        /// <summary>
        /// Handles the BeforeExpand event of the tvMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/> instance containing the event data.</param>
        private void tvMain_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 1 || !String.Empty.Equals(e.Node.Nodes[0].Text))
                return;

            e.Node.Nodes.Clear();

            if (e.Node.Tag is CouchDatabase)
                LoadDatabase(e.Node.Tag as CouchDatabase, e.Node);
            else if (e.Node.Tag is CouchDesignDocument)
                LoadView(e.Node.Tag as CouchDesignDocument, e.Node);
        }

        /// <summary>
        /// Loads the database into the treeview, under the given parent.
        /// </summary>
        /// <param name="couchDatabase">The couch database.</param>
        /// <param name="parent">The parent.</param>
        private void LoadDatabase(CouchDatabase couchDatabase, TreeNode parent)
        {
            foreach (var view in couchDatabase.QueryAllDocuments().StartKey("_design").EndKey("_designZZZZZZZZZZZZZZZZZ").GetResult().RowDocuments())
            {
                var design = couchDatabase.GetDocument<CouchDesignDocument>(view.Key);
                design.Owner = couchDatabase;
                CreateDesignNode(design, parent);
            }
        }

        /// <summary>
        /// Loads the view into the treeview, under the given parent.
        /// </summary>
        /// <param name="couchViewDefinition">The couch view definition.</param>
        /// <param name="parent">The parent.</param>
        private void LoadView(CouchDesignDocument couchViewDefinition, TreeNode parent)
        {
            foreach (var view in couchViewDefinition.Definitions)
            {
                TreeNode node = CreateViewNode(parent, view);
                CreateFunctionNode(view, node, "map");
                if (!String.IsNullOrEmpty(view.Reduce))
                    CreateFunctionNode(view, node, "reduce");
            }

            foreach (var index in couchViewDefinition.LuceneDefinitions)
            {
                TreeNode node = CreateFTINode(parent, index);
                CreateFunctionNode(index, node, "index");
            }
        }

        private TreeNode CreateFTINode(TreeNode parent, Divan.Lucene.CouchLuceneViewDefinition index)
        {
            var node = parent.Nodes.Add(index.Name);
            node.Tag = index;
            node.ImageIndex = 4;
            node.SelectedImageIndex = 4;
            return node;
        }

        /// <summary>
        /// Creates a design node.
        /// </summary>
        /// <param name="design">The design.</param>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        private TreeNode CreateDesignNode(CouchDesignDocument design, TreeNode parent)
        {
            var node = parent.Nodes.Add(design.Id);
            node.Nodes.Add(String.Empty);
            node.Tag = design;
            node.ImageIndex = 1;
            node.SelectedImageIndex = 1;

            return node;
        }

        /// <summary>
        /// Creates a function (map/reduce) node.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="node">The node.</param>
        /// <param name="function">The name of the function.</param>
        /// <returns></returns>
        private TreeNode CreateFunctionNode(CouchViewDefinitionBase view, TreeNode node, string function)
        {
            var functionNode = node.Nodes.Add(function, function);
            functionNode.Name = function;
            functionNode.Tag = view;
            functionNode.ImageIndex = 3;
            functionNode.SelectedImageIndex = 3;

            return functionNode;
        }

        /// <summary>
        /// Creates the view node.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="view">The view.</param>
        /// <returns></returns>
        private TreeNode CreateViewNode(TreeNode parent, CouchViewDefinition view)
        {
            var node = parent.Nodes.Add(view.Name);
            node.Tag = view;
            node.ImageIndex = 2;
            node.SelectedImageIndex = 2;
            return node;
        }

        /// <summary>
        /// Handles the DoubleClick event of the tvMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tvMain_DoubleClick(object sender, EventArgs e)
        {
            if (tvMain.SelectedNode == null)
                return;

            switch (tvMain.SelectedNode.Name)
            {
                case "map":
                    if (!VerifySave(true))
                        return;
                    _isMap = true;
                    _currentDefinition = (CouchViewDefinitionBase)tvMain.SelectedNode.Tag;
                    rtSource.Text = ((CouchViewDefinition)_currentDefinition).Map;

                    if (String.IsNullOrEmpty(rtSource.Text))
                        rtSource.Text = MapTemplate;

                    break;
                case "reduce":
                    if (!VerifySave(true))
                        return;
                    _isMap = false;
                    _currentDefinition = (CouchViewDefinitionBase)tvMain.SelectedNode.Tag;
                    rtSource.Text = ((CouchViewDefinition)_currentDefinition).Reduce;

                    if (String.IsNullOrEmpty(rtSource.Text))
                        rtSource.Text = ReduceTemplate;

                    break;
                case "index":
                    if (!VerifySave(true))
                        return;
                    _isMap = false;
                    _currentDefinition = (CouchViewDefinitionBase)tvMain.SelectedNode.Tag;
                    rtSource.Text = ((CouchLuceneViewDefinition)_currentDefinition).Index;

                    if (String.IsNullOrEmpty(rtSource.Text))
                        rtSource.Text = IndexTemplate;

                    break;
            }
        }

        /// <summary>
        /// Saves the active text if necessary, and propmts for a yes/no/cancel action when <c>prompt</c> is <c>true</c>
        /// </summary>
        /// <param name="prompt">if set to <c>true</c> prompts for save confirmation.</param>
        /// <returns>whether the save occurred</returns>
        private bool VerifySave(bool prompt)
        {
            if (_currentDefinition == null || 
                    (((_currentDefinition is CouchViewDefinition) &&
                        ((_isMap && (((CouchViewDefinition)_currentDefinition).Map == rtSource.Text)) ||
                        (!_isMap && (((CouchViewDefinition)_currentDefinition).Reduce == rtSource.Text)))) ||
                    ((_currentDefinition is CouchLuceneViewDefinition) &&
                        (((CouchLuceneViewDefinition)_currentDefinition).Index == rtSource.Text))))
            {
                if (_currentDefinition != null)
                    toolStripStatusLabel1.Text = String.Format("{0} is up to date.", _currentDefinition.Name);
                return true;
            }

            switch (
                prompt ? 
                MessageBox.Show(this, "Do you want to save changes to " + tvMain.SelectedNode.FullPath + "?", "Save Changes?", MessageBoxButtons.YesNoCancel) : 
                DialogResult.Yes)
            {
                case DialogResult.Yes:
                    if (_currentDefinition is CouchViewDefinition)
                    {
                        if (_isMap)
                            ((CouchViewDefinition)_currentDefinition).Map = rtSource.Text;
                        else
                            ((CouchViewDefinition)_currentDefinition).Reduce = rtSource.Text;
                    }
                    else
                        ((CouchLuceneViewDefinition)_currentDefinition).Index = rtSource.Text;

                    _currentDefinition.Doc.Synch();
                    toolStripStatusLabel1.Text = String.Format("Saved {0}.", _currentDefinition.Name);
                    break;
                case DialogResult.Cancel:
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Handles the Click event of the cmdCommit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cmdCommit_Click(object sender, EventArgs e)
        {
            VerifySave(false);
        }

        /// <summary>
        /// Handles the Click event of the addReduceToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void addReduceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_contextNode == null)
                return;

            CreateFunctionNode((CouchViewDefinition)_contextNode.Tag, _contextNode, "reduce");
        }

        /// <summary>
        /// Handles the Opening event of the contextMenuStrip1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            _contextNode = tvMain.GetNodeAt(tvMain.PointToClient(Cursor.Position));

            addReduceToolStripMenuItem.Visible =
                (_contextNode != null) &&
                (_contextNode.Tag is CouchViewDefinition) &&
                (_contextNode.Nodes.Count != 2);

            addDesignToolStripMenuItem.Visible =
                (_contextNode != null) &&
                (_contextNode.Tag is CouchDatabase);

            addViewToolStripMenuItem.Visible =
                (_contextNode != null) &&
                (_contextNode.Tag is CouchDesignDocument);

            addLuceneIndexToolStripMenuItem.Visible =
                (_contextNode != null) &&
                (_contextNode.Tag is CouchDesignDocument);

            ctxSeparator.Visible = !(_contextNode == null || _contextNode.Tag == null);
        }

        /// <summary>
        /// Handles the Click event of the addViewToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void addViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_contextNode == null)
                return;

            using (var dialog = new dlgName("View name", "New View"))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                var view = ((CouchDesignDocument)_contextNode.Tag).AddView(dialog.EnteredName, String.Empty);

                var viewNode = CreateViewNode(_contextNode, view);
                var mapNode = CreateFunctionNode(view, viewNode, "map");

                mapNode.EnsureVisible();
            }
        }

        /// <summary>
        /// Handles the Click event of the addDesignToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void addDesignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_contextNode == null)
                return;

            using (var dialog = new dlgName("Design name", "New Design"))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                var design = new CouchDesignDocument(dialog.EnteredName,
                                                     ((CouchDatabase)_contextNode.Tag));

                var designNode = CreateDesignNode(design, _contextNode);
                designNode.EnsureVisible();
            }
        }


        private void addLuceneIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_contextNode == null)
                return;

            using (var dialog = new dlgName("Index name", "New Index"))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                var view = ((CouchDesignDocument)_contextNode.Tag).AddLuceneView(dialog.EnteredName, String.Empty);

                var viewNode = CreateFTINode(_contextNode, view);
                var indexNode = CreateFunctionNode(view, viewNode, "index");

                indexNode.EnsureVisible();
            }
        }
        
        /// <summary>
        /// Handles the Click event of the refreshToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadServer(tstServer.Text);
        }

        /// <summary>
        /// Handles the KeyUp event of the frmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (((e.Modifiers & Keys.Control) == Keys.Control) && e.KeyCode == Keys.S)
                VerifySave(false);
        }
    }
}