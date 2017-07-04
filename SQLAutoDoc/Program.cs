using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SQLAutoDoc
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SQLAutoDocInstall.Config.Config_Factory oReg =
                new SQLAutoDocInstall.Config.Config_Factory();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SQLAutoDoc.forms.frmDatabase());
        }
    }
}
