using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SQLAutoDocLib.SCAN.AGENT
{
    class DBServerAgent_Factory : SQLAutoDocLib.SCAN.shared.DBLBase
    {
        public DBServerAgent_Factory(string sConnectionString)
            : base(sConnectionString)
        {
        }

        public string GetServerName()
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select @@SERVERNAME");

            DataTable oDT = ExecuteSQLToDatatable(sSQL.ToString());

            return oDT.Rows[0][0].ToString();
        }
    }
}
