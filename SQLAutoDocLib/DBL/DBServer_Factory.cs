using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SQLAutoDocLib.UTIL;

namespace SQLAutoDocLib.DBL
{
    class DBServer_Factory : shared.DBLBase, IDisposable
    {
        public DBServer_Factory()
            : base(UTIL.ConnectionSchemaManager.ConnectionString)
        {
        }

        void IDisposable.Dispose()
        {
        }

        public Guid GetServerID(string sServerName)
        {
            Guid oResult = Guid.Empty;

            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	a.ServerID")
            .AppendLine(" from")
            .AppendLine("	[server] a")
            .AppendLine(" where")
            .AppendFormat("	a.[name]='{0}'", sServerName);

            DataTable oDT = base.ExecuteSQLToDatatable(sSQL.ToString());

            if (oDT.Rows.Count > 0)
                oResult = (Guid)oDT.Rows[0][0];

            return oResult;
        }


        public void LoadSingleServer(BLL.Server oDB)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	*")
            .AppendLine(" from")
            .AppendLine("   [server] a")
            .AppendLine(" where")
            .AppendFormat("	a.[serverid]='{0}'", oDB.ServerID);

            DataTable oDT = base.ExecuteSQLToDatatable(sSQL.ToString());

            if (oDT.Rows.Count > 0)
            {
                oDB.Name = oDT.Rows[0]["name"].ToString();
                oDB.Description = DBHelper.NullToString(oDT.Rows[0]["description"]);
                oDB.ChangeDateTime = oDT.Rows[0]["change_date_time"].ToString();
                oDB.ChangeUID = oDT.Rows[0]["change_uid"].ToString();

            }
            else
                oDB = null;
        }

        public void InsertSingleServer(BLL.Server oDB)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("insert into [server] (")
                .AppendLine("   serverid,")
                .AppendLine("   name,")
                .AppendLine("   description,")
                .AppendLine("   Change_Date_Time,")
                .AppendLine("   Change_UID) values (")
                .AppendFormat("'{0}',", oDB.ServerID)
                .AppendFormat("'{0}',", oDB.Name)
                .AppendFormat("'{0}',", oDB.Description)
                .AppendFormat("'{0}',", oDB.ChangeDateTime)
                .AppendFormat("'{0}')", oDB.ChangeUID);

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public void UpdateSingleServer(BLL.Server oDB)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("update [server] set")
                .AppendFormat("   name='{0}',", oDB.Name)
                .AppendFormat("   description='{0}',", oDB.Description)
                .AppendFormat("   Change_Date_Time='{0}',", oDB.ChangeDateTime)
                .AppendFormat("   Change_UID='{0}'", oDB.ChangeUID)
                .AppendLine(" where")
                .AppendFormat(" serverid='{0}'", oDB.ServerID);

            base.ExecuteNonQuery(sSQL.ToString());
        }
    }
}
