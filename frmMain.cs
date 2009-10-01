using System;
using System.ComponentModel;
using System.Windows.Forms;
using Divan;
using System.IO;
using System.Security;
using Divan.Lucene;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LoveSeat
{
    public partial class FrmMain : Form
    {
        class Settings
        {
            public string Connection;
            public int FormX, FormY, FormWidth, FormHeight;
        }

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
        Settings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmMain"/> class.
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = String.Empty;
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
            _svr = CreateServer(parts);
            if (_svr == null)
                return;

            foreach (var db in _svr.GetDatabaseNames())
            {
                var node = tvMain.Nodes.Add(db, db);
                node.Tag = new CouchDatabase(db, _svr);
                node.Nodes.Add(String.Empty);
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
            }

            toolStripStatusLabel1.Text = "Connected to " + connection;

            settings.Connection = connection;
        }

        private CouchServer CreateServer(string[] parts)
        {
            switch (parts.Length)
            {
                case 1:
                    return new CouchServer(parts[0]);
                case 2:
                    return new CouchServer(parts[0], Convert.ToInt32(parts[1]));
                case 3:
                    return new CouchServer(parts[1].TrimStart('/'), Convert.ToInt32(parts[2]));
                default:
                    MessageBox.Show(String.Format("{0} is not a recognized URL", parts[0]), "Can't connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
            }
        }

        private void SaveSettings(Settings settings)
        {
            try
            {
                if (File.Exists(ConfigFile))
                    File.Delete(ConfigFile);

                using (var streamWriter = new StreamWriter(File.OpenWrite(ConfigFile))) {
                    new JsonSerializer().Serialize(streamWriter, settings);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
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

        private Settings TryLoadSettings()
        {
            try
            {
                if (!File.Exists(ConfigFile))
                    return null;
                
                using (var reader = File.OpenText(ConfigFile))
                {
                    return (Settings)new JsonSerializer().Deserialize(reader, typeof(Settings));
                }
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

            cloneViewsToolStripMenuItem.Visible =
                (_contextNode != null) &&
                (_contextNode.Tag is CouchDatabase);

            extractViewsToolStripMenuItem.Visible =
                (_contextNode != null) &&
                (_contextNode.Tag is CouchDatabase);

            importViewsToolStripMenuItem.Visible =
                (_contextNode != null) &&
                (_contextNode.Tag is CouchDatabase);

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

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            settings.FormX = Left;
            settings.FormY = Top;
            settings.FormWidth = Width;
            settings.FormHeight = Height;

            SaveSettings(settings);
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            settings = TryLoadSettings();
            if (settings != null)
            {
                if (!String.IsNullOrEmpty(settings.Connection))
                    tstServer.Text = settings.Connection;

                if (settings.FormX != 0)
                    Left = settings.FormX;

                if (settings.FormY != 0)
                    Top = settings.FormY;

                if (settings.FormWidth != 0)
                    Width = settings.FormWidth;

                if (settings.FormHeight != 0)
                    Height = settings.FormHeight;
            }
            else
                settings = new Settings();
        }

        private void cmdResults_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = !cmdResults.Checked;
        }

        private void cmdRun_Click(object sender, EventArgs e)
        {
            if (_currentDefinition == null)
                return;

            if (!VerifySave(true))
            {
                toolStripStatusLabel1.Text = "Command aborted";
                return;
            }

            var viewDefinition = _currentDefinition as CouchViewDefinition;
            tvResults.Nodes.Clear();

            var root = tvResults.Nodes.Add(viewDefinition.Path() + "/" + txtParams.Text);

            if (viewDefinition == null)
            {
                var luceneDefinition = _currentDefinition as CouchLuceneViewDefinition;
                var luceneQuery = luceneDefinition.Query();
                if (!String.IsNullOrEmpty(txtParams.Text))
                    foreach (var optionSet in txtParams.Text.Split('&'))
                    {
                        var option = optionSet.Split('&');
                        luceneQuery.Options[option[0]] = option[1];
                    }

                try
                {
                    ShowResult(root, luceneQuery.GetResult().result, null);
                }
                catch (Exception ex)
                {
                    root.Nodes.Add("Error: " + ex.Message);
                    MessageBox.Show(ex.ToString(), "Exception running view", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var viewQuery = viewDefinition.Query();
                if (!String.IsNullOrEmpty(txtParams.Text))
                    foreach (var optionSet in txtParams.Text.Split('&'))
                    {
                        var option = optionSet.Split('=');
                        if (option.Length != 2)
                            throw new Exception(txtParams.Text + " is not a valid view query string.");

                        viewQuery.Options[option[0]] = option[1];
                    }

                try
                {
                    ShowResult(root, viewQuery.GetResult().result, null);
                }
                catch (Exception ex)
                {
                    root.Nodes.Add("Error: " + ex.Message);
                    MessageBox.Show(ex.ToString(), "Exception running view", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            ShowResults();
        }

        private void ShowResults()
        {
            splitContainer2.Panel2Collapsed = false;
            cmdResults.Checked = true;
        }

        private void ShowResult(TreeNode root, JToken value, JToken parent)
        {
            switch (value.Type)
            {
                case JTokenType.Array:
                    var i = 0;
                    foreach (var element in (JArray)value)
                        ShowResult(root.Nodes.Add("element " + (i++)), element, value);
                    break;
                case JTokenType.Object:
                    foreach (var property in ((JObject)value))
                        ShowResult(root.Nodes.Add(property.Key), property.Value, property.Value.Parent);
                    break;
                default:
                    if (parent != null && parent.Type == JTokenType.Property)
                        root.Text += ": " + value.ToString();
                    else
                        root.Nodes.Add(value.ToString());
                    break;
            }
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tvResults.CollapseAll();
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tvResults.ExpandAll();
        }

        private void cloneViewsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CouchDatabase sourceDb = (CouchDatabase)_contextNode.Tag;

            using (var dialog = new dlgName("Specify Target", "Target Server/database"))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                var dbParts = dialog.EnteredName.Replace("//", "").Split('/');
                var svr = CreateServer(dbParts[0].Split(':'));
                var targetDb = svr.GetDatabase(dbParts[1]);

                foreach (var view in sourceDb.QueryAllDocuments().StartKey("_design").EndKey("_designZZZZZZZZZZZZZZZZZ").GetResult().RowDocuments())
                {
                    var design = sourceDb.GetDocument<CouchDesignDocument>(view.Key);
                    
                    // need to make sure to overwrite the target
                    if (targetDb.HasDocument(design.Id))
                        design.Rev = targetDb.GetDocument(design.Id).Rev;
                    else
                        design.Rev = null;

                    design.Owner = targetDb;
                    design.Synch();
                }
            }
        }

        private void extractViewsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CouchDatabase sourceDb = (CouchDatabase)_contextNode.Tag;

            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                return;

            foreach (var view in sourceDb.QueryAllDocuments().StartKey("_design").EndKey("_designZZZZZZZZZZZZZZZZZ").GetResult().RowDocuments())
            {
                var design = sourceDb.GetDocument<CouchDesignDocument>(view.Key);

                using (var streamWriter = new StreamWriter(File.OpenWrite(Path.Combine(folderBrowserDialog1.SelectedPath, design.Id.Substring(8) + ".js"))))
                {
                    var writer = new JsonTextWriter(streamWriter);
                    writer.Indentation = 1;
                    writer.IndentChar = '\t';
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartObject();
                    design.WriteJson(writer);
                    writer.WriteEndObject();
                }
            }
        }

        private void importViewsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CouchDatabase targetDb = (CouchDatabase)_contextNode.Tag;

            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                return;

            foreach (var file in Directory.GetFiles(folderBrowserDialog1.SelectedPath))
            {
                using (var reader = new StreamReader(File.OpenRead(file)))
                {
                    var design = new CouchDesignDocument();
                    design.ReadJson((JObject)JToken.ReadFrom(new JsonTextReader(reader)));
                    
                    // need to make sure to overwrite the target
                    if (targetDb.HasDocument(design.Id))
                        design.Rev = targetDb.GetDocument(design.Id).Rev;
                    else
                        design.Rev = null;
                    
                    design.Owner = targetDb;
                    design.Synch();
                }
            }
        }
    }
}