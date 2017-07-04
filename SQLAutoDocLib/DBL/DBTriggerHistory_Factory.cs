using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SQLAutoDocLib.UTIL;

namespace SQLAutoDocLib.DBL
{
    class DBTriggerHistory_Factory : shared.DBLBase, IDisposable
    {
        public DBTriggerHistory_Factory()
            : base(UTIL.ConnectionSchemaManager.ConnectionString)
        {
        }

        void IDisposable.Dispose()
        {
        }

        public void LoadSingleTriggerHistory(BLL.TriggerHistory oTriggerHID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	*")
            .AppendLine(" from")
            .AppendLine("   [dbTriggerhistory] a")
            .AppendLine(" where")
            .AppendFormat(" TriggerHistoryID='{0}'", oTriggerHID.TriggerHistoryID);

            DataTable oDT = base.ExecuteSQLToDatatable(sSQL.ToString());

            if (oDT.Rows.Count > 0)
            {
                oTriggerHID.TriggerID = (Guid)(oDT.Rows[0]["TriggerID"]);
                oTriggerHID.VersionID = DBHelper.NullToString(oDT.Rows[0]["VersionID"]);
                oTriggerHID.Description = DBHelper.NullToString(oDT.Rows[0]["description"]);
                oTriggerHID.Configuration = oDT.Rows[0]["configuration"].ToString();

                oTriggerHID.ChangeDateTime = oDT.Rows[0]["change_date_time"].ToString();
                oTriggerHID.ChangeUID = oDT.Rows[0]["change_uid"].ToString();

            }
            else
                oTriggerHID = null;
        }

        public void InsertSingleTriggerHistory(BLL.TriggerHistory oTriggerHID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("insert into [dbTriggerhistory] (")
                .AppendLine("   TriggerHistoryID,")
                .AppendLine("   TriggerID,")
                .AppendLine("   VersionID,")
                .AppendLine("   description,")
                .AppendLine("   configuration,")
                .AppendLine("   currentlyexists,")

                .AppendLine("   Change_Date_Time,")
                .AppendLine("   Change_UID) values (")

                .AppendFormat("'{0}',", oTriggerHID.TriggerHistoryID)
                .AppendFormat("'{0}',", oTriggerHID.TriggerID)
                .AppendFormat("'{0}',", oTriggerHID.VersionID)
                .AppendFormat("'{0}',", oTriggerHID.Description)
                .AppendFormat("'{0}',", oTriggerHID.Configuration.Replace("'", "''"))
                .AppendFormat("'{0}',", DBHelper.BoolToBit(oTriggerHID.CurrentlyExists))

                .AppendFormat("'{0}',", oTriggerHID.ChangeDateTime)
                .AppendFormat("'{0}')", oTriggerHID.ChangeUID);

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public void UpdateSingleTriggerHistory(BLL.TriggerHistory oTriggerHID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("update [dbTriggerhistory] set")
                .AppendFormat("   TriggerID='{0}',", oTriggerHID.TriggerID)
                .AppendFormat("   VersionID='{0}',", oTriggerHID.VersionID)
                .AppendFormat("   description='{0}',", oTriggerHID.Description)
                .AppendFormat("   configuration='{0}',", oTriggerHID.Configuration.Replace("'", "''"))
                .AppendFormat("   currentlyexists='{0}',", DBHelper.BoolToBit(oTriggerHID.CurrentlyExists))

                .AppendFormat("   Change_Date_Time='{0}',", oTriggerHID.ChangeDateTime)
                .AppendFormat("   Change_UID='{0}'", oTriggerHID.ChangeUID)
                .AppendLine(" where")
                .AppendFormat(" TriggerHistoryID='{0}'", oTriggerHID.TriggerHistoryID);

            base.ExecuteNonQuery(sSQL.ToString());
        }
    }
}
