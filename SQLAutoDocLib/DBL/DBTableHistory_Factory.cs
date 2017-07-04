using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SQLAutoDocLib.UTIL;

namespace SQLAutoDocLib.DBL
{
    class DBTableHistory_Factory : shared.DBLBase,IDisposable
    {
        public DBTableHistory_Factory()
            : base(UTIL.ConnectionSchemaManager.ConnectionString)
        {
        }

        void IDisposable.Dispose()
        {
        }


        public void LoadSingleTableHistory(BLL.TableHistory oTableHID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	*")
            .AppendLine(" from")
            .AppendLine("   [dbtablehistory] a")
            .AppendLine(" where")
            .AppendFormat(" TableHistoryID='{0}'", oTableHID.TableHistoryID);

            DataTable oDT = base.ExecuteSQLToDatatable(sSQL.ToString());

            if (oDT.Rows.Count > 0)
            {
                oTableHID.TableID = (Guid)(oDT.Rows[0]["TableID"]);
                oTableHID.VersionID = DBHelper.NullToString(oDT.Rows[0]["VersionID"]);
                oTableHID.Description = DBHelper.NullToString(oDT.Rows[0]["description"]);
                oTableHID.Configuration =oDT.Rows[0]["configuration"].ToString();

                oTableHID.ChangeDateTime = oDT.Rows[0]["change_date_time"].ToString();
                oTableHID.ChangeUID = oDT.Rows[0]["change_uid"].ToString();

            }
            else
                oTableHID = null;
        }

        public void InsertSingleTableHistory(BLL.TableHistory oTableHID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("insert into [dbtablehistory] (")
                .AppendLine("   TableHistoryID,")
                .AppendLine("   TableID,")
                .AppendLine("   VersionID,")
                .AppendLine("   description,")
                .AppendLine("   configuration,")
                .AppendLine("   currentlyexists,")

                .AppendLine("   Change_Date_Time,")
                .AppendLine("   Change_UID) values (")

                .AppendFormat("'{0}',", oTableHID.TableHistoryID)
                .AppendFormat("'{0}',", oTableHID.TableID)
                .AppendFormat("'{0}',", oTableHID.VersionID)
                .AppendFormat("'{0}',", oTableHID.Description)
                .AppendFormat("'{0}',", oTableHID.Configuration.Replace("'","''"))
                .AppendFormat("'{0}',", DBHelper.BoolToBit(oTableHID.CurrentlyExists))

                .AppendFormat("'{0}',", oTableHID.ChangeDateTime)
                .AppendFormat("'{0}')", oTableHID.ChangeUID);

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public void UpdateSingleTableHistory(BLL.TableHistory oTableHID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("update [dbtablehistory] set")
                .AppendFormat("   TableID='{0}',", oTableHID.TableID)
                .AppendFormat("   VersionID='{0}',", oTableHID.VersionID)
                .AppendFormat("   description='{0}',", oTableHID.Description)
                .AppendFormat("   configuration='{0}',", oTableHID.Configuration.Replace("'","''"))
                .AppendFormat("   currentlyexists='{0}',", DBHelper.BoolToBit(oTableHID.CurrentlyExists))

                .AppendFormat("   Change_Date_Time='{0}',", oTableHID.ChangeDateTime)
                .AppendFormat("   Change_UID='{0}'", oTableHID.ChangeUID)
                .AppendLine(" where")
                .AppendFormat(" TableHistoryID='{0}'", oTableHID.TableHistoryID);

            base.ExecuteNonQuery(sSQL.ToString());
        }
    }
}
