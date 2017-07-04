namespace SQLAutoDoc.forms
{
    partial class frmDatabase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDatabase));
            this.ilTreeview = new System.Windows.Forms.ImageList(this.components);
            this.ilToolbar = new System.Windows.Forms.ImageList(this.components);
            this.tsStatus = new System.Windows.Forms.StatusStrip();
            this.tsMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsServer = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsDatabase = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsDSN = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDSNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.publishDescriptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.spSplit = new System.Windows.Forms.SplitContainer();
            this.pList = new System.Windows.Forms.TableLayoutPanel();
            this.tsTools = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnComment = new System.Windows.Forms.ToolStripButton();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.btnDetail = new System.Windows.Forms.ToolStripButton();
            this.tsUpdateDescriptions = new System.Windows.Forms.ToolStripButton();
            this.btnScan = new System.Windows.Forms.ToolStripButton();
            this.tvwObject = new System.Windows.Forms.TreeView();
            this.tlDetail = new System.Windows.Forms.TableLayoutPanel();
            this.pDetailVersion = new System.Windows.Forms.Panel();
            this.chkShowChangesOnly = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbVersion = new System.Windows.Forms.ComboBox();
            this.pInfo = new System.Windows.Forms.Panel();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.lblLastDate = new System.Windows.Forms.Label();
            this.lblFirstDate = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblLastDateCaption = new System.Windows.Forms.Label();
            this.lblFirstDateCaption = new System.Windows.Forms.Label();
            this.lblNameCaption = new System.Windows.Forms.Label();
            this.html = new System.Windows.Forms.WebBrowser();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.showChangesInThisVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsStatus.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spSplit)).BeginInit();
            this.spSplit.Panel1.SuspendLayout();
            this.spSplit.Panel2.SuspendLayout();
            this.spSplit.SuspendLayout();
            this.pList.SuspendLayout();
            this.tsTools.SuspendLayout();
            this.tlDetail.SuspendLayout();
            this.pDetailVersion.SuspendLayout();
            this.pInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // ilTreeview
            // 
            this.ilTreeview.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTreeview.ImageStream")));
            this.ilTreeview.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTreeview.Images.SetKeyName(0, "table.png");
            this.ilTreeview.Images.SetKeyName(1, "table_add.png");
            this.ilTreeview.Images.SetKeyName(2, "table_delete.png");
            this.ilTreeview.Images.SetKeyName(3, "table_edit.png");
            this.ilTreeview.Images.SetKeyName(4, "table_error.png");
            this.ilTreeview.Images.SetKeyName(5, "table_go.png");
            this.ilTreeview.Images.SetKeyName(6, "table_key.png");
            this.ilTreeview.Images.SetKeyName(7, "table_lightning.png");
            this.ilTreeview.Images.SetKeyName(8, "database_refresh.png");
            // 
            // ilToolbar
            // 
            this.ilToolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilToolbar.ImageStream")));
            this.ilToolbar.TransparentColor = System.Drawing.Color.Transparent;
            this.ilToolbar.Images.SetKeyName(0, "tb_refresh.jpg");
            this.ilToolbar.Images.SetKeyName(1, "tb_save.jpg");
            this.ilToolbar.Images.SetKeyName(2, "tb_search.jpg");
            this.ilToolbar.Images.SetKeyName(3, "tb_viewdetail.jpg");
            this.ilToolbar.Images.SetKeyName(4, "cog_go.png");
            // 
            // tsStatus
            // 
            this.tsStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMessage,
            this.tsUser,
            this.tsServer,
            this.tsDatabase,
            this.tsDSN,
            this.tsProgress});
            this.tsStatus.Location = new System.Drawing.Point(0, 553);
            this.tsStatus.Name = "tsStatus";
            this.tsStatus.Size = new System.Drawing.Size(671, 22);
            this.tsStatus.TabIndex = 0;
            this.tsStatus.Text = "statusStrip1";
            // 
            // tsMessage
            // 
            this.tsMessage.Name = "tsMessage";
            this.tsMessage.Size = new System.Drawing.Size(0, 17);
            // 
            // tsUser
            // 
            this.tsUser.Name = "tsUser";
            this.tsUser.Size = new System.Drawing.Size(39, 17);
            this.tsUser.Text = "tsUser";
            // 
            // tsServer
            // 
            this.tsServer.Name = "tsServer";
            this.tsServer.Size = new System.Drawing.Size(48, 17);
            this.tsServer.Text = "tsServer";
            // 
            // tsDatabase
            // 
            this.tsDatabase.Name = "tsDatabase";
            this.tsDatabase.Size = new System.Drawing.Size(64, 17);
            this.tsDatabase.Text = "tsDatabase";
            // 
            // tsDSN
            // 
            this.tsDSN.Name = "tsDSN";
            this.tsDSN.Size = new System.Drawing.Size(39, 17);
            this.tsDSN.Text = "tsDSN";
            // 
            // tsProgress
            // 
            this.tsProgress.Name = "tsProgress";
            this.tsProgress.Size = new System.Drawing.Size(100, 16);
            this.tsProgress.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.editToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(671, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setDSNToolStripMenuItem,
            this.scanToolStripMenuItem1,
            this.publishDescriptionsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // setDSNToolStripMenuItem
            // 
            this.setDSNToolStripMenuItem.Name = "setDSNToolStripMenuItem";
            this.setDSNToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.setDSNToolStripMenuItem.Text = "Set &DSN";
            this.setDSNToolStripMenuItem.Click += new System.EventHandler(this.setDSNToolStripMenuItem_Click);
            // 
            // scanToolStripMenuItem1
            // 
            this.scanToolStripMenuItem1.Name = "scanToolStripMenuItem1";
            this.scanToolStripMenuItem1.Size = new System.Drawing.Size(181, 22);
            this.scanToolStripMenuItem1.Text = "&Scan";
            this.scanToolStripMenuItem1.Click += new System.EventHandler(this.scanToolStripMenuItem1_Click);
            // 
            // publishDescriptionsToolStripMenuItem
            // 
            this.publishDescriptionsToolStripMenuItem.Name = "publishDescriptionsToolStripMenuItem";
            this.publishDescriptionsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.publishDescriptionsToolStripMenuItem.Text = "&Publish Descriptions";
            this.publishDescriptionsToolStripMenuItem.Click += new System.EventHandler(this.publishDescriptionsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.detailToolStripMenuItem,
            this.filterToolStripMenuItem,
            this.showChangesInThisVersionToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.refreshToolStripMenuItem.Text = "&Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // detailToolStripMenuItem
            // 
            this.detailToolStripMenuItem.Name = "detailToolStripMenuItem";
            this.detailToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.detailToolStripMenuItem.Text = "&Detail";
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.filterToolStripMenuItem.Text = "&Filter";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scanToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            this.scanToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.scanToolStripMenuItem.Text = "&Description";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem1.Text = "&About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // spSplit
            // 
            this.spSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spSplit.Location = new System.Drawing.Point(0, 24);
            this.spSplit.Name = "spSplit";
            // 
            // spSplit.Panel1
            // 
            this.spSplit.Panel1.Controls.Add(this.pList);
            // 
            // spSplit.Panel2
            // 
            this.spSplit.Panel2.Controls.Add(this.tlDetail);
            this.spSplit.Size = new System.Drawing.Size(671, 529);
            this.spSplit.SplitterDistance = 223;
            this.spSplit.TabIndex = 3;
            // 
            // pList
            // 
            this.pList.ColumnCount = 1;
            this.pList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pList.Controls.Add(this.tsTools, 0, 0);
            this.pList.Controls.Add(this.tvwObject, 0, 1);
            this.pList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pList.Location = new System.Drawing.Point(0, 0);
            this.pList.Name = "pList";
            this.pList.RowCount = 2;
            this.pList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pList.Size = new System.Drawing.Size(223, 529);
            this.pList.TabIndex = 0;
            // 
            // tsTools
            // 
            this.tsTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnComment,
            this.btnSearch,
            this.btnDetail,
            this.tsUpdateDescriptions,
            this.btnScan,
            this.toolStripButton1});
            this.tsTools.Location = new System.Drawing.Point(0, 0);
            this.tsTools.Name = "tsTools";
            this.tsTools.Size = new System.Drawing.Size(223, 40);
            this.tsTools.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 37);
            this.btnRefresh.Text = "btnRefresh";
            this.btnRefresh.ToolTipText = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnComment
            // 
            this.btnComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnComment.Image = ((System.Drawing.Image)(resources.GetObject("btnComment.Image")));
            this.btnComment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnComment.Name = "btnComment";
            this.btnComment.Size = new System.Drawing.Size(23, 37);
            this.btnComment.Text = "Comment";
            this.btnComment.Click += new System.EventHandler(this.btnComment_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 37);
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDetail
            // 
            this.btnDetail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDetail.Image = ((System.Drawing.Image)(resources.GetObject("btnDetail.Image")));
            this.btnDetail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(23, 37);
            this.btnDetail.Text = "View Detail";
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // tsUpdateDescriptions
            // 
            this.tsUpdateDescriptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsUpdateDescriptions.Image = ((System.Drawing.Image)(resources.GetObject("tsUpdateDescriptions.Image")));
            this.tsUpdateDescriptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsUpdateDescriptions.Name = "tsUpdateDescriptions";
            this.tsUpdateDescriptions.Size = new System.Drawing.Size(23, 37);
            this.tsUpdateDescriptions.Text = "Publish comments to target database";
            this.tsUpdateDescriptions.Click += new System.EventHandler(this.tsUpdateDescriptions_Click);
            // 
            // btnScan
            // 
            this.btnScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnScan.Image = ((System.Drawing.Image)(resources.GetObject("btnScan.Image")));
            this.btnScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(23, 37);
            this.btnScan.Text = "Scan";
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // tvwObject
            // 
            this.tvwObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwObject.ImageIndex = 0;
            this.tvwObject.ImageList = this.ilTreeview;
            this.tvwObject.Location = new System.Drawing.Point(3, 43);
            this.tvwObject.Name = "tvwObject";
            this.tvwObject.SelectedImageIndex = 0;
            this.tvwObject.Size = new System.Drawing.Size(217, 483);
            this.tvwObject.TabIndex = 1;
            this.tvwObject.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwObject_AfterSelect);
            // 
            // tlDetail
            // 
            this.tlDetail.ColumnCount = 1;
            this.tlDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlDetail.Controls.Add(this.pDetailVersion, 0, 0);
            this.tlDetail.Controls.Add(this.pInfo, 0, 1);
            this.tlDetail.Controls.Add(this.html, 0, 2);
            this.tlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlDetail.Location = new System.Drawing.Point(0, 0);
            this.tlDetail.Name = "tlDetail";
            this.tlDetail.RowCount = 3;
            this.tlDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tlDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlDetail.Size = new System.Drawing.Size(444, 529);
            this.tlDetail.TabIndex = 0;
            // 
            // pDetailVersion
            // 
            this.pDetailVersion.Controls.Add(this.chkShowChangesOnly);
            this.pDetailVersion.Controls.Add(this.label1);
            this.pDetailVersion.Controls.Add(this.cmbVersion);
            this.pDetailVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDetailVersion.Location = new System.Drawing.Point(3, 3);
            this.pDetailVersion.Name = "pDetailVersion";
            this.pDetailVersion.Size = new System.Drawing.Size(438, 34);
            this.pDetailVersion.TabIndex = 0;
            // 
            // chkShowChangesOnly
            // 
            this.chkShowChangesOnly.AutoSize = true;
            this.chkShowChangesOnly.Location = new System.Drawing.Point(249, 10);
            this.chkShowChangesOnly.Name = "chkShowChangesOnly";
            this.chkShowChangesOnly.Size = new System.Drawing.Size(92, 17);
            this.chkShowChangesOnly.TabIndex = 2;
            this.chkShowChangesOnly.Text = "Changes Only";
            this.chkShowChangesOnly.UseVisualStyleBackColor = true;
            this.chkShowChangesOnly.CheckedChanged += new System.EventHandler(this.chkShowChangesOnly_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Version:";
            // 
            // cmbVersion
            // 
            this.cmbVersion.DisplayMember = "VersionID";
            this.cmbVersion.FormattingEnabled = true;
            this.cmbVersion.Location = new System.Drawing.Point(64, 6);
            this.cmbVersion.Name = "cmbVersion";
            this.cmbVersion.Size = new System.Drawing.Size(154, 21);
            this.cmbVersion.TabIndex = 0;
            this.cmbVersion.ValueMember = "VersionID";
            this.cmbVersion.SelectedIndexChanged += new System.EventHandler(this.cmbVersion_SelectedIndexChanged);
            this.cmbVersion.SelectionChangeCommitted += new System.EventHandler(this.cmbVersion_SelectionChangeCommitted);
            // 
            // pInfo
            // 
            this.pInfo.Controls.Add(this.lblDesc);
            this.pInfo.Controls.Add(this.txtDesc);
            this.pInfo.Controls.Add(this.lblLastDate);
            this.pInfo.Controls.Add(this.lblFirstDate);
            this.pInfo.Controls.Add(this.lblName);
            this.pInfo.Controls.Add(this.lblLastDateCaption);
            this.pInfo.Controls.Add(this.lblFirstDateCaption);
            this.pInfo.Controls.Add(this.lblNameCaption);
            this.pInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pInfo.Location = new System.Drawing.Point(3, 43);
            this.pInfo.Name = "pInfo";
            this.pInfo.Size = new System.Drawing.Size(438, 96);
            this.pInfo.TabIndex = 1;
            this.pInfo.Resize += new System.EventHandler(this.pInfo_Resize);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(59, 71);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(35, 13);
            this.lblDesc.TabIndex = 7;
            this.lblDesc.Text = "Desc:";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(100, 68);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.ReadOnly = true;
            this.txtDesc.Size = new System.Drawing.Size(329, 19);
            this.txtDesc.TabIndex = 6;
            this.txtDesc.TextChanged += new System.EventHandler(this.txtDesc_TextChanged);
            this.txtDesc.DoubleClick += new System.EventHandler(this.txtDesc_DoubleClick);
            // 
            // lblLastDate
            // 
            this.lblLastDate.AutoSize = true;
            this.lblLastDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastDate.Location = new System.Drawing.Point(97, 52);
            this.lblLastDate.Name = "lblLastDate";
            this.lblLastDate.Size = new System.Drawing.Size(71, 13);
            this.lblLastDate.TabIndex = 5;
            this.lblLastDate.Text = "lblLastDate";
            // 
            // lblFirstDate
            // 
            this.lblFirstDate.AutoSize = true;
            this.lblFirstDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstDate.Location = new System.Drawing.Point(97, 28);
            this.lblFirstDate.Name = "lblFirstDate";
            this.lblFirstDate.Size = new System.Drawing.Size(71, 13);
            this.lblFirstDate.TabIndex = 4;
            this.lblFirstDate.Text = "lblFirstDate";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(97, 4);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(52, 13);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "lblName";
            // 
            // lblLastDateCaption
            // 
            this.lblLastDateCaption.AutoSize = true;
            this.lblLastDateCaption.Location = new System.Drawing.Point(16, 52);
            this.lblLastDateCaption.Name = "lblLastDateCaption";
            this.lblLastDateCaption.Size = new System.Drawing.Size(82, 13);
            this.lblLastDateCaption.TabIndex = 2;
            this.lblLastDateCaption.Text = "Date last found:";
            // 
            // lblFirstDateCaption
            // 
            this.lblFirstDateCaption.AutoSize = true;
            this.lblFirstDateCaption.Location = new System.Drawing.Point(16, 28);
            this.lblFirstDateCaption.Name = "lblFirstDateCaption";
            this.lblFirstDateCaption.Size = new System.Drawing.Size(82, 13);
            this.lblFirstDateCaption.TabIndex = 1;
            this.lblFirstDateCaption.Text = "Date first found:";
            // 
            // lblNameCaption
            // 
            this.lblNameCaption.AutoSize = true;
            this.lblNameCaption.Location = new System.Drawing.Point(16, 4);
            this.lblNameCaption.Name = "lblNameCaption";
            this.lblNameCaption.Size = new System.Drawing.Size(38, 13);
            this.lblNameCaption.TabIndex = 0;
            this.lblNameCaption.Text = "Name:";
            // 
            // html
            // 
            this.html.Dock = System.Windows.Forms.DockStyle.Fill;
            this.html.Location = new System.Drawing.Point(3, 145);
            this.html.MinimumSize = new System.Drawing.Size(20, 20);
            this.html.Name = "html";
            this.html.Size = new System.Drawing.Size(438, 381);
            this.html.TabIndex = 2;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 37);
            this.toolStripButton1.Text = "tsBtnShowVersions";
            this.toolStripButton1.ToolTipText = "Show Changes in this Version";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // showChangesInThisVersionToolStripMenuItem
            // 
            this.showChangesInThisVersionToolStripMenuItem.Name = "showChangesInThisVersionToolStripMenuItem";
            this.showChangesInThisVersionToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.showChangesInThisVersionToolStripMenuItem.Text = "&Show Changes in this Version";
            this.showChangesInThisVersionToolStripMenuItem.Click += new System.EventHandler(this.showChangesInThisVersionToolStripMenuItem_Click);
            // 
            // frmDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 575);
            this.Controls.Add(this.spSplit);
            this.Controls.Add(this.tsStatus);
            this.Controls.Add(this.menuStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(571, 475);
            this.Name = "frmDatabase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Info";
            this.Load += new System.EventHandler(this.frmDatabase_Load);
            this.Resize += new System.EventHandler(this.frmDatabase_Resize);
            this.tsStatus.ResumeLayout(false);
            this.tsStatus.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.spSplit.Panel1.ResumeLayout(false);
            this.spSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spSplit)).EndInit();
            this.spSplit.ResumeLayout(false);
            this.pList.ResumeLayout(false);
            this.pList.PerformLayout();
            this.tsTools.ResumeLayout(false);
            this.tsTools.PerformLayout();
            this.tlDetail.ResumeLayout(false);
            this.pDetailVersion.ResumeLayout(false);
            this.pDetailVersion.PerformLayout();
            this.pInfo.ResumeLayout(false);
            this.pInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList ilToolbar;
        private System.Windows.Forms.ImageList ilTreeview;
        private System.Windows.Forms.StatusStrip tsStatus;
        private System.Windows.Forms.ToolStripStatusLabel tsMessage;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.SplitContainer spSplit;
        private System.Windows.Forms.TableLayoutPanel pList;
        private System.Windows.Forms.ToolStrip tsTools;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripButton btnComment;
        private System.Windows.Forms.ToolStripButton btnSearch;
        private System.Windows.Forms.ToolStripButton btnDetail;
        private System.Windows.Forms.ToolStripButton btnScan;
        private System.Windows.Forms.TreeView tvwObject;
        private System.Windows.Forms.TableLayoutPanel tlDetail;
        private System.Windows.Forms.Panel pDetailVersion;
        private System.Windows.Forms.CheckBox chkShowChangesOnly;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbVersion;
        private System.Windows.Forms.Panel pInfo;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label lblLastDate;
        private System.Windows.Forms.Label lblFirstDate;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblLastDateCaption;
        private System.Windows.Forms.Label lblFirstDateCaption;
        private System.Windows.Forms.Label lblNameCaption;
        private System.Windows.Forms.WebBrowser html;
        private System.Windows.Forms.ToolStripMenuItem setDSNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem publishDescriptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton tsUpdateDescriptions;
        private System.Windows.Forms.ToolStripStatusLabel tsUser;
        private System.Windows.Forms.ToolStripStatusLabel tsServer;
        private System.Windows.Forms.ToolStripStatusLabel tsDatabase;
        private System.Windows.Forms.ToolStripStatusLabel tsDSN;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.ToolStripProgressBar tsProgress;
        private System.Windows.Forms.ToolStripMenuItem showChangesInThisVersionToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}