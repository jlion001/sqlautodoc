using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLAutoDoc
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            SQLAutoDocInstall.Config.Config_Factory oReg =
                new SQLAutoDocInstall.Config.Config_Factory();

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            forms.frmDatabase oScan = new forms.frmDatabase();
            ShowChildForm(oScan);
        }

        private void ShowChildForm(Form oForm)
        {
            oForm.MdiParent = this;

            oForm.Show();

            this.Width = this.Width + 1;
            this.Width = this.Width - 1;
        }
    }
}
