using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SQLAutoDoc.classes;

namespace SQLAutoDoc
{
    /// <summary>
    /// Form for setting the current DSN
    /// </summary>
    public partial class frmDSN : Form
    {
        /// <summary>
        /// Form constructor
        /// </summary>
        public frmDSN()
        {
            InitializeComponent();
        }

        private void frmDSN_Load(object sender, EventArgs e)
        {
            ShowDSNs();
        }

        /// <summary>
        /// Get or set DSN shown as selected when form is displayed.
        /// </summary>
        public string SelectedDSN
        {
            set { SetSelectedDSN(value); }
            get { return GetSelectedDSN(); }
        }

        /// <summary>
        /// Name of currently active sql server
        /// </summary>
        public string ServerName
        {
            get {return GetSelectedServerName();}
        }

        /// <summary>
        /// Name of currently active database
        /// </summary>
        public string DatabaseName
        {
            get { return GetSelectedDatabaseName(); }
        }

        /// <summary>
        /// Form constructor
        /// </summary>
        protected void ShowDSNs()
        {
            OdbcDataSourceManager dsnManager = new OdbcDataSourceManager();
            System.Collections.SortedList dsnList = dsnManager.GetAllDataSourceNames();
            for (int i = 0; i < dsnList.Count; i++)
            {
                string sName = (string)dsnList.GetKey(i);
                SQLAutoDoc.classes.DataSourceType type = (SQLAutoDoc.classes.DataSourceType)dsnList.GetByIndex(i);
                AddToListBox(sName + " - (" + type.ToString() + " DSN)");
            }
        }

        private void AddToListBox(string sDSN)
        {
            //Is this a sql server dsn?
            string sConnectionString = "DSN=" + SQLAutoDocLib.UTIL.DSNConnectionSchemaManager.ParseConnection(sDSN);
            AutoDocSQLBase oDBase = new AutoDocSQLBase(Environment.UserName, sConnectionString);

            string sServer = "";
            string sDatabase = "";

            bool bResult=oDBase.GetServerInfo(out sServer, out sDatabase);

            if (bResult)
            {
                ListViewItem oItem = new ListViewItem();
                oItem.Text = sDSN;

                oItem.SubItems.Add(sServer);
                oItem.SubItems.Add(sDatabase);

                lvwDSN.Items.Add(oItem);
            }
        }

        private void SetSelectedDSN(string sDSN)
        {
            for (int i = 0; i < lvwDSN.Items.Count; i++)
                if (lvwDSN.Items[i].Text == sDSN)
                {
                    lvwDSN.Items[i].Selected=true;
                    break;
                }
        }

        private string GetSelectedDSN()
        {
            string sRetValue = "";

            if (lvwDSN.SelectedItems.Count>0)
                sRetValue = lvwDSN.SelectedItems[0].Text;

            return sRetValue;
        }

        private string GetSelectedServerName()
        {
            string sRetValue = "";

            if (lvwDSN.SelectedItems.Count > 0)
                sRetValue = lvwDSN.SelectedItems[0].SubItems[1].Text;

            return sRetValue;
        }

        private string GetSelectedDatabaseName()
        {
            string sRetValue = "";

            if (lvwDSN.SelectedItems.Count > 0)
                sRetValue = lvwDSN.SelectedItems[0].SubItems[2].Text;

            return sRetValue;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (lvwDSN.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a DSN.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Hide();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }

    }
}
