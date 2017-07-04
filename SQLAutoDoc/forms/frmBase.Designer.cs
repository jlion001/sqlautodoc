namespace SQLAutoDoc.forms
{
    partial class frmBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBase));
            this.spSplit = new System.Windows.Forms.SplitContainer();
            this.pList = new System.Windows.Forms.TableLayoutPanel();
            this.tsTools = new System.Windows.Forms.ToolStrip();
            this.ilToolbar = new System.Windows.Forms.ImageList(this.components);
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnComment = new System.Windows.Forms.ToolStripButton();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.btnDetail = new System.Windows.Forms.ToolStripButton();
            this.tlDetail = new System.Windows.Forms.TableLayoutPanel();
            this.pDetailVersion = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pInfo = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblFirstDate = new System.Windows.Forms.Label();
            this.lblLastDate = new System.Windows.Forms.Label();
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
            // spSplit
            // 
            this.spSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spSplit.Location = new System.Drawing.Point(0, 0);
            this.spSplit.Name = "spSplit";
            // 
            // spSplit.Panel1
            // 
            this.spSplit.Panel1.Controls.Add(this.pList);
            // 
            // spSplit.Panel2
            // 
            this.spSplit.Panel2.Controls.Add(this.tlDetail);
            this.spSplit.Size = new System.Drawing.Size(355, 348);
            this.spSplit.SplitterDistance = 118;
            this.spSplit.TabIndex = 0;
            // 
            // pList
            // 
            this.pList.ColumnCount = 1;
            this.pList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pList.Controls.Add(this.tsTools, 0, 0);
            this.pList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pList.Location = new System.Drawing.Point(0, 0);
            this.pList.Name = "pList";
            this.pList.RowCount = 2;
            this.pList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pList.Size = new System.Drawing.Size(118, 348);
            this.pList.TabIndex = 0;
            // 
            // tsTools
            // 
            this.tsTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnComment,
            this.btnSearch,
            this.btnDetail});
            this.tsTools.Location = new System.Drawing.Point(0, 0);
            this.tsTools.Name = "tsTools";
            this.tsTools.Size = new System.Drawing.Size(118, 40);
            this.tsTools.TabIndex = 0;
            // 
            // ilToolbar
            // 
            this.ilToolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilToolbar.ImageStream")));
            this.ilToolbar.TransparentColor = System.Drawing.Color.Transparent;
            this.ilToolbar.Images.SetKeyName(0, "tb_refresh.jpg");
            this.ilToolbar.Images.SetKeyName(1, "tb_save.jpg");
            this.ilToolbar.Images.SetKeyName(2, "tb_search.jpg");
            this.ilToolbar.Images.SetKeyName(3, "tb_viewdetail.jpg");
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
            // 
            // btnComment
            // 
            this.btnComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnComment.Image = ((System.Drawing.Image)(resources.GetObject("btnComment.Image")));
            this.btnComment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnComment.Name = "btnComment";
            this.btnComment.Size = new System.Drawing.Size(23, 37);
            this.btnComment.Text = "Comment";
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 37);
            this.btnSearch.Text = "Search";
            // 
            // btnDetail
            // 
            this.btnDetail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDetail.Image = ((System.Drawing.Image)(resources.GetObject("btnDetail.Image")));
            this.btnDetail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(23, 37);
            this.btnDetail.Text = "View Detail";
            // 
            // tlDetail
            // 
            this.tlDetail.ColumnCount = 1;
            this.tlDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlDetail.Controls.Add(this.pDetailVersion, 0, 0);
            this.tlDetail.Controls.Add(this.pInfo, 0, 1);
            this.tlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlDetail.Location = new System.Drawing.Point(0, 0);
            this.tlDetail.Name = "tlDetail";
            this.tlDetail.RowCount = 3;
            this.tlDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlDetail.Size = new System.Drawing.Size(233, 348);
            this.tlDetail.TabIndex = 0;
            // 
            // pDetailVersion
            // 
            this.pDetailVersion.Controls.Add(this.label1);
            this.pDetailVersion.Controls.Add(this.comboBox1);
            this.pDetailVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDetailVersion.Location = new System.Drawing.Point(3, 3);
            this.pDetailVersion.Name = "pDetailVersion";
            this.pDetailVersion.Size = new System.Drawing.Size(227, 34);
            this.pDetailVersion.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(64, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(154, 21);
            this.comboBox1.TabIndex = 0;
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
            // pInfo
            // 
            this.pInfo.Controls.Add(this.lblLastDate);
            this.pInfo.Controls.Add(this.lblFirstDate);
            this.pInfo.Controls.Add(this.lblName);
            this.pInfo.Controls.Add(this.label4);
            this.pInfo.Controls.Add(this.label3);
            this.pInfo.Controls.Add(this.label2);
            this.pInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pInfo.Location = new System.Drawing.Point(3, 43);
            this.pInfo.Name = "pInfo";
            this.pInfo.Size = new System.Drawing.Size(227, 74);
            this.pInfo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Date first found:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Date last found:";
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
            // frmBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 348);
            this.Controls.Add(this.spSplit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBase";
            this.Text = "frmBase";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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

        }

        #endregion

        private System.Windows.Forms.SplitContainer spSplit;
        private System.Windows.Forms.TableLayoutPanel pList;
        private System.Windows.Forms.ToolStrip tsTools;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripButton btnComment;
        private System.Windows.Forms.ToolStripButton btnSearch;
        private System.Windows.Forms.ToolStripButton btnDetail;
        private System.Windows.Forms.ImageList ilToolbar;
        private System.Windows.Forms.TableLayoutPanel tlDetail;
        private System.Windows.Forms.Panel pDetailVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel pInfo;
        private System.Windows.Forms.Label lblLastDate;
        private System.Windows.Forms.Label lblFirstDate;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}