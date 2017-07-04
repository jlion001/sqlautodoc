namespace SQLAutoDoc
{
    partial class frmDSN
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
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.lvwDSN = new System.Windows.Forms.ListView();
            this.chDSN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chServer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDatabase = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(348, 238);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 18;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(267, 238);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 19;
            this.cmdOK.Text = "&OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // lvwDSN
            // 
            this.lvwDSN.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDSN,
            this.chServer,
            this.chDatabase});
            this.lvwDSN.FullRowSelect = true;
            this.lvwDSN.GridLines = true;
            this.lvwDSN.Location = new System.Drawing.Point(9, 11);
            this.lvwDSN.MultiSelect = false;
            this.lvwDSN.Name = "lvwDSN";
            this.lvwDSN.Size = new System.Drawing.Size(415, 215);
            this.lvwDSN.TabIndex = 20;
            this.lvwDSN.UseCompatibleStateImageBehavior = false;
            this.lvwDSN.View = System.Windows.Forms.View.Details;
            this.lvwDSN.DoubleClick += new System.EventHandler(this.cmdOK_Click);
            // 
            // chDSN
            // 
            this.chDSN.Text = "DSN";
            this.chDSN.Width = 171;
            // 
            // chServer
            // 
            this.chServer.Text = "Server";
            this.chServer.Width = 108;
            // 
            // chDatabase
            // 
            this.chDatabase.Text = "Database";
            this.chDatabase.Width = 110;
            // 
            // frmDSN
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(436, 273);
            this.Controls.Add(this.lvwDSN);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDSN";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Set DSN";
            this.Load += new System.EventHandler(this.frmDSN_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.ListView lvwDSN;
        private System.Windows.Forms.ColumnHeader chDSN;
        private System.Windows.Forms.ColumnHeader chServer;
        private System.Windows.Forms.ColumnHeader chDatabase;
    }
}