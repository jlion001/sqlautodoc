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
    public partial class frmScan : Form
    {
        public frmScan()
        {
            InitializeComponent();
        }

        private void frmScan_Load(object sender, EventArgs e)
        {
      
        }

        private void cmdScan_Click(object sender, EventArgs e)
        {
            SQLAutoDocLib.UTIL.DSNConnectionSchemaManager.ConnectionString = "DSN=sqlautodoc";

            SQLAutoDocLib.SCAN.SCANNER oDBScanner = new SQLAutoDocLib.SCAN.SCANNER(this,sConnectionString: "DSN=SJS_Prod_FL24");
            oDBScanner.ScanDatabaseBegin += new SQLAutoDocLib.SCAN.SCANNER.ScanDatabaseBeginEvent(ScanDatabaseBeginEvent_Handler);
            oDBScanner.ScanDatabaseEnd += new SQLAutoDocLib.SCAN.SCANNER.ScanDatabaseEndEvent(ScanDatabaseEndEvent_Handler);

            oDBScanner.ScanObjectTypeBegin += new SQLAutoDocLib.SCAN.SCANNER.ScanObjectTypeBeginEvent(ScanObjectBeginEvent_Handler);
            oDBScanner.ScanObjectTypeEnd += new SQLAutoDocLib.SCAN.SCANNER.ScanObjectTypeEndEvent(ScanObjectEndEvent_Handler);

            oDBScanner.ScanObjectChanged += new SQLAutoDocLib.SCAN.SCANNER.ScanObjectChangedEvent(ScanObjectChanged_Handler);
            oDBScanner.ScanObjectNotFound += new SQLAutoDocLib.SCAN.SCANNER.ScanObjectNotFoundEvent(ScanObjectNotFound_Handler);

            oDBScanner.Scan();              
        }

        public void ScanDatabaseBeginEvent_Handler(DateTime e)
        {
            Console.WriteLine("Scan starting: " + e.ToString());
        }

        public void ScanDatabaseEndEvent_Handler(DateTime e)
        {
            Console.WriteLine("Scan complete: " + e.ToString());
        }

        public void ScanObjectBeginEvent_Handler(string type,DateTime e)
        {
            Console.WriteLine("Scan starting: " + e.ToString());
        }

        public void ScanObjectEndEvent_Handler(string type, DateTime e)
        {
            Console.WriteLine("Scan starting: " + e.ToString());
        }

        public void ScanObjectChanged_Handler(string type, string name, DateTime e)
        {
            Console.WriteLine("Scan starting: " + e.ToString());
        }

        public void ScanObjectNotFound_Handler(string type, string name,DateTime e)
        {
            Console.WriteLine("Scan starting: " + e.ToString());
        }
    }
}
