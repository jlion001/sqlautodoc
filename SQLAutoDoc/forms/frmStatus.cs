using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLAutoDoc.forms
{
    public partial class frmStatus : Form
    {
        public frmStatus()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            lvwHistory.Items.Clear();
        }

        public void AddEntry(string StatusText)
        {
            ListViewItem oEntry= lvwHistory.Items.Add(DateTime.Now.ToShortTimeString());
            oEntry.SubItems.Add(StatusText);
        }

        private void frmStatus_Resize(object sender, EventArgs e)
        {
            chEntry.Width = this.Width - 91;
        }
    }
}
