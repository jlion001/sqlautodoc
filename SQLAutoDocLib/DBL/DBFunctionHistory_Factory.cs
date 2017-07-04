using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SQLAutoDocLib.UTIL;

namespace SQLAutoDocLib.DBL
{
    class DBFunctionHistory_Factory : shared.DBLBase, IDisposable
    {
        public DBFunctionHistory_Factory()
            : base(UTIL.ConnectionSchemaManager.ConnectionString)
        {
        }

        void IDisposable.Dispose()
        {
        }

        public void LoadSingleFunctionHistory(BLL.FunctionHistory oFunctionHID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	*")
            .AppendLine(" from")
            .AppendLine("   [dbFunctionhistory] a")
            .AppendLine(" where")
            .AppendFormat(" FunctionHistoryID='{0}'", oFunctionHID.FunctionHistoryID);

            DataTable oDT = base.ExecuteSQLToDatatable(sSQL.ToString());

            if (oDT.Rows.Count > 0)
            {
                oFunctionHID.FunctionID = (Guid)(oDT.Rows[0]["FunctionID"]);
                oFunctionHID.VersionID = DBHelper.NullToString(oDT.Rows[0]["VersionID"]);
                oFunctionHID.Description = DBHelper.NullToString(oDT.Rows[0]["description"]);
                oFunctionHID.Configuration = oDT.Rows[0]["configuration"].ToString();

                oFunctionHID.ChangeDateTime = oDT.Rows[0]["change_date_time"].ToString();
                oFunctionHID.ChangeUID = oDT.Rows[0]["change_uid"].ToString();

            }
            else
                oFunctionHID = null;
        }

        public void InsertSingleFunctionHistory(BLL.FunctionHistory oFunctionHID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("insert into [dbFunctionhistory] (")
                .AppendLine("   FunctionHistoryID,")
                .AppendLine("   FunctionID,")
                .AppendLine("   VersionID,")
                .AppendLine("   description,")
                .AppendLine("   configuration,")
                .AppendLine("   currentlyexists,")

                .AppendLine("   Change_Date_Time,")
                .AppendLine("   Change_UID) values (")

                .AppendFormat("'{0}',", oFunctionHID.FunctionHistoryID)
                .AppendFormat("'{0}',", oFunctionHID.FunctionID)
                .AppendFormat("'{0}',", oFunctionHID.VersionID)
                .AppendFormat("'{0}',", oFunctionHID.Description)
                .AppendFormat("'{0}',", oFunctionHID.Configuration.Replace("'", "''"))
                .AppendFormat("'{0}',", DBHelper.BoolToBit(oFunctionHID.CurrentlyExists))

                .AppendFormat("'{0}',", oFunctionHID.ChangeDateTime)
                .AppendFormat("'{0}')", oFunctionHID.ChangeUID);

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public void UpdateSingleFunctionHistory(BLL.FunctionHistory oFunctionHID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("update [dbFunctionhistory] set")
                .AppendFormat("   FunctionID='{0}',", oFunctionHID.FunctionID)
                .AppendFormat("   VersionID='{0}',", oFunctionHID.VersionID)
                .AppendFormat("   description='{0}',", oFunctionHID.Description)
                .AppendFormat("   configuration='{0}',", oFunctionHID.Configuration.Replace("'", "''"))
                .AppendFormat("   currentlyexists='{0}',", DBHelper.BoolToBit(oFunctionHID.CurrentlyExists))

                .AppendFormat("   Change_Date_Time='{0}',", oFunctionHID.ChangeDateTime)
                .AppendFormat("   Change_UID='{0}'", oFunctionHID.ChangeUID)
                .AppendLine(" where")
                .AppendFormat(" FunctionHistoryID='{0}'", oFunctionHID.FunctionHistoryID);

            base.ExecuteNonQuery(sSQL.ToString());
        }
    }
}
