using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SQLAutoDocLib.SCAN.AGENT
{
    class DBDatabaseAgent_Factory : SQLAutoDocLib.SCAN.shared.DBLBase
    {
        public DBDatabaseAgent_Factory(string sConnectionString)
            : base(sConnectionString)
        {
        }

        public string GetDatabaseName()
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select DB_NAME()");

            DataTable oDT = ExecuteSQLToDatatable(sSQL.ToString());

            return oDT.Rows[0][0].ToString();
        }
    }
}
