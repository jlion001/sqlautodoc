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
    public partial class frmDesc : Form
    {
        private SQLAutoDocLib.UTIL.ObjectType m_ObjectType = SQLAutoDocLib.UTIL.ObjectType.Not_Specified;
        private Guid m_ObjectID = Guid.Empty;
        private string m_ObjectName = null;

        public bool CancelPressed { get; set; }

        public string Desc
        {
            get { return txtDesc.Text; }
        }

        public frmDesc(
                    SQLAutoDocLib.UTIL.ObjectType ObjectType,
                    string ObjectDesc,
                    string ObjectName)
        {
            InitializeComponent();

            m_ObjectType = ObjectType;
            txtDesc.Text = ObjectDesc;
            m_ObjectName = ObjectName;

            lblName.Text = m_ObjectType.ToString() + '.' + m_ObjectName;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            CancelPressed = true;
            this.DialogResult = DialogResult.Cancel;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            CancelPressed = false;
            this.DialogResult = DialogResult.OK;
        }
    }
}
