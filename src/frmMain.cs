/**
 *   Copyright 2010 Alex Pedenko
 *
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Divan;
using System.IO;
using System.Security;
using Divan.Lucene;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UrielGuy.SyntaxHighlightingTextBox;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Diagnostics;

namespace LoveSeat
{
    public partial class FrmMain : Form
    {
        class Settings
        {
            public string Connection;
            public int FormX, FormY, FormWidth, FormHeight;
            public Font Font;
        }

        readonly Dictionary<string, string> templates = new Dictionary<string, string>
        {
            { "map", "function (doc) {\r\n\t\r\n}" },
            { "reduce", "function (keys, values, rereduce) {\r\n\t\r\n}" },
            { "fti", "function (doc) {\r\n\tvar ret = new Document();\r\n\r\n\treturn ret;\r\n}" },
            { "show", "function (doc, req) {\r\n\t\r\n}" },
            { "list", "function (head, req) {\r\n\t\r\n}" }
        };


        private const string ConfigFile = "loveseat.config";

        private TreeNode _contextNode;
        private TreeNode _currentNode;
        private CouchServer _svr;
        private Settings settings;
        private string buffer;
        
        private bool runningOnMono = Type.GetType("Mono.Runtime") != null;


        /// <summary>
        /// Initializes a new instance of the <see cref="FrmMain"/> class.
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = String.Empty;

            // current highlighting editor implementation doesn't play nice with mono
            if (!runningOnMono)
                ConfigureHighlightingEditor();
			else
				this.Icon = null;
        }

        /// <summary>
        /// Configures the editor.
        /// </summary>
        public void ConfigureHighlightingEditor()
        {
            splitContainer2.Panel1.Controls.Remove(rtSource);

            var editor = new SyntaxHighlightingTextBox
                             {
                                 CaseSensitive = false,
                                 ContextMenuStrip = contextMenuStrip3,
                                 Dock = DockStyle.Fill,
                                 FilterAutoComplete = false,
                                 Font =
                                     new Font("Anonymous", 8.999999F, FontStyle.Regular, GraphicsUnit.Point,
                                              ((byte) (0))),
                                 Location = new Point(0, 0),
                                 MaxUndoRedoSteps = 50,
                                 Name = "rtSource",
                                 ShowSelectionMargin = true,
                                 Size = new Size(778, 486),
                                 TabIndex = 0,
                                 Text = "",
                                 WordWrap = false,
                                 AcceptsTab = true
                             };

            editor.Seperators.Add(' ');
            editor.Seperators.Add('\r');
            editor.Seperators.Add('\t');
            editor.Seperators.Add('\n');
            editor.Seperators.Add(',');
            editor.Seperators.Add('.');
            editor.Seperators.Add('-');
            editor.Seperators.Add('+');
            editor.Seperators.Add('(');
            editor.Seperators.Add(')');
            editor.Seperators.Add(';');
            editor.Seperators.Add('=');
            editor.Seperators.Add('*');
            editor.Seperators.Add('/');

            editor.WordWrap = false;
            editor.ScrollBars = RichTextBoxScrollBars.Both;// & RichTextBoxScrollBars.ForcedVertical;

            editor.FilterAutoComplete = false;

            foreach (var kw in SimpleJSLanguageModel.Keywords)
                editor.HighlightDescriptors.Add(
                    new HighlightDescriptor(
                        kw,
                        Color.Blue,
                        null,
                        DescriptorType.Word,
                        DescriptorRecognition.WholeWord,
                        true));

            foreach (var kw in SimpleJSLanguageModel.ReservedWords)
                editor.HighlightDescriptors.Add(
                    new HighlightDescriptor(
                        kw,
                        Color.Green,
                        null,
                        DescriptorType.Word,
                        DescriptorRecognition.WholeWord,
                        true));

            rtSource = editor;
            splitContainer2.Panel1.Controls.Add(rtSource);
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
            try
            {
                _svr = null;
                _currentNode = null;
                buffer = null;
                tvMain.Nodes.Clear();
                rtSource.Clear();

                var parts = connection.Split(':');
                _svr = CreateServer(parts);
                if (_svr == null)
                    return;

                tvMain.Nodes.Clear();
                var svrNode = tvMain.Nodes.Add(parts[0], parts[0]);
                svrNode.Tag = _svr;
                svrNode.ImageIndex = 5;
                svrNode.SelectedImageIndex = 5;

                foreach (var db in _svr.GetDatabaseNames())
                {
                    var node = svrNode.Nodes.Add(db, db);
                    node.Tag = new CouchDatabase(db, _svr);
                    node.Nodes.Add(String.Empty);
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                }

                toolStripStatusLabel1.Text = "Connected to " + connection;

                settings.Connection = connection;
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message, "Unable to connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            else if (e.Node.Tag is GenericDesignDocument)
                LoadDesign(e.Node.Tag as GenericDesignDocument, e.Node);
        }

        /// <summary>
        /// Loads the database into the treeview, under the given parent.
        /// </summary>
        /// <param name="couchDatabase">The couch database.</param>
        /// <param name="parent">The parent.</param>
        private void LoadDatabase(CouchDatabase couchDatabase, TreeNode parent)
        {
            foreach (var view in couchDatabase.QueryAllDocuments().StartKey("_design").EndKey("_design0").GetResult().RowDocuments())
            {
                var design = couchDatabase.GetDocument<GenericDesignDocument>(view.Key);
                design.Owner = couchDatabase;
                CreateDesignNode(design, parent);
            }
        }

        private void LoadDesign(GenericDesignDocument designDoc, TreeNode parent)
        {
            var viewsNode = CreateViewsNode(parent);
            foreach (var view in designDoc.Definitions)
            {
                TreeNode node = CreateViewNode(viewsNode, view);
                CreateGenericFunctionNode("map", node, "map");
                if (!String.IsNullOrEmpty(view.Reduce))
                    CreateGenericFunctionNode("reduce", node, "reduce");
            }

            var ftiNode = CreateIndicesNode(parent);
            foreach (var index in designDoc.LuceneDefinitions)
                CreateGenericFunctionNode("fti", ftiNode, index.Name);

            var showsNode = CreateShowsNode(parent);
            foreach (var show in designDoc.Shows)
                CreateGenericFunctionNode("show", showsNode, show.Name);

            var listsNode = CreateListsNode(parent);
            foreach (var list in designDoc.Lists)
                CreateGenericFunctionNode("list", listsNode, list.Name);
        }

        /// <summary>
        /// Creates a design node.
        /// </summary>
        /// <param name="design">The design.</param>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        private TreeNode CreateDesignNode(GenericDesignDocument design, TreeNode parent)
        {
            var node = parent.Nodes.Add(design.Id);
            node.Nodes.Add(String.Empty);
            node.Tag = design;
            node.ImageIndex = 1;
            node.SelectedImageIndex = 1;

            return node;
        }

        private TreeNode CreateViewsNode(TreeNode parent)
        {
            var viewsNode = parent.Nodes.Add("views");
            viewsNode.Name = "views";
            viewsNode.Tag = "views";
            viewsNode.ImageIndex = 8;
            viewsNode.SelectedImageIndex = 8;

            return viewsNode;
        }

        private TreeNode CreateIndicesNode(TreeNode parent)
        {
            var indicesNode = parent.Nodes.Add("indices");
            indicesNode.Name = "indices";
            indicesNode.Tag = "indices";
            indicesNode.ImageIndex = 4;
            indicesNode.SelectedImageIndex = 4;

            return indicesNode;
        }

        private TreeNode CreateShowsNode(TreeNode parent)
        {
            var showsNode = parent.Nodes.Add("shows");
            showsNode.Name = "shows";
            showsNode.Tag = "shows";
            showsNode.ImageIndex = 6;
            showsNode.SelectedImageIndex = 6;

            return showsNode;
        }

        private TreeNode CreateListsNode(TreeNode parent)
        {
            var listsNode = parent.Nodes.Add("lists");
            listsNode.Name = "lists";
            listsNode.Tag = "lists";
            listsNode.ImageIndex = 7;
            listsNode.SelectedImageIndex = 7;

            return listsNode;
        }

        private TreeNode CreateGenericFunctionNode(string functionType, TreeNode node, string name)
        {
            var functionNode = node.Nodes.Add(name);
            functionNode.Name = name;
            functionNode.Tag = functionType;
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
            var node = parent.Nodes.Add(view.Name, view.Name);
            node.Tag = view;
            node.ImageIndex = 2;
            node.SelectedImageIndex = 2;
            return node;
        }

        private void updateBuffer()
        {
            buffer = rtSource.Text;
        }

        private bool HasBufferChanged()
        {
            return (buffer ?? "") != (rtSource.Text ?? "");
        }

        private T GetMetaElement<T>(TreeNode node) where T: class
        {
            if (node == null)
                return null;

            var doc = node.Tag as T;
            if (doc == null)
                return GetMetaElement<T>(node.Parent);

            return doc;
        }

        private GenericDesignDocument GetDesignDoc(TreeNode node)
        {
            return GetMetaElement<GenericDesignDocument>(node);
        }

        private string GetSourceByNode(TreeNode node)
        {
            if (!(node.Tag is string))
                return String.Empty;

            var designDoc = GetDesignDoc(node);
            switch (node.Tag as string)
            {
                case "map":
                    return designDoc.Definitions.Where(view => view.Name == node.Parent.Name).FirstOrDefault().Map;
                case "reduce":
                    return designDoc.Definitions.Where(view => view.Name == node.Parent.Name).FirstOrDefault().Reduce;
                case "fti":
                    return designDoc.LuceneDefinitions.Where(view => view.Name == node.Name).FirstOrDefault().Index;
                case "show":
                    return designDoc.Shows.Where(fnc => fnc.Name == node.Name).FirstOrDefault().Function;
                case "list":
                    return designDoc.Lists.Where(fnc => fnc.Name == node.Name).FirstOrDefault().Function;
            }

            return String.Empty;
        }

        private void SetSourceByNode(TreeNode node, string source)
        {
            if (!(node.Tag is string))
                return;

            var designDoc = GetDesignDoc(node);
            switch (node.Tag as string)
            {
                case "map":
                    designDoc.Definitions.Where(view => view.Name == node.Parent.Name).FirstOrDefault().Map = source;
                    break;
                case "reduce":
                    designDoc.Definitions.Where(view => view.Name == node.Parent.Name).FirstOrDefault().Reduce = source;
                    break;
                case "fti":
                    var index = designDoc.LuceneDefinitions.Where(idx => idx.Name == node.Name).FirstOrDefault();
                    if (index == null)
                        designDoc.AddLuceneView(node.Name, source);
                    else
                        index.Index = source;
                    break;
                case "show":
                    var showFunc = designDoc.Shows.Where(fnc => fnc.Name == node.Name).FirstOrDefault();
                    if (showFunc == null)
                        designDoc.Shows.Add(new CouchFunctionDefinition { Name = node.Name, Function = source });
                    else
                        showFunc.Function = source;
                    break;
                case "list":
                    var listFunc = designDoc.Lists.Where(fnc => fnc.Name == node.Name).FirstOrDefault();
                    if (listFunc == null)
                        designDoc.Lists.Add(new CouchFunctionDefinition { Name = node.Name, Function = source });
                    else
                        listFunc.Function = source;
                    break;
            }
        }

        private void DeleteSourceByNode(TreeNode node)
        {
            if (!(node.Tag is string))
                return;

            var designDoc = GetDesignDoc(node);
            switch (node.Tag as string)
            {
                case "reduce":
                    designDoc.Definitions.Where(view => view.Name == node.Parent.Name).FirstOrDefault().Reduce = null;
                    break;
                case "fti":
                    designDoc.RemoveLuceneViewNamed(node.Name);
                    break;
                case "show":
                    designDoc.Shows.Remove(designDoc.Shows.Where(fnc => fnc.Name == node.Name).FirstOrDefault());
                    break;
                case "list":
                    designDoc.Lists.Remove(designDoc.Lists.Where(fnc => fnc.Name == node.Name).FirstOrDefault());
                    break;
            }
        }

        private bool IsFunctionNode(TreeNode node)
        {
            if (node == null || !(node.Tag is string))
                return false;

            switch (node.Tag as string)
            {
                case "map":
                case "reduce":
                case "fti":
                case "show":
                case "list":
                    return true;
                default: 
                    return false;
            }
        }

        private void PrepopulateByType(string type)
        {
            rtSource.Text = templates[type];
        }

        /// <summary>
        /// Handles the DoubleClick event of the tvMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tvMain_DoubleClick(object sender, EventArgs e)
        {
            if (tvMain.SelectedNode == null || !IsFunctionNode(tvMain.SelectedNode))
                return;

            if (!VerifySave(true))
                return;

            _currentNode = tvMain.SelectedNode;
            rtSource.Text = GetSourceByNode(tvMain.SelectedNode);
            updateBuffer();
        }

        /// <summary>
        /// Saves the active text if necessary, and propmts for a yes/no/cancel action when <c>prompt</c> is <c>true</c>
        /// </summary>
        /// <param name="prompt">if set to <c>true</c> prompts for save confirmation.</param>
        /// <returns>whether the save occurred</returns>
        private bool VerifySave(bool prompt)
        {
            if (!HasBufferChanged())
            {
                if (_currentNode != null)
                    toolStripStatusLabel1.Text = String.Format("{0} is up to date.", _currentNode.Name);

                return true;
            }

            switch (
                prompt ?
                MessageBox.Show(this, "Do you want to save changes to " + _currentNode.FullPath + "?", "Save Changes?", MessageBoxButtons.YesNoCancel) : 
                DialogResult.Yes)
            {
                case DialogResult.Yes:
                    SetSourceByNode(_currentNode, rtSource.Text);
                    GetDesignDoc(_currentNode).Synch();
                    toolStripStatusLabel1.Text = String.Format("Saved {0}.", _currentNode.Name);
                    updateBuffer();
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

            CreateGenericFunctionNode("reduce", _contextNode, "reduce");
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
                (_contextNode.Tag as string == "views");

            addLuceneIndexToolStripMenuItem.Visible =
                (_contextNode != null) &&
                (_contextNode.Tag as string == "indices");

            addShowToolStripMenuItem.Visible =
                (_contextNode != null) &&
                (_contextNode.Tag as string == "shows");

            addListToolStripMenuItem.Visible =
                (_contextNode != null) &&
                (_contextNode.Tag as string == "lists");

            addDatabaseToolStripMenuItem.Visible =
                (_contextNode != null) &&
                (_contextNode.Tag is CouchServer);

            deleteToolStripMenuItem.Visible =
                (_contextNode != null) && (
                (_contextNode.Tag as string == "reduce") ||
                (_contextNode.Tag as string == "show") ||
                (_contextNode.Tag as string == "list") ||
                (_contextNode.Tag as string == "fti") ||
                (_contextNode.Tag is CouchDatabase) ||
                (_contextNode.Tag is GenericDesignDocument) ||
                (_contextNode.Tag is CouchViewDefinition) ||
                (_contextNode.Tag is CouchViewDefinitionBase) ||
                (_contextNode.Tag is CouchLuceneViewDefinition)
                );

            cloneViewsToolStripMenuItem.Visible =
                (_contextNode != null) &&
                (_contextNode.Tag is CouchDatabase);

            extractViewsToolStripMenuItem.Visible =
                (_contextNode != null) &&
                (_contextNode.Tag is CouchDatabase);

            importViewsToolStripMenuItem.Visible =
                (_contextNode != null) &&
                (_contextNode.Tag is CouchDatabase);

            ctxSeparator.Visible = (_contextNode != null) && (
                (_contextNode.Tag is CouchDatabase) ||
                (_contextNode.Tag is GenericDesignDocument) ||
                (_contextNode.Tag is CouchViewDefinition)
                );
            ctxSeparator2.Visible = (_contextNode != null) && (_contextNode.Tag is CouchDatabase);
        }

        private bool HasOnlyDummyNode(TreeNode node)
        {
            return node.Nodes.Count == 1 && String.Empty.Equals(node.Nodes[0].Text);
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

            if (HasOnlyDummyNode(_contextNode))
                _contextNode.Nodes.Clear();

            using (var dialog = new dlgName("View name", "New View"))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                var view = GetDesignDoc(_contextNode).AddView(dialog.EnteredName, String.Empty);

                var viewNode = CreateViewNode(_contextNode, view);
                var mapNode = CreateGenericFunctionNode("map", viewNode, "map");

                mapNode.EnsureVisible();
                _currentNode = mapNode;
                PrepopulateByType("map");
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

            if (HasOnlyDummyNode(_contextNode))
                _contextNode.Nodes.Clear();

            using (var dialog = new dlgName("Design name", "New Design"))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                var design = new GenericDesignDocument(dialog.EnteredName,
                                                     ((CouchDatabase)_contextNode.Tag));

                var designNode = CreateDesignNode(design, _contextNode);
                designNode.EnsureVisible();
            }
        }

        private void addLuceneIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_contextNode == null)
                return;

            if (HasOnlyDummyNode(_contextNode))
                _contextNode.Nodes.Clear();

            using (var dialog = new dlgName("Index name", "New Index"))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                var indexNode = CreateGenericFunctionNode("fti", _contextNode, dialog.EnteredName);
                
                indexNode.EnsureVisible();
                _currentNode = indexNode;
                PrepopulateByType("fti");
            }
        }


        private void addDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_contextNode == null)
                return;

            if (HasOnlyDummyNode(_contextNode))
                _contextNode.Nodes.Clear();

            using (var dialog = new dlgName("Database name", "New Database"))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                ((CouchServer)_contextNode.Tag).CreateDatabase(dialog.EnteredName);
                LoadServer(tstServer.Text);
            }
        }


        private void addShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_contextNode == null)
                return;

            if (HasOnlyDummyNode(_contextNode))
                _contextNode.Nodes.Clear();

            using (var dialog = new dlgName("Show name", "New Show Function"))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                GetDesignDoc(_contextNode).Shows.Add(new CouchFunctionDefinition { Name = dialog.EnteredName });
                var showNode = CreateGenericFunctionNode("show", _contextNode, dialog.EnteredName);

                showNode.EnsureVisible();
                _currentNode = showNode;
                PrepopulateByType("show");
            }
        }

        private void addListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_contextNode == null)
                return;

            if (HasOnlyDummyNode(_contextNode))
                _contextNode.Nodes.Clear();

            using (var dialog = new dlgName("List Name", "New List Function"))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                GetDesignDoc(_contextNode).Lists.Add(new CouchFunctionDefinition { Name = dialog.EnteredName });
                var listNode = CreateGenericFunctionNode("list", _contextNode, dialog.EnteredName);

                listNode.EnsureVisible();
                _currentNode = listNode;
                PrepopulateByType("list");
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_contextNode == null)
                return;

            Action deleter = null;
            string elementType = "";
            if (_contextNode.Tag is CouchDatabase)
            {
                elementType = "database";
                deleter = () =>
                              {
                                  ((CouchDatabase)_contextNode.Tag).Delete();
                                  LoadServer(tstServer.Text);
                              };
            }
            else if (_contextNode.Tag is GenericDesignDocument)
            {
                elementType = "design document";
                deleter = () =>
                {
                    ((GenericDesignDocument)_contextNode.Tag).Owner.DeleteDocument((GenericDesignDocument)_contextNode.Tag);
                    _contextNode.Remove();
                };
            }
            else if (_contextNode.Tag is CouchViewDefinition)
            {
                elementType = "view";
                deleter = () =>
                {
                    var doc = ((CouchViewDefinition)_contextNode.Tag).Doc;
                    doc.RemoveView((CouchViewDefinition)_contextNode.Tag);
                    _contextNode.Remove();
                    doc.Synch();
                };
            }
            else if (IsFunctionNode(_contextNode))
            {
                elementType = _contextNode.Tag.ToString() + " function";
                deleter = () =>
                {
                    var doc = GetDesignDoc(_contextNode);
                    DeleteSourceByNode(_contextNode);
                    _contextNode.Remove();
                    doc.Synch();
                };
            }

            if (deleter == null)
                return;

            if (MessageBox.Show(
                "Are you sure you wish to delete this " + elementType + "? If this element has child nodes, they will also be deleted.",
                "Confirm delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            deleter();
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
            settings.Font = rtSource.Font;

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

                if (settings.Font != null)
                    rtSource.Font = settings.Font;
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
            if (_currentNode == null)
                return;

            if (!VerifySave(true))
            {
                toolStripStatusLabel1.Text = "Command aborted";
                return;
            }

            var designDoc = GetDesignDoc(_currentNode);
            switch (_currentNode.Tag as string)
            {
                case "show":
                    if (String.IsNullOrEmpty(txtParams.Text))
                    {
                        MessageBox.Show("Show queries require a document id to show (\"docid[?parameter=value]\")", "Query parameter required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    Process.Start(
                        "http://" + 
                        _svr.Host + ":" + _svr.Port + "/" +
                        designDoc.Owner.Name + "/" +
                        designDoc.Id + "/_show/" +
                        _currentNode.Name + "/" +
                        txtParams.Text);
                    break;
                case "list":
                    if (String.IsNullOrEmpty(txtParams.Text))
                    {
                        MessageBox.Show("List queries require a view to show (\"viewname[?parameter=value]\")", "Query parameter required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    Process.Start(
                        "http://" +
                        _svr.Host + ":" + _svr.Port + "/" +
                        designDoc.Owner.Name + "/" +
                        designDoc.Id + "/_list/" +
                        _currentNode.Name + "/" +
                        txtParams.Text);
                    break;
                case "map":
                case "reduce":
                case "fti":
                    var viewDefinition = GetMetaElement<CouchViewDefinition>(_currentNode);
                    tvResults.Nodes.Clear();

                    if (viewDefinition == null)
                    {
                        var luceneDefinition =
                            designDoc.LuceneDefinitions.Where(view => view.Name == _currentNode.Name).
                                FirstOrDefault();
                        if (!RunLuceneQuery(luceneDefinition))
                            return;
                    }
                    else if (!RunView(viewDefinition))
                        return;

                    ShowResults();
                    break;
            }
        }

        private bool RunView(CouchViewDefinition viewDefinition)
        {
            var root = tvResults.Nodes.Add(viewDefinition.Path() + "/" + txtParams.Text);
            var viewQuery = viewDefinition.Query();
            if (!String.IsNullOrEmpty(txtParams.Text))
                foreach (var optionSet in txtParams.Text.Split('&'))
                {
                    var option = optionSet.Split('=');
                    if (option.Length != 2)
                    {
                        MessageBox.Show(txtParams.Text + " is not a valid view query string (needs to be in the form name=value[&name2=value2]).", "Invalid query", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

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
                return false;
            }

            return true;
        }

        private bool RunLuceneQuery(CouchLuceneViewDefinition luceneDefinition)
        {
            var root = tvResults.Nodes.Add(luceneDefinition.Path() + "/" + txtParams.Text);
            var luceneQuery = luceneDefinition.Query();
                
            if (String.IsNullOrEmpty(txtParams.Text) || !txtParams.Text.Contains("q="))
            {
                MessageBox.Show("FTI queries need a query string parameter (q=\"foo\")", "Query parameter required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            foreach (var optionSet in txtParams.Text.Split('&'))
            {
                var option = optionSet.Split('=');
                luceneQuery.Options[option[0]] = option[1];
            }

            try
            {
                ShowResult(root, luceneQuery.GetResult().result, null);
            }
            catch (Exception ex)
            {
                root.Nodes.Add("Error: " + ex.Message);
                MessageBox.Show(ex.ToString(), "Exception running fti", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;
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
                    var design = sourceDb.GetDocument<GenericDesignDocument>(view.Key);
                    
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
                var design = sourceDb.GetDocument<GenericDesignDocument>(view.Key);

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
                    var design = new GenericDesignDocument();
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

        private void tstServer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                cmdOpen_Click(sender, EventArgs.Empty);
        }

        private void txtParams_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                cmdRun_Click(sender, EventArgs.Empty);
        }

        private void fontsAndColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = rtSource.Font;
            if (fontDialog1.ShowDialog(this) != DialogResult.OK)
                return;

            rtSource.Font = fontDialog1.Font;

            // trigger the highlighting
            rtSource.Text += "";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog(this);
        }
    }
}