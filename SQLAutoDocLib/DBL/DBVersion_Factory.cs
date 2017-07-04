using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SQLAutoDocLib.UTIL;

namespace SQLAutoDocLib.DBL
{
    class DBVersion_Factory : shared.DBLBase, IDisposable
    {
        public DBVersion_Factory()
            : base(UTIL.ConnectionSchemaManager.ConnectionString)
        {
        }

        void IDisposable.Dispose()
        {
        }

        public DataTable AllVersions(Guid DBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select a.[Versionid]")
                .AppendLine("   from")
                .AppendLine("   	version a")
                .AppendLine("   where")
                .AppendFormat("   	a.[DBID]='{0}'", DBID)
                .AppendLine("   	and a.[ChangesFound]=1")
                .AppendLine(" order by")
                .AppendLine("   a.[versionid] desc");

            return base.ExecuteSQLToDatatable(sSQL.ToString());
        }

        public void AddSingleVersion(SQLAutoDocLib.BLL.Version oVersion)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("insert into [Version] (")
                .AppendLine("   DBID,")
                .AppendLine("   VersionID,")
                .AppendLine("   ScanStart,")
                .AppendLine("   ChangesFound,")
                .AppendLine("   Change_Date_Time,")
                .AppendLine("   Change_UID) values (")
                .AppendFormat("'{0}',", oVersion.DBID)
                .AppendFormat("'{0}',",oVersion.VersionID)
                .AppendFormat("'{0}',",oVersion.ScanStart)
                .AppendFormat("'{0}',", DBHelper.BoolToBit(oVersion.ChangesFound))
                .AppendFormat("'{0}',",oVersion.ChangeDateTime)
                .AppendFormat("'{0}')",oVersion.ChangeUID);

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public void UpdateSingleVersion(SQLAutoDocLib.BLL.Version oVersion)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("update [Version] set")
                .AppendFormat(" ScanComplete='{0}',", oVersion.ScanComplete)
                .AppendFormat(" ChangesFound='{0}',", DBHelper.BoolToBit(oVersion.ChangesFound))
                .AppendFormat(" Change_Date_Time='{0}',", oVersion.ChangeDateTime)
                .AppendFormat(" Change_UID='{0}'", oVersion.ChangeUID)
                .AppendLine(" where")
                .AppendFormat(" DBID='{0}'", oVersion.DBID)
                .AppendFormat(" and VersionID='{0}'", oVersion.VersionID);

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public void LoadSingleVersion(BLL.Version oVersion)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	*")
            .AppendLine(" from")
            .AppendLine("   [version] a")
            .AppendLine(" where")
            .AppendFormat("	a.[VersionID]='{0}'", oVersion.VersionID);

            DataTable oDT = base.ExecuteSQLToDatatable(sSQL.ToString());

            if (oDT.Rows.Count > 0)
            {
                oVersion.DBID = (Guid)oDT.Rows[0]["DBID"];
                oVersion.ScanStart = oDT.Rows[0]["ScanStart"].ToString();
                oVersion.ScanComplete = DBHelper.NullToString(oDT.Rows[0]["ScanComplete"]);
                oVersion.ChangesFound = (bool)oDT.Rows[0]["ChangesFound"];

                oVersion.ChangeDateTime = oDT.Rows[0]["change_date_time"].ToString();
                oVersion.ChangeUID = oDT.Rows[0]["change_uid"].ToString();

            }
            else
                oVersion = null;
        }
    }
}
