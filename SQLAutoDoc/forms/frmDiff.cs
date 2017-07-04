using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace SQLAutoDoc.forms
{
    public partial class frmDiff : Form
    {
        Guid m_DBID = Guid.Empty;
        private string m_Version=null;

        public frmDiff(Guid DBID,string sVersion)
        {
            InitializeComponent();

            m_DBID = DBID;
            m_Version = sVersion;
        }

        public void ShowVersion()
        {
            this.Show();
            this.TopMost = true;

            this.UseWaitCursor = true;

            lblVersion.Text = m_Version;

            //Add tables
            SQLAutoDocLib.BLL.Table_Factory oTableFactory = new SQLAutoDocLib.BLL.Table_Factory();
            List<SQLAutoDocLib.BLL.Table> oTableList = oTableFactory.ListAllTablesInDatabase(m_DBID, m_Version,ChangedOnly:true);
            foreach (SQLAutoDocLib.BLL.Table oTable in oTableList)
                {
                    ListViewItem oItem = new ListViewItem(text: oTable.Name);
                    oItem.Tag = oTable;
                    oItem.Group = lvwDifferences.Groups[0];
                    lvwDifferences.Items.Add(oItem);
                }

            //Add views
            SQLAutoDocLib.BLL.View_Factory oViewFactory = new SQLAutoDocLib.BLL.View_Factory();
            List<SQLAutoDocLib.BLL.View> oViewList = oViewFactory.ListAllViewsInDatabase(m_DBID, m_Version, ChangedOnly: true);
            foreach (SQLAutoDocLib.BLL.View oView in oViewList)
                {
                    ListViewItem oItem = new ListViewItem(text: oView.Name);
                    oItem.Tag = oView;
                    oItem.Group = lvwDifferences.Groups[1];
                    lvwDifferences.Items.Add(oItem);
                }

            //Add procs
            SQLAutoDocLib.BLL.Procedure_Factory oProcFactory = new SQLAutoDocLib.BLL.Procedure_Factory();
            List<SQLAutoDocLib.BLL.Procedure> oProcList = oProcFactory.ListAllProceduresInDatabase(m_DBID, m_Version, ChangedOnly: true);
            foreach (SQLAutoDocLib.BLL.Procedure oProc in oProcList)
                {
                    ListViewItem oItem = new ListViewItem(text: oProc.Name);
                    oItem.Tag = oProc;
                    oItem.Group = lvwDifferences.Groups[2];
                    lvwDifferences.Items.Add(oItem);
                }

            //Add functions
            SQLAutoDocLib.BLL.Function_Factory oFunctionFactory = new SQLAutoDocLib.BLL.Function_Factory();
            List<SQLAutoDocLib.BLL.Function> oFunctionList = oFunctionFactory.ListAllFunctionsInDatabase(m_DBID, m_Version, ChangedOnly: true);
            foreach (SQLAutoDocLib.BLL.Function oFunction in oFunctionList)
                {
                    ListViewItem oItem = new ListViewItem(text: oFunction.Name);
                    oItem.Tag = oFunction;
                    oItem.Group = lvwDifferences.Groups[3];
                    lvwDifferences.Items.Add(oItem);
                }

            //Add triggers
            SQLAutoDocLib.BLL.Trigger_Factory oTriggerFactory = new SQLAutoDocLib.BLL.Trigger_Factory();
            List<SQLAutoDocLib.BLL.Trigger> oTriggerList = oTriggerFactory.ListAllTriggersInDatabase(m_DBID, m_Version, ChangedOnly: true);
            foreach (SQLAutoDocLib.BLL.Trigger oTrigger in oTriggerList)
                {
                    ListViewItem oItem = new ListViewItem(text: oTrigger.Name);
                    oItem.Tag = oTrigger;
                    oItem.Group = lvwDifferences.Groups[4];
                    lvwDifferences.Items.Add(oItem);
                }

            this.UseWaitCursor = false;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdGenerate_Click(object sender, EventArgs e)
        {
            SaveFileDialog oSF = new SaveFileDialog();
            oSF.Title = "Save Generated Script File";
            oSF.FileName = m_Version + ".SQL";

            if (oSF.ShowDialog(this) == DialogResult.OK)
            {
                SaveScript(oSF.FileName);
            }
        }

        private void SaveScript(string sFileName)
        {
            using (StreamWriter oSW = new StreamWriter(path: sFileName, append: false))
            {
                oSW.WriteLine("/* VERSION: " + m_Version + " */");

                foreach (ListViewItem oItem in lvwDifferences.Items)
                {
                    if (oItem.Checked == true)
                    {
                        object oValue = oItem.Tag;
                        string sTypeName = oValue.GetType().ToString();
                                                
                        if (oValue.GetType()==typeof(SQLAutoDocLib.BLL.Table))
                        {
                            //This is a table
                            SQLAutoDocLib.BLL.Table oTable = (SQLAutoDocLib.BLL.Table)oValue;
                            if (oTable.CurrentlyExists == true)
                                oSW.WriteLine("/* ALTER */");
                            else
                                oSW.WriteLine("/* CREATE */");

                            SQLAutoDocLib.BLL.Table_Factory oFactory = new SQLAutoDocLib.BLL.Table_Factory();
                            oSW.WriteLine(oFactory.Reconstitute(oTable));
                        }
                        else if (oValue.GetType()==typeof(SQLAutoDocLib.BLL.View))
                        {
                            //This is a view
                            SQLAutoDocLib.BLL.View oView = (SQLAutoDocLib.BLL.View)oValue;
                            SQLAutoDocLib.BLL.View_Factory oFactory = new SQLAutoDocLib.BLL.View_Factory();
                            oSW.WriteLine(oFactory.Reconstitute(oView));
                        }
                        else if (oValue.GetType() == typeof(SQLAutoDocLib.BLL.Function))
                        {
                            //This is a function
                            SQLAutoDocLib.BLL.Function oFunction = (SQLAutoDocLib.BLL.Function)oValue;
                            SQLAutoDocLib.BLL.Function_Factory oFactory = new SQLAutoDocLib.BLL.Function_Factory();
                            oSW.WriteLine(oFactory.Reconstitute(oFunction));
                        }
                        else if (oValue.GetType() == typeof(SQLAutoDocLib.BLL.Procedure))
                        {
                            //This is a procedure
                            SQLAutoDocLib.BLL.Procedure oProcedure = (SQLAutoDocLib.BLL.Procedure)oValue;
                            SQLAutoDocLib.BLL.Procedure_Factory oFactory = new SQLAutoDocLib.BLL.Procedure_Factory();
                            oSW.WriteLine(oFactory.Reconstitute(oProcedure));
                        }
                        else if (oValue.GetType() == typeof(SQLAutoDocLib.BLL.Trigger))
                        {
                            //This is a trigger
                            SQLAutoDocLib.BLL.Trigger oTrigger = (SQLAutoDocLib.BLL.Trigger)oValue;
                            SQLAutoDocLib.BLL.Trigger_Factory oFactory = new SQLAutoDocLib.BLL.Trigger_Factory();
                            oSW.WriteLine(oFactory.Reconstitute(oTrigger));
                        }
                    }
                }
            }
        }
    }
}
