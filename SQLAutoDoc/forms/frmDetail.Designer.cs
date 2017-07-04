namespace SQLAutoDoc.forms
{
    partial class frmDetail
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
            this.tlDetail = new System.Windows.Forms.TableLayoutPanel();
            this.pDetailVersion = new System.Windows.Forms.Panel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pInfo = new System.Windows.Forms.Panel();
            this.lblLastDate = new System.Windows.Forms.Label();
            this.lblFirstDate = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblLastDateCaption = new System.Windows.Forms.Label();
            this.lblFirstDateCaption = new System.Windows.Forms.Label();
            this.lblNameCaption = new System.Windows.Forms.Label();
            this.html = new System.Windows.Forms.WebBrowser();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.tlDetail.SuspendLayout();
            this.pDetailVersion.SuspendLayout();
            this.pInfo.SuspendLayout();
            this.SuspendLayout();
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
            this.tlDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlDetail.Size = new System.Drawing.Size(485, 449);
            this.tlDetail.TabIndex = 1;
            // 
            // pDetailVersion
            // 
            this.pDetailVersion.Controls.Add(this.lblVersion);
            this.pDetailVersion.Controls.Add(this.label1);
            this.pDetailVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDetailVersion.Location = new System.Drawing.Point(3, 3);
            this.pDetailVersion.Name = "pDetailVersion";
            this.pDetailVersion.Size = new System.Drawing.Size(479, 34);
            this.pDetailVersion.TabIndex = 0;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(97, 9);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(62, 13);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Text = "lblVersion";
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
            this.pInfo.Size = new System.Drawing.Size(479, 74);
            this.pInfo.TabIndex = 1;
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
            this.html.Location = new System.Drawing.Point(3, 123);
            this.html.MinimumSize = new System.Drawing.Size(20, 20);
            this.html.Name = "html";
            this.html.Size = new System.Drawing.Size(479, 323);
            this.html.TabIndex = 2;
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(213, 4);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.ReadOnly = true;
            this.txtDesc.Size = new System.Drawing.Size(229, 61);
            this.txtDesc.TabIndex = 7;
            // 
            // frmDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 449);
            this.Controls.Add(this.tlDetail);
            this.MinimumSize = new System.Drawing.Size(501, 487);
            this.Name = "frmDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "View Detail";
            this.tlDetail.ResumeLayout(false);
            this.pDetailVersion.ResumeLayout(false);
            this.pDetailVersion.PerformLayout();
            this.pInfo.ResumeLayout(false);
            this.pInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlDetail;
        private System.Windows.Forms.Panel pDetailVersion;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pInfo;
        private System.Windows.Forms.Label lblLastDate;
        private System.Windows.Forms.Label lblFirstDate;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblLastDateCaption;
        private System.Windows.Forms.Label lblFirstDateCaption;
        private System.Windows.Forms.Label lblNameCaption;
        private System.Windows.Forms.WebBrowser html;
        private System.Windows.Forms.TextBox txtDesc;
    }
}