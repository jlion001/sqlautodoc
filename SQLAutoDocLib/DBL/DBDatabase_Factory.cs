using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SQLAutoDocLib.UTIL;

namespace SQLAutoDocLib.DBL
{
    class DBDatabase_Factory : shared.DBLBase,IDisposable
    {
        public DBDatabase_Factory()
            : base(UTIL.ConnectionSchemaManager.ConnectionString)
        {
        }

        void IDisposable.Dispose()
        {
        }

        public Guid GetDatabaseID(Guid ServerID, string sDatabaseName)
        {
            Guid oResult = Guid.Empty;

            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	a.DBID")
            .AppendLine(" from")
            .AppendLine("   [database] a")
            .AppendLine(" where")
            .AppendFormat("	a.[serverid]='{0}'", ServerID)
            .AppendFormat("	and a.[Name]='{0}'", sDatabaseName);

            DataTable oDT = base.ExecuteSQLToDatatable(sSQL.ToString());

            if (oDT.Rows.Count > 0)
                oResult = (Guid)oDT.Rows[0][0];

            return oResult;
        }

        public void LoadSingleDatabase(BLL.Database oDB)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	*")
            .AppendLine(" from")
            .AppendLine("   [database] a")
            .AppendLine(" where")
            .AppendFormat("	a.[DBID]='{0}'", oDB.DBID);

            DataTable oDT = base.ExecuteSQLToDatatable(sSQL.ToString());

            if (oDT.Rows.Count > 0)
            {
                oDB.Name = oDT.Rows[0]["name"].ToString();
                oDB.Description = DBHelper.NullToString(oDT.Rows[0]["description"]);
                oDB.ServerID = (Guid)oDT.Rows[0]["serverid"];
                oDB.ChangeDateTime= oDT.Rows[0]["change_date_time"].ToString();
                oDB.ChangeUID= oDT.Rows[0]["change_uid"].ToString();

            }
            else
                oDB = null;
        }

        public void InsertSingleDatabase(BLL.Database oDB)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("insert into [database] (")
                .AppendLine("   serverid,")
                .AppendLine("   DBID,")
                .AppendLine("   name,")
                .AppendLine("   description,")
                .AppendLine("   Change_Date_Time,")
                .AppendLine("   Change_UID) values (")
                .AppendFormat("'{0}',", oDB.ServerID)
                .AppendFormat("'{0}',", oDB.DBID)
                .AppendFormat("'{0}',", oDB.Name)
                .AppendFormat("'{0}',", oDB.Description)
                .AppendFormat("'{0}',", oDB.ChangeDateTime)
                .AppendFormat("'{0}')", oDB.ChangeUID);

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public void UpdateSingleDatabase(BLL.Database oDB)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("update [database] set")
                .AppendFormat("   name='{0}',", oDB.Name)
                .AppendFormat("   description='{0}',", oDB.Description)
                .AppendFormat("   Change_Date_Time='{0}',", oDB.ChangeDateTime)
                .AppendFormat("   Change_UID='{0}'", oDB.ChangeUID)
                .AppendLine(" where")
                .AppendFormat(" DBID='{0}'", oDB.DBID);

            base.ExecuteNonQuery(sSQL.ToString());
        }
    }
}
