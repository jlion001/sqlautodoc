using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SQLAutoDocLib.UTIL;

namespace SQLAutoDocLib.DBL
{
    class DBViewHistory_Factory : shared.DBLBase, IDisposable
    {
    public DBViewHistory_Factory()
            : base(UTIL.ConnectionSchemaManager.ConnectionString)
        {
        }

        void IDisposable.Dispose()
        {
        }

        public void LoadSingleViewHistory(BLL.ViewHistory oViewHID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	*")
            .AppendLine(" from")
            .AppendLine("   [dbViewhistory] a")
            .AppendLine(" where")
            .AppendFormat(" ViewHistoryID='{0}'", oViewHID.ViewHistoryID);

            DataTable oDT = base.ExecuteSQLToDatatable(sSQL.ToString());

            if (oDT.Rows.Count > 0)
            {
                oViewHID.ViewID = (Guid)(oDT.Rows[0]["ViewID"]);
                oViewHID.VersionID = DBHelper.NullToString(oDT.Rows[0]["VersionID"]);
                oViewHID.Description = DBHelper.NullToString(oDT.Rows[0]["description"]);
                oViewHID.Configuration = oDT.Rows[0]["configuration"].ToString();

                oViewHID.ChangeDateTime = oDT.Rows[0]["change_date_time"].ToString();
                oViewHID.ChangeUID = oDT.Rows[0]["change_uid"].ToString();

            }
            else
                oViewHID = null;
        }

        public void InsertSingleViewHistory(BLL.ViewHistory oViewHID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("insert into [dbViewhistory] (")
                .AppendLine("   ViewHistoryID,")
                .AppendLine("   ViewID,")
                .AppendLine("   VersionID,")
                .AppendLine("   description,")
                .AppendLine("   configuration,")
                .AppendLine("   currentlyexists,")

                .AppendLine("   Change_Date_Time,")
                .AppendLine("   Change_UID) values (")

                .AppendFormat("'{0}',", oViewHID.ViewHistoryID)
                .AppendFormat("'{0}',", oViewHID.ViewID)
                .AppendFormat("'{0}',", oViewHID.VersionID)
                .AppendFormat("'{0}',", oViewHID.Description)
                .AppendFormat("'{0}',", oViewHID.Configuration.Replace("'", "''"))
                .AppendFormat("'{0}',", DBHelper.BoolToBit(oViewHID.CurrentlyExists))

                .AppendFormat("'{0}',", oViewHID.ChangeDateTime)
                .AppendFormat("'{0}')", oViewHID.ChangeUID);

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public void UpdateSingleViewHistory(BLL.ViewHistory oViewHID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("update [dbViewhistory] set")
                .AppendFormat("   ViewID='{0}',", oViewHID.ViewID)
                .AppendFormat("   VersionID='{0}',", oViewHID.VersionID)
                .AppendFormat("   description='{0}',", oViewHID.Description)
                .AppendFormat("   configuration='{0}',", oViewHID.Configuration.Replace("'", "''"))
                .AppendFormat("   currentlyexists='{0}',", DBHelper.BoolToBit(oViewHID.CurrentlyExists))
                
                .AppendFormat("   Change_Date_Time='{0}',", oViewHID.ChangeDateTime)
                .AppendFormat("   Change_UID='{0}'", oViewHID.ChangeUID)
                .AppendLine(" where")
                .AppendFormat(" ViewHistoryID='{0}'", oViewHID.ViewHistoryID);

            base.ExecuteNonQuery(sSQL.ToString());
        }
    }
}
