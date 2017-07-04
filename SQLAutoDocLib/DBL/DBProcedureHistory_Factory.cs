using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SQLAutoDocLib.UTIL;

namespace SQLAutoDocLib.DBL
{
    class DBProcedureHistory_Factory : shared.DBLBase, IDisposable
    {
        public DBProcedureHistory_Factory()
            : base(UTIL.ConnectionSchemaManager.ConnectionString)
        {
        }

        void IDisposable.Dispose()
        {
        }

        public void LoadSingleProcedureHistory(BLL.ProcedureHistory oProcedureHID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	*")
            .AppendLine(" from")
            .AppendLine("   [dbprocedurehistory] a")
            .AppendLine(" where")
            .AppendFormat(" ProcedureHistoryID='{0}'", oProcedureHID.ProcedureHistoryID);

            DataTable oDT = base.ExecuteSQLToDatatable(sSQL.ToString());

            if (oDT.Rows.Count > 0)
            {
                oProcedureHID.ProcedureID = (Guid)(oDT.Rows[0]["ProcedureID"]);
                oProcedureHID.VersionID = DBHelper.NullToString(oDT.Rows[0]["VersionID"]);
                oProcedureHID.Description = DBHelper.NullToString(oDT.Rows[0]["description"]);
                oProcedureHID.Configuration = oDT.Rows[0]["configuration"].ToString();

                oProcedureHID.ChangeDateTime = oDT.Rows[0]["change_date_time"].ToString();
                oProcedureHID.ChangeUID = oDT.Rows[0]["change_uid"].ToString();

            }
            else
                oProcedureHID = null;
        }

        public void InsertSingleProcedureHistory(BLL.ProcedureHistory oProcedureHID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("insert into [dbProcedurehistory] (")
                .AppendLine("   ProcedureHistoryID,")
                .AppendLine("   ProcedureID,")
                .AppendLine("   VersionID,")
                .AppendLine("   description,")
                .AppendLine("   configuration,")
                .AppendLine("   currentlyexists,")

                .AppendLine("   Change_Date_Time,")
                .AppendLine("   Change_UID) values (")

                .AppendFormat("'{0}',", oProcedureHID.ProcedureHistoryID)
                .AppendFormat("'{0}',", oProcedureHID.ProcedureID)
                .AppendFormat("'{0}',", oProcedureHID.VersionID)
                .AppendFormat("'{0}',", oProcedureHID.Description)
                .AppendFormat("'{0}',", oProcedureHID.Configuration.Replace("'", "''"))
                .AppendFormat("'{0}',", DBHelper.BoolToBit(oProcedureHID.CurrentlyExists))

                .AppendFormat("'{0}',", oProcedureHID.ChangeDateTime)
                .AppendFormat("'{0}')", oProcedureHID.ChangeUID);

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public void UpdateSingleProcedureHistory(BLL.ProcedureHistory oProcedureHID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("update [dbProcedurehistory] set")
                .AppendFormat("   ProcedureID='{0}',", oProcedureHID.ProcedureID)
                .AppendFormat("   VersionID='{0}',", oProcedureHID.VersionID)
                .AppendFormat("   description='{0}',", oProcedureHID.Description)
                .AppendFormat("   currentlyexists='{0}',", DBHelper.BoolToBit(oProcedureHID.CurrentlyExists))

                .AppendFormat("   Change_Date_Time='{0}',", oProcedureHID.ChangeDateTime)
                .AppendFormat("   Change_UID='{0}'", oProcedureHID.ChangeUID)
                .AppendLine(" where")
                .AppendFormat(" ProcedureHistoryID='{0}'", oProcedureHID.ProcedureHistoryID);

            base.ExecuteNonQuery(sSQL.ToString());
        }
    }
}
