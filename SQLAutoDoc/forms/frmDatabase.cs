using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using System.Reflection;

namespace SQLAutoDoc.forms
{
    public partial class frmDatabase : Form
    {
        private enum ImageKeys
        {
            table=0,
            table_add=1,
            table_delete=2,
            table_edit=3,
            table_error=4,
            table_go=5,
            table_key=6,
            table_lightning=7,
            refresh=8
        }

        private bool m_NowRefreshing = false;

        private Guid m_DBID = Guid.Empty;

        private SQLAutoDocLib.SCAN.SCANNER m_DBScanner = null;

        private SQLAutoDocLib.SCAN.DescriptionUpdater m_Updater = null;

        private DateTime m_ScanStart;

        private frmStatus m_StatusForm = null;

        public frmDatabase()
        {
            InitializeComponent();
        }

        private void ClearTree()
        {
            tvwObject.Nodes.Clear();
            tvwObject.TreeViewNodeSorter = new NodeSorter();

            TreeNode oDatabase = tvwObject.Nodes.Add(SQLAutoDocLib.UTIL.Constants.DATABASENODE, "Database", (int)ImageKeys.table);
            oDatabase.SelectedImageIndex = (int)ImageKeys.table_go;

            TreeNode oTables = oDatabase.Nodes.Add(SQLAutoDocLib.UTIL.Constants.TABLENODE, "Tables", (int)ImageKeys.table);
            oTables.SelectedImageIndex = (int)ImageKeys.table_go;

            TreeNode oProcs = oDatabase.Nodes.Add(SQLAutoDocLib.UTIL.Constants.PROCEDURENODE, "Procedures", (int)ImageKeys.table);
            oProcs.SelectedImageIndex = (int)ImageKeys.table_go;

            TreeNode oFunctions = oDatabase.Nodes.Add(SQLAutoDocLib.UTIL.Constants.FUNCTIONNODE, "Functions", (int)ImageKeys.table);
            oFunctions.SelectedImageIndex = (int)ImageKeys.table_go;

            TreeNode oViews = oDatabase.Nodes.Add(SQLAutoDocLib.UTIL.Constants.VIEWNODE, "Views", (int)ImageKeys.table);
            oViews.SelectedImageIndex = (int)ImageKeys.table_go;
           
            TreeNode oTriggers = oDatabase.Nodes.Add(SQLAutoDocLib.UTIL.Constants.TRIGGERNODE, "Triggers", (int)ImageKeys.table);
            oTriggers.SelectedImageIndex = (int)ImageKeys.table_go;

            tvwObject.ExpandAll();
        }

        private void InitTree(string VersionID)
        {
            string sCurrentVersion = CurrentVersion();

            ClearTree();
            
            if (VersionID.Length>0)
                try
                {
                    TreeNode oDatabase = GetNodeByKey(tvwObject.Nodes, SQLAutoDocLib.UTIL.Constants.DATABASENODE);

                    //Add tables
                    SQLAutoDocLib.BLL.Table_Factory oTableFactory = new SQLAutoDocLib.BLL.Table_Factory();
                    List<SQLAutoDocLib.BLL.Table> oTableList = oTableFactory.ListAllTablesInDatabase(
                                                                                DBID:m_DBID, 
                                                                                VersionID:sCurrentVersion,
                                                                                ChangedOnly:false);

                    TreeNode oTablesNode = GetNodeByKey(oDatabase.Nodes, SQLAutoDocLib.UTIL.Constants.TABLENODE);
                    foreach (SQLAutoDocLib.BLL.Table oTable in oTableList)
                    {
                        if (chkShowChangesOnly.Checked==false || oTable.ChangedInLastScan == true)
                        {
                            TreeNode oTableNode = oTablesNode.Nodes.Add(oTable.Name, oTable.Name, imageIndex: (int)ImageKeys.table);
                            oTableNode.SelectedImageIndex= (int)ImageKeys.table_go;

                            if (oTable.ChangedInLastScan == true)
                            {
                                if (oTable.CurrentlyExists == false)
                                {
                                    oTableNode.ImageIndex = (int)ImageKeys.table_delete;
                                    oTableNode.Parent.ImageIndex = (int)ImageKeys.table_edit;
                                }
                                else
                                {
                                    oTableNode.ImageIndex = (int)ImageKeys.table_edit;
                                    oTableNode.Parent.ImageIndex = (int)ImageKeys.table_edit;
                                }
                            }
                        }
                    }

                    //Add views
                    SQLAutoDocLib.BLL.View_Factory oViewFactory = new SQLAutoDocLib.BLL.View_Factory();
                    List<SQLAutoDocLib.BLL.View> oViewList = oViewFactory.ListAllViewsInDatabase(
                                                                                DBID:m_DBID,
                                                                                VersionID: sCurrentVersion, 
                                                                                ChangedOnly: false);

                    TreeNode oViewsNode = GetNodeByKey(oDatabase.Nodes, SQLAutoDocLib.UTIL.Constants.VIEWNODE);
                    foreach (SQLAutoDocLib.BLL.View oView in oViewList)
                    {
                        if (chkShowChangesOnly.Checked == false || oView.ChangedInLastScan == true)
                        {
                            TreeNode oViewNode = oViewsNode.Nodes.Add(oView.Name, oView.Name, imageIndex: (int)ImageKeys.table);
                            oViewNode.SelectedImageIndex = (int)ImageKeys.table_go;

                            if (oView.ChangedInLastScan == true)
                            {
                                if (oView.CurrentlyExists == false)
                                {
                                    oViewNode.ImageIndex = (int)ImageKeys.table_delete;
                                    oViewNode.Parent.ImageIndex = (int)ImageKeys.table_edit;
                                }
                                else
                                {
                                    oViewNode.ImageIndex = (int)ImageKeys.table_edit;
                                    oViewNode.Parent.ImageIndex = (int)ImageKeys.table_edit;
                                }
                            }
                        }
                    }

                    //Add procs
                    SQLAutoDocLib.BLL.Procedure_Factory oProcFactory = new SQLAutoDocLib.BLL.Procedure_Factory();
                    List<SQLAutoDocLib.BLL.Procedure> oProcList = oProcFactory.ListAllProceduresInDatabase(
                                                                                    DBID:m_DBID, 
                                                                                    VersionID: sCurrentVersion, 
                                                                                    ChangedOnly: false);

                    TreeNode oProcsNode = GetNodeByKey(oDatabase.Nodes, SQLAutoDocLib.UTIL.Constants.PROCEDURENODE);
                    foreach (SQLAutoDocLib.BLL.Procedure oProc in oProcList)
                    {
                        if (chkShowChangesOnly.Checked == false || oProc.ChangedInLastScan == true)
                        {
                            TreeNode oProcNode = oProcsNode.Nodes.Add(oProc.Name, oProc.Name, imageIndex: (int)ImageKeys.table);
                            oProcNode.SelectedImageIndex = (int)ImageKeys.table_go;

                            if (oProc.ChangedInLastScan == true)
                            {
                                if (oProc.CurrentlyExists == false)
                                {
                                    oProcNode.ImageIndex = (int)ImageKeys.table_delete;
                                    oProcNode.Parent.ImageIndex = (int)ImageKeys.table_edit;
                                }
                                else
                                {
                                    oProcNode.ImageIndex = (int)ImageKeys.table_edit;
                                    oProcNode.Parent.ImageIndex = (int)ImageKeys.table_edit;
                                }
                            }
                        }
                    }

                    //Add functions
                    SQLAutoDocLib.BLL.Function_Factory oFunctionFactory = new SQLAutoDocLib.BLL.Function_Factory();
                    List<SQLAutoDocLib.BLL.Function> oFunctionList = oFunctionFactory.ListAllFunctionsInDatabase(
                                                                                            DBID:m_DBID, 
                                                                                            VersionID: sCurrentVersion, 
                                                                                            ChangedOnly: false);

                    TreeNode oFunctionsNode = GetNodeByKey(oDatabase.Nodes, SQLAutoDocLib.UTIL.Constants.FUNCTIONNODE);
                    foreach (SQLAutoDocLib.BLL.Function oFunction in oFunctionList)
                    {
                        if (chkShowChangesOnly.Checked == false || oFunction.ChangedInLastScan == true)
                        {
                            TreeNode oFunctionNode = oFunctionsNode.Nodes.Add(oFunction.Name, oFunction.Name, imageIndex: (int)ImageKeys.table);
                            oFunctionsNode.SelectedImageIndex = (int)ImageKeys.table_go;

                            if (oFunction.ChangedInLastScan == true)
                            {
                                if (oFunction.CurrentlyExists == false)
                                {
                                    oFunctionsNode.ImageIndex = (int)ImageKeys.table_delete;
                                    oFunctionsNode.Parent.ImageIndex = (int)ImageKeys.table_edit;
                                }
                                else
                                {
                                    oFunctionsNode.ImageIndex = (int)ImageKeys.table_edit;
                                    oFunctionNode.Parent.ImageIndex = (int)ImageKeys.table_edit;
                                }
                            }
                        }
                    }

                    //Add triggers
                    SQLAutoDocLib.BLL.Trigger_Factory oTriggerFactory = new SQLAutoDocLib.BLL.Trigger_Factory();
                    List<SQLAutoDocLib.BLL.Trigger> oTriggerList = oTriggerFactory.ListAllTriggersInDatabase(
                                                                                        DBID:m_DBID, 
                                                                                        VersionID: sCurrentVersion, 
                                                                                        ChangedOnly: false);

                    TreeNode oTriggersNode = GetNodeByKey(oDatabase.Nodes, SQLAutoDocLib.UTIL.Constants.TRIGGERNODE);
                    foreach (SQLAutoDocLib.BLL.Trigger oTrigger in oTriggerList)
                    {
                        if (chkShowChangesOnly.Checked == false || oTrigger.ChangedInLastScan == true)
                        {
                            TreeNode oTriggerNode = oTriggersNode.Nodes.Add(oTrigger.Name, oTrigger.Name, imageIndex: (int)ImageKeys.table);
                            oTriggersNode.SelectedImageIndex = (int)ImageKeys.table_go;

                            if (oTrigger.ChangedInLastScan == true)
                            {
                                if (oTrigger.CurrentlyExists == false)
                                {
                                    oTriggersNode.ImageIndex = (int)ImageKeys.table_delete;
                                    oTriggersNode.Parent.ImageIndex = (int)ImageKeys.table_edit;
                                }
                                else
                                {
                                    oTriggersNode.ImageIndex = (int)ImageKeys.table_edit;
                                    oTriggersNode.Parent.ImageIndex = (int)ImageKeys.table_edit;
                                }
                            }
                        }
                    }            
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
        }

        private TreeNode GetNodeByKey(TreeNodeCollection oNodes,string Key)
        {
            
            TreeNode oResult = null;

            oResult = oNodes[oNodes.IndexOfKey(Key)];

            return oResult;
        }
        
        private void ShowCurrentNodeDetail(TreeNode e)
        {
            if (e!=null)
                if (e.Parent == null)
                {
                    //this is the database node
                    HideShowCaptions(false);
                }
                else if (e.Parent.Name == SQLAutoDocLib.UTIL.Constants.DATABASENODE)
                {
                    //this is a folder node
                    if (e.Name==SQLAutoDocLib.UTIL.Constants.TABLENODE)
                        HideShowCaptions(false);
                       
                    else if (e.Name==SQLAutoDocLib.UTIL.Constants.VIEWNODE)
                        HideShowCaptions(false);

                    else if (e.Name==SQLAutoDocLib.UTIL.Constants.TRIGGERNODE)
                        HideShowCaptions(false);

                    else if (e.Name==SQLAutoDocLib.UTIL.Constants.PROCEDURENODE)
                        HideShowCaptions(false);

                    else if (e.Name==SQLAutoDocLib.UTIL.Constants.FUNCTIONNODE)
                        HideShowCaptions(false);
                }
                else
                {
                    //this is an object node.
                    HideShowCaptions(true);

                    if (e.Parent.Name==SQLAutoDocLib.UTIL.Constants.TABLENODE)
                            ShowTableDetail(e.Name);

                    else if (e.Parent.Name==SQLAutoDocLib.UTIL.Constants.VIEWNODE)
                            ShowViewDetail(e.Name);

                    else if (e.Parent.Name==SQLAutoDocLib.UTIL.Constants.TRIGGERNODE)
                            ShowTriggerDetail(e.Name);

                    else if (e.Parent.Name==SQLAutoDocLib.UTIL.Constants.PROCEDURENODE)
                            ShowProcDetail(e.Name);

                    else if (e.Parent.Name==SQLAutoDocLib.UTIL.Constants.FUNCTIONNODE)
                            ShowFunctionDetail(e.Name);
    
                }
        }

        private void ShowVersions()
        {
            m_NowRefreshing = true;

            SQLAutoDocLib.BLL.Version_Factory oFactory = new SQLAutoDocLib.BLL.Version_Factory();

            cmbVersion.DataSource = oFactory.ListAllVersionsInDatabase(m_DBID);

            m_NowRefreshing = false;
        }

        private void ShowTableDetail(string TableName)
        {
            classes.UIManager.ShowTableDetail(
                                    oParent: this,
                                    DBID: m_DBID,
                                    VersionID: cmbVersion.SelectedValue.ToString(),
                                    TableName: TableName);
        }

        private void ShowViewDetail(string ViewName)
        {
            classes.UIManager.ShowViewDetail(
                                    oParent: this,
                                    DBID: m_DBID,
                                    VersionID: cmbVersion.SelectedValue.ToString(),
                                    ViewName: ViewName);
        }

        private void ShowTriggerDetail(string TriggerName)
        {
            classes.UIManager.ShowTriggerDetail(
                                    oParent: this,
                                    DBID: m_DBID,
                                    VersionID: cmbVersion.SelectedValue.ToString(),
                                    TriggerName: TriggerName);
        }

        private void ShowProcDetail(string ProcName)
        {
            classes.UIManager.ShowProcDetail(
                                    oParent: this,
                                    DBID: m_DBID,
                                    VersionID: cmbVersion.SelectedValue.ToString(),
                                    ProcName: ProcName);
        }

        private void ShowFunctionDetail(string FunctionName)
        {
            classes.UIManager.ShowFunctionDetail(
                                    oParent: this,
                                    DBID: m_DBID,
                                    VersionID: cmbVersion.SelectedValue.ToString(),
                                    FunctionName: FunctionName);
        }
        
        private void ShowCurrentVersion()
        {
            ShowCurrentNodeDetail(tvwObject.SelectedNode);
        }

        private void ResetIcons(TreeNode oRoot)
        {
            oRoot.ImageIndex = (int)ImageKeys.table;

            foreach (TreeNode oNode in oRoot.Nodes)
                ResetIcons(oNode);
        }

        private void HideShowCaptions(bool bVisible)
        {
            lblNameCaption.Visible = bVisible;
            lblName.Visible = bVisible;

            lblFirstDateCaption.Visible = bVisible;
            lblFirstDate.Visible = bVisible;

            lblLastDateCaption.Visible = bVisible;
            lblLastDate.Visible = bVisible;

            lblDesc.Visible = bVisible;
            txtDesc.Visible = bVisible;

            html.Visible = bVisible;
        }

        private string GetMetadataRepositoryConnection()
        {
            string MetaDataConnection="";

            MetaDataConnection=
                    MetaDataConnection = System.Configuration.ConfigurationManager.ConnectionStrings["MetaData"].ConnectionString;

            return MetaDataConnection;
        }

        private bool showDSNDialog()
        {
            bool bNewDSNSelected = false;

            frmDSN oDSN = new frmDSN();

            oDSN.SelectedDSN = SQLAutoDocLib.UTIL.DSNConnectionSchemaManager.DSN;

            if (oDSN.ShowDialog() == DialogResult.OK)
            {
                SQLAutoDocLib.UTIL.DSNConnectionSchemaManager.DSN = oDSN.SelectedDSN;

                tsDSN.Text = "DSN=" + SQLAutoDocLib.UTIL.DSNConnectionSchemaManager.DSN;
                tsServer.Text = "Server: " + SQLAutoDocLib.UTIL.DSNConnectionSchemaManager.SQLServer;
                tsDatabase.Text = "Database: " + SQLAutoDocLib.UTIL.DSNConnectionSchemaManager.SQLDatabase;
                tsUser.Text = "User:" + SQLAutoDocLib.UTIL.ConnectionSchemaManager.UserID;

                setDBScanner();

                bNewDSNSelected = true;
            }

            return bNewDSNSelected;
        }

        private void setDBScanner()
        {
            m_DBScanner = new SQLAutoDocLib.SCAN.SCANNER(this, sConnectionString: SQLAutoDocLib.UTIL.DSNConnectionSchemaManager.ConnectionString);
            m_DBID = m_DBScanner.DBID;

            m_Updater = new SQLAutoDocLib.SCAN.DescriptionUpdater(HostForm: this, DBID: m_DBID);

            /* Scanning events */
            m_DBScanner.ScanDatabaseBegin += new SQLAutoDocLib.SCAN.SCANNER.ScanDatabaseBeginEvent(ScanDatabaseBeginEvent_Handler);
            m_DBScanner.ScanDatabaseEnd += new SQLAutoDocLib.SCAN.SCANNER.ScanDatabaseEndEvent(ScanDatabaseEndEvent_Handler);

            m_DBScanner.ScanObjectTypeBegin += new SQLAutoDocLib.SCAN.SCANNER.ScanObjectTypeBeginEvent(ScanObjectBeginEvent_Handler);
            m_DBScanner.ScanObjectTypeEnd += new SQLAutoDocLib.SCAN.SCANNER.ScanObjectTypeEndEvent(ScanObjectEndEvent_Handler);

            m_DBScanner.ScanObjectBegin += new SQLAutoDocLib.SCAN.SCANNER.ScanObjectBeginEvent(ScanObjectBegin_Handler);
            m_DBScanner.ScanObjectChanged += new SQLAutoDocLib.SCAN.SCANNER.ScanObjectChangedEvent(ScanObjectChanged_Handler);
            m_DBScanner.ScanObjectNotFound += new SQLAutoDocLib.SCAN.SCANNER.ScanObjectNotFoundEvent(ScanObjectNotFound_Handler);
            m_DBScanner.ScanObjectDeleted += new SQLAutoDocLib.SCAN.SCANNER.ScanObjectDeletedEvent(ScanObjectDeleted_Handler);
            m_DBScanner.ScanObjectCount += new SQLAutoDocLib.SCAN.SCANNER.ScanCountOfObjectsEvent(ScanObjectCount_Handler);

            /* Description update events */
            m_Updater.DescriptionUpdateBegin += new SQLAutoDocLib.SCAN.DescriptionUpdater.DescriptionUpdateBeginEvent(DescriptionUpdateBeginEvent_Handler);
            m_Updater.DescriptionUpdateEnd += new SQLAutoDocLib.SCAN.DescriptionUpdater.DescriptionUpdateEndEvent(DescriptionUpdateEndEvent_Handler);

            m_Updater.ObjectDescriptionUpdate += new SQLAutoDocLib.SCAN.DescriptionUpdater.ObjectDescriptionUpdateEvent(ObjectDescriptionUpdateEvent_Handler);
            m_Updater.TypeDescriptionUpdate += new SQLAutoDocLib.SCAN.DescriptionUpdater.TypeDescriptionUpdateEvent(TypeDescriptionUpdateEvent_Handler);

            ShowVersions();

            ClearTree();
            InitTree(CurrentVersion());
        }

        private string CurrentVersion()
        {
            return (cmbVersion.SelectedValue == null) ? "" : cmbVersion.SelectedValue.ToString();
        }

        private void LogToStatus(string StatusText)
        {
            m_StatusForm.AddEntry(StatusText: StatusText);
        }

        private void ShowStatusForm()
        {
            if (m_StatusForm == null)
            {
                m_StatusForm = new frmStatus();
            }

            m_StatusForm.Clear();
            m_StatusForm.Show(owner: this);
            m_StatusForm.TopMost = true;
        }

        private void StartScan()
        {
            TreeNode oDatabase = GetNodeByKey(tvwObject.Nodes, SQLAutoDocLib.UTIL.Constants.DATABASENODE);
            ResetIcons(oDatabase);

            ShowStatusForm();

            m_DBScanner.Scan();
        }

        private void UpdateDescriptions()
        {
            m_Updater.Update();
            MessageBox.Show("Descriptions updated.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Create a node sorter that implements the IComparer interface. 
        internal class NodeSorter : IComparer
        {
            // Compare the length of the strings, or the strings 
            // themselves, if they are the same length. 
            public int Compare(object x, object y)
            {
                TreeNode tx = x as TreeNode;
                TreeNode ty = y as TreeNode;

                // Compare the length of the strings, returning the difference. 
                //if (tx.Text.Length != ty.Text.Length)
                //    return tx.Text.Length - ty.Text.Length;

                // If they are the same length, call Compare. 
                return string.Compare(tx.Text, ty.Text);
            }

        }

        private void ShowDesc()
        {
            string VersionID=cmbVersion.SelectedValue.ToString();

            TreeNode CurrentNode = tvwObject.SelectedNode;

            if (CurrentNode.Parent != null)
            {
                if (CurrentNode.Parent.Name == SQLAutoDocLib.UTIL.Constants.TABLENODE)
                {
                    SQLAutoDocLib.BLL.Table_Factory oFactory = new SQLAutoDocLib.BLL.Table_Factory();
                    SQLAutoDocLib.BLL.Table oTable = oFactory.GetTableByName(DBID: m_DBID, TableName: CurrentNode.Name, VersionID: VersionID);

                    frmDesc oForm = new frmDesc(
                                    ObjectName: CurrentNode.Name,
                                    ObjectDesc:oTable.Description,
                                    ObjectType: SQLAutoDocLib.UTIL.ObjectType.Table);

                    if (oForm.ShowDialog() == DialogResult.OK)
                    {
                        oTable.Load();
                        oTable.Description = oForm.Desc;
                        oTable.Save();

                        txtDesc.Text = oForm.Desc;
                    }
                }

                else if (CurrentNode.Parent.Name == SQLAutoDocLib.UTIL.Constants.VIEWNODE)
                {
                    SQLAutoDocLib.BLL.View_Factory oFactory = new SQLAutoDocLib.BLL.View_Factory();
                    SQLAutoDocLib.BLL.View oView = oFactory.GetViewByName(DBID: m_DBID, ViewName: CurrentNode.Name, VersionID: VersionID);

                    frmDesc oForm = new frmDesc(
                                    ObjectName: CurrentNode.Name,
                                    ObjectDesc: oView.Description,
                                    ObjectType: SQLAutoDocLib.UTIL.ObjectType.View);

                    if (oForm.ShowDialog() == DialogResult.OK)
                    {
                        oView.Load();
                        oView.Description = oForm.Desc;
                        oView.Save();

                        txtDesc.Text = oForm.Desc;
                    }
                }

                else if (CurrentNode.Parent.Name == SQLAutoDocLib.UTIL.Constants.TRIGGERNODE)
                {
                    SQLAutoDocLib.BLL.Trigger_Factory oFactory = new SQLAutoDocLib.BLL.Trigger_Factory();
                    SQLAutoDocLib.BLL.Trigger oTrigger = oFactory.GetTriggerByName(DBID: m_DBID, TriggerName: CurrentNode.Name, VersionID: VersionID);

                    frmDesc oForm = new frmDesc(
                                    ObjectName: CurrentNode.Name,
                                    ObjectDesc: oTrigger.Description,
                                    ObjectType: SQLAutoDocLib.UTIL.ObjectType.Trigger);

                    if (oForm.ShowDialog() == DialogResult.OK)
                    {
                        oTrigger.Load();
                        oTrigger.Description = oForm.Desc;
                        oTrigger.Save();

                        txtDesc.Text = oForm.Desc;
                    }
                }

                else if (CurrentNode.Parent.Name == SQLAutoDocLib.UTIL.Constants.PROCEDURENODE)
                {
                    SQLAutoDocLib.BLL.Procedure_Factory oFactory = new SQLAutoDocLib.BLL.Procedure_Factory();
                    SQLAutoDocLib.BLL.Procedure oProcedure = oFactory.GetProcedureByName(DBID: m_DBID, ProcedureName: CurrentNode.Name, VersionID: VersionID);

                    frmDesc oForm = new frmDesc(
                                    ObjectName: CurrentNode.Name,
                                    ObjectDesc: oProcedure.Description,
                                    ObjectType: SQLAutoDocLib.UTIL.ObjectType.Procedure);

                    if (oForm.ShowDialog() == DialogResult.OK)
                    {
                        oProcedure.Load();
                        oProcedure.Description = oForm.Desc;
                        oProcedure.Save();

                        txtDesc.Text = oForm.Desc;
                    }
                }

                else if (CurrentNode.Parent.Name == SQLAutoDocLib.UTIL.Constants.FUNCTIONNODE)
                {
                    SQLAutoDocLib.BLL.Function_Factory oFactory = new SQLAutoDocLib.BLL.Function_Factory();
                    SQLAutoDocLib.BLL.Function oFunction = oFactory.GetFunctionByName(DBID: m_DBID, FunctionName: CurrentNode.Name, VersionID: VersionID);

                    frmDesc oForm = new frmDesc(
                                    ObjectName: CurrentNode.Name,
                                    ObjectDesc: oFunction.Description,
                                    ObjectType: SQLAutoDocLib.UTIL.ObjectType.Function);

                    if (oForm.ShowDialog() == DialogResult.OK)
                    {
                        oFunction.Load();
                        oFunction.Description = oForm.Desc;
                        oFunction.Save();

                        txtDesc.Text = oForm.Desc;
                    }
                }
            }
        }

        private void ShowThisVersionChanges()
        {
            frmDiff oDiff = new frmDiff(DBID:m_DBID,sVersion:cmbVersion.SelectedValue.ToString());
            oDiff.ShowVersion();
        }

        #region Events

        /* Scanning events */
        public void ScanDatabaseBeginEvent_Handler(DateTime e)
        {
            tvwObject.Nodes[SQLAutoDocLib.UTIL.Constants.DATABASENODE].ImageIndex = (int)ImageKeys.refresh;
            LogToStatus(StatusText: "Scanning Database");
            m_ScanStart = e;
        }

        public void ScanDatabaseEndEvent_Handler(DateTime e)
        {
            tvwObject.Nodes[SQLAutoDocLib.UTIL.Constants.DATABASENODE].ImageIndex = (int)ImageKeys.table;

            ShowVersions();

            StringBuilder sMessage = new StringBuilder("Completed Database Scan.");

            if (m_DBScanner.FoundDatabaseChanges)
                sMessage.Append(" Changes found.");
            else
                sMessage.Append(" No changes found.");
            
            sMessage.Append(" Elapsed seconds: " + Math.Round(e.Subtract(m_ScanStart).TotalSeconds,3).ToString());

            LogToStatus(StatusText:  sMessage.ToString());

            tsProgress.Value = 0;
            tsProgress.Visible = false;
        }

        public void ScanObjectBeginEvent_Handler(string type, DateTime e)
        {
            tvwObject.Nodes[SQLAutoDocLib.UTIL.Constants.DATABASENODE].Nodes[type].ImageIndex = (int)ImageKeys.refresh;

            LogToStatus(StatusText: type + " now scanning");
        }

        public void ScanObjectEndEvent_Handler(string type, DateTime e)
        {
            tvwObject.Nodes[SQLAutoDocLib.UTIL.Constants.DATABASENODE].Nodes[type].ImageIndex = (int)ImageKeys.table;

            LogToStatus(StatusText: type + " scan complete");
        }

        public void ScanObjectBegin_Handler(string type, string name, DateTime e)
        {
            tsProgress.Value += 1;
        }

        public void ScanObjectChanged_Handler(string type, string name, DateTime e)
        {
            TreeNode oParentNode = tvwObject.Nodes[SQLAutoDocLib.UTIL.Constants.DATABASENODE].Nodes[type];

            if (oParentNode.Nodes.ContainsKey(name))
            {
                oParentNode.Nodes[name].ImageIndex = (int)ImageKeys.table_edit;
            }
            else
            {
                //create child node item
                TreeNode oChildNode = oParentNode.Nodes.Add(name, name, (int)ImageKeys.table_edit);
            }

            LogToStatus(StatusText: " **** " + type + " changed: " + name);
        }

        public void ScanObjectCount_Handler(string type, int count)
        {
            tsProgress.Maximum = count;
            tsProgress.Value = 0;
            tsProgress.ToolTipText = "Now scanning " + type;
            tsProgress.Visible = true;
        }

        /// <summary>
        /// Event fired when a new (not previously existing) database object is found.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="e"></param>
        public void ScanObjectNotFound_Handler(string type, string name, DateTime e)
        {
            /* sort newly added node.
             * http://social.msdn.microsoft.com/forums/en-US/winforms/thread/ea680ff3-435b-489b-9446-300c36095719/
             * */

            TreeNode oParentNode = tvwObject.Nodes[SQLAutoDocLib.UTIL.Constants.DATABASENODE].Nodes[type];

            TreeNode oNewObject = oParentNode.Nodes.Add(name, name, (int)ImageKeys.table_edit);
            oParentNode.ImageIndex = (int)ImageKeys.table_edit;
            tvwObject.Nodes[SQLAutoDocLib.UTIL.Constants.DATABASENODE].ImageIndex = (int)ImageKeys.table_edit;

            LogToStatus(StatusText: " **** " + type + " discovered: " + name);
        }

        public void ScanObjectDeleted_Handler(string type, string name, DateTime e)
        {
            TreeNode oParentNode = tvwObject.Nodes[SQLAutoDocLib.UTIL.Constants.DATABASENODE].Nodes[type];

            TreeNode oNewObject = oParentNode.Nodes.Add(name, name, (int)ImageKeys.table_delete);
            oParentNode.ImageIndex = (int)ImageKeys.table_edit;
            tvwObject.Nodes[SQLAutoDocLib.UTIL.Constants.DATABASENODE].ImageIndex = (int)ImageKeys.table_edit;

            LogToStatus(StatusText: " **** " + type + " deleted: " + name);
        }

        /* Description events */
        public void DescriptionUpdateBeginEvent_Handler(DateTime e)
        {
            LogToStatus(StatusText: "Beginning description update...");
        }

        public void DescriptionUpdateEndEvent_Handler(DateTime e)
        {
            LogToStatus(StatusText: "Description update complete...");
        }

        public void TypeDescriptionUpdateEvent_Handler(string type, DateTime e)
        {
            LogToStatus(StatusText: "Updating " + type + " descriptions...");
        }

        public void ObjectDescriptionUpdateEvent_Handler(string type, string name, DateTime e)
        {
            LogToStatus(StatusText:  "Updating " + type + "." + name + " descriptions...");
        }

        /* Click handling events */
        private void btnDetail_Click(object sender, EventArgs e)
        {
            TreeNode CurrentNode = tvwObject.SelectedNode;

            if (CurrentNode.Parent != null)
            {
                frmDetail oForm = null;

                if (CurrentNode.Parent.Name == SQLAutoDocLib.UTIL.Constants.TABLENODE)
                {
                    oForm = new frmDetail();
                    classes.UIManager.ShowTableDetail(
                                            oParent: oForm,
                                            DBID: m_DBID,
                                            VersionID: cmbVersion.SelectedValue.ToString(),
                                            TableName: CurrentNode.Name);
                }

                else if (CurrentNode.Parent.Name == SQLAutoDocLib.UTIL.Constants.VIEWNODE)
                {
                    oForm = new frmDetail();
                    classes.UIManager.ShowViewDetail(
                                            oParent: oForm,
                                            DBID: m_DBID,
                                            VersionID: cmbVersion.SelectedValue.ToString(),
                                            ViewName: CurrentNode.Name);
                }

                else if (CurrentNode.Parent.Name == SQLAutoDocLib.UTIL.Constants.TRIGGERNODE)
                {
                    oForm = new frmDetail();
                    classes.UIManager.ShowTriggerDetail(
                                            oParent: oForm,
                                            DBID: m_DBID,
                                            VersionID: cmbVersion.SelectedValue.ToString(),
                                            TriggerName: CurrentNode.Name);
                }

                else if (CurrentNode.Parent.Name == SQLAutoDocLib.UTIL.Constants.PROCEDURENODE)
                {
                    oForm = new frmDetail();
                    classes.UIManager.ShowProcDetail(
                                            oParent: oForm,
                                            DBID: m_DBID,
                                            VersionID: cmbVersion.SelectedValue.ToString(),
                                            ProcName: CurrentNode.Name);
                }

                else if (CurrentNode.Parent.Name == SQLAutoDocLib.UTIL.Constants.FUNCTIONNODE)
                {
                    oForm = new frmDetail();
                    classes.UIManager.ShowFunctionDetail(
                                            oParent: oForm,
                                            DBID: m_DBID,
                                            VersionID: cmbVersion.SelectedValue.ToString(),
                                            FunctionName: CurrentNode.Name);
                }

                if (oForm != null)
                {
                    oForm.Text = CurrentNode.Parent.Name + ": " + CurrentNode.Name;
                    oForm.Show();
                }
            }
        }

        private void txtDesc_DoubleClick(object sender, EventArgs e)
        {
            ShowDesc();
        }

        private void tsUpdateDescriptions_Click(object sender, EventArgs e)
        {
            UpdateDescriptions();
        }

        private void cmbVersion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!m_NowRefreshing)
                InitTree(cmbVersion.SelectedValue.ToString());
        }

        private void chkShowChangesOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_NowRefreshing)
                InitTree(cmbVersion.SelectedValue.ToString());
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            StartScan();
        }

        private void tvwObject_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowCurrentNodeDetail(e.Node);
        }

        private void setDSNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showDSNDialog();
        }

        /* resize & form events */
        private void frmDatabase_Resize(object sender, EventArgs e)
        {

        }

        private void pInfo_Resize(object sender, EventArgs e)
        {
            if (pInfo.Width >= 438)
                txtDesc.Width = pInfo.Width - 243;
            else
                txtDesc.Width = 195;
        }

        private void frmDatabase_Load(object sender, EventArgs e)
        {
            try
            {
                html.Navigate("about:blank");

                this.Text =
                    String.Format("SQLAutoDoc Version {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

                SQLAutoDocLib.UTIL.ConnectionSchemaManager.ConnectionString = GetMetadataRepositoryConnection();

                showDSNDialog();
            }
            catch (Exception oEX)
            {
                MessageBox.Show("Error in form_load: " + oEX.Message);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ShowCurrentVersion();
        }

        private void btnComment_Click(object sender, EventArgs e)
        {
            ShowDesc();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void scanToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StartScan();
        }

        private void txtDesc_TextChanged(object sender, EventArgs e)
        {

        }  

        private void cmbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCurrentVersion();
        }

               private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAbout oAbout = new frmAbout();
            oAbout.ShowDialog();
        }

        private void publishDescriptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateDescriptions();
        }
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ShowThisVersionChanges();
        }

        private void showChangesInThisVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowThisVersionChanges();
        }

        #endregion
    }
}
