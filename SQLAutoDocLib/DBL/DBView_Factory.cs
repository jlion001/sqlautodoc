using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SQLAutoDocLib.UTIL;

namespace SQLAutoDocLib.DBL
{
    class DBView_Factory : shared.DBLBase, IDisposable
    {
       public DBView_Factory()
            : base(UTIL.ConnectionSchemaManager.ConnectionString)
        {
        }

        void IDisposable.Dispose()
        {
        }

        public DataTable GetViewByName(Guid DBID, string ViewName)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select a.[viewid]")
                .AppendLine("   from")
                .AppendLine("   	dbView a")
                .AppendLine("   where")
                .AppendFormat("   	a.[DBID]='{0}'", DBID)
                .AppendFormat("     and a.[name]='{0}'", ViewName)
                .AppendLine("   	and a.[CurrentlyExists]=1")
                .AppendLine(" order by")
                .AppendLine("   a.[name]");

            return base.ExecuteSQLToDatatable(sSQL.ToString());
        }

        //TODO: p_GetViewForVersion proc not returning rows.
        public DataTable GetViewByName(Guid DBID, string ViewName,string VersionID)
        {
            List<SqlParameter> oParms = new List<SqlParameter>();
            oParms.Add(new SqlParameter(parameterName: "@dbid", value: DBID));
            oParms.Add(new SqlParameter(parameterName: "@name", value: ViewName));
            oParms.Add(new SqlParameter(parameterName: "@versionid", value: VersionID));

            return base.ExecuteProcToDatatable(ProcName: "p_GetViewForVersion", oParms: oParms);

        }

        public DataTable AllViews(Guid DBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select a.[Viewid]")
                .AppendLine("   from")
                .AppendLine("   	dbView a")
                .AppendLine("   where")
                .AppendFormat("   	a.[DBID]='{0}'", DBID)
                .AppendLine("   	and a.[CurrentlyExists]=1")
                .AppendLine(" order by")
                .AppendLine("   a.[name]");

            return base.ExecuteSQLToDatatable(sSQL.ToString());
        }

        public DataTable AllViews(Guid DBID, string VersionID, bool ChangedOnly)
        {
            List<SqlParameter> oList = new List<SqlParameter>();
            oList.Add(new SqlParameter(parameterName: "@dbid", value: DBID));
            oList.Add(new SqlParameter("@versionid", VersionID));
            oList.Add(new SqlParameter("@changed_only", ChangedOnly));

            return base.ExecuteProcToDatatable(ProcName: "p_GetAllViewsforVersion", oParms: oList);
        }

        public void DeleteIncompleteHistory(Guid sDBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("delete b")
                .AppendLine("   from")
                .AppendLine("   	dbView a")
                .AppendLine("   		join dbViewHistory b on b.[ViewID]=a.[ViewID]")
                .AppendLine("   		join [version] c on c.[VersionID]=b.[VersionID]")
                .AppendLine("   							and c.[ScanComplete] is null")
                .AppendLine("   where")
                .AppendFormat("   	a.[DBID]='{0}'", sDBID)
                .AppendLine("   	and a.[ScanInProgress]=1");

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public void MarkScanningInProgress(Guid sDBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("update a set")
            .AppendLine("	a.[ScanInProgress]=1")
            .AppendLine("from")
            .AppendLine("	dbView a")
            .AppendLine("where")
            .AppendFormat("   	a.[DBID]='{0}'", sDBID);

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public DataTable FindUnfound(Guid DBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	a.[viewid]")
            .AppendLine("from")
            .AppendLine("	dbView a")
            .AppendLine("where")
            .AppendFormat(" a.[DBID]='{0}'", DBID)
            .AppendLine("	and a.[ScanInProgress]=1")
            .AppendLine("	and a.[CurrentlyExists]=1");

            return base.ExecuteSQLToDatatable(sSQL.ToString());
        }

        public void MarkUnfoundAsDeleted(Guid sDBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("update a set")
            .AppendLine("	a.[CurrentlyExists]=0,")
            .AppendLine("	a.[ScanInProgress]=0")
            .AppendLine("from")
            .AppendLine("	dbView a")
            .AppendLine("where")
            .AppendFormat(" a.[DBID]='{0}'", sDBID)
            .AppendLine("	and a.[ScanInProgress]=1")
            .AppendLine("	and a.[CurrentlyExists]=1");

            base.ExecuteNonQuery(sSQL.ToString());
        }


        public Guid FindWithSchemaMatch(Guid DBID, string sName, string sSchemaXML, out bool IsMatch)
        {
            Guid oFoundView = Guid.Empty;

            List<SqlParameter> oList = new List<SqlParameter>();

            oList.Add(new SqlParameter(parameterName: "@dbid", value: DBID));
            oList.Add(new SqlParameter(parameterName: "@name", value: sName));
            oList.Add(new SqlParameter(parameterName: "@schema", value: sSchemaXML));

            SqlParameter oIsMatch = new SqlParameter("@IsMatch", false);
            oIsMatch.DbType = DbType.Boolean;
            oIsMatch.Direction = ParameterDirection.Output;
            oList.Add(oIsMatch);

            DataTable oDT = base.ExecuteProcToDatatable(ProcName: "pCompareViewSchema", oParms: oList);

            if (oDT.Rows.Count > 0 && oDT.Rows[0][0] != System.DBNull.Value)
            {
                oFoundView = (Guid)oDT.Rows[0][0];

                IsMatch = (bool)oIsMatch.Value;
            }
            else
                IsMatch = false;

            return oFoundView;
        }

        public void LoadSingleView(BLL.View oViewID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	*")
            .AppendLine(" from")
            .AppendLine("   [dbView] a")
            .AppendLine(" where")
            .AppendFormat("	a.[ViewID]='{0}'", oViewID.ViewID);

            DataTable oDT = base.ExecuteSQLToDatatable(sSQL.ToString());

            if (oDT.Rows.Count > 0)
            {
                oViewID.DBID = (Guid)oDT.Rows[0]["DBID"];
                oViewID.Name = oDT.Rows[0]["name"].ToString();
                oViewID.Description = DBHelper.NullToString(oDT.Rows[0]["description"]);
                oViewID.Configuration = DBHelper.NullToString(oDT.Rows[0]["configuration"]);
                oViewID.FindFirstVersion = DBHelper.NullToString(oDT.Rows[0]["firstfindversion"]);
                oViewID.FindLastVersion = DBHelper.NullToString(oDT.Rows[0]["lastfindversion"]);
                oViewID.ScanInProgress = (bool)(oDT.Rows[0]["scaninprogress"]);
                oViewID.CurrentlyExists = (bool)(oDT.Rows[0]["currentlyexists"]);
                oViewID.ChangedInLastScan = (bool)(oDT.Rows[0]["changedinlastscan"]);

                oViewID.ChangeDateTime = oDT.Rows[0]["change_date_time"].ToString();
                oViewID.ChangeUID = oDT.Rows[0]["change_uid"].ToString();

                oViewID.ConfigurationUpdated = false;
            }
            else
                oViewID = null;
        }

        public void InsertSingleView(BLL.View oViewID)
        {
            List<SqlParameter> oParms = new List<SqlParameter>();
            oParms.Add(new SqlParameter("@DBID", oViewID.DBID));
            oParms.Add(new SqlParameter("@ViewID", oViewID.ViewID));
            oParms.Add(new SqlParameter("@Name", oViewID.Name));
            oParms.Add(new SqlParameter("@Description", oViewID.Description));
            oParms.Add(new SqlParameter("@Configuration", oViewID.Configuration));
            oParms.Add(new SqlParameter("@FirstFindVersion", oViewID.FindFirstVersion));
            oParms.Add(new SqlParameter("@LastFindVersion", oViewID.FindLastVersion));
            oParms.Add(new SqlParameter("@ScanInProgress", oViewID.ScanInProgress));
            oParms.Add(new SqlParameter("@CurrentlyExists", oViewID.CurrentlyExists));
            oParms.Add(new SqlParameter("@ChangedInLastScan", oViewID.ChangedInLastScan));
            oParms.Add(new SqlParameter("@ChangeDateTime", oViewID.ChangeDateTime));
            oParms.Add(new SqlParameter("@ChangeUID", oViewID.ChangeUID));

            base.ExecuteNonQuery("InsertView", oParms);
        }

        public void UpdateSingleView(BLL.View oViewID)
        {
            List<SqlParameter> oParms = new List<SqlParameter>();
            oParms.Add(new SqlParameter("@ViewID", oViewID.ViewID));
            oParms.Add(new SqlParameter("@Name", oViewID.Name));
            oParms.Add(new SqlParameter("@Description", oViewID.Description));
            oParms.Add(new SqlParameter("@FirstFindVersion", oViewID.FindFirstVersion));
            oParms.Add(new SqlParameter("@LastFindVersion", oViewID.FindLastVersion));
            oParms.Add(new SqlParameter("@ScanInProgress", oViewID.ScanInProgress));
            oParms.Add(new SqlParameter("@CurrentlyExists", oViewID.CurrentlyExists));
            oParms.Add(new SqlParameter("@ChangedInLastScan", oViewID.ChangedInLastScan));
            oParms.Add(new SqlParameter("@ChangeDateTime", oViewID.ChangeDateTime));
            oParms.Add(new SqlParameter("@ChangeUID", oViewID.ChangeUID));

            if (oViewID.ConfigurationUpdated == true)
            {
                SqlParameter oConfiguration = new SqlParameter("@configuration", SqlDbType.NVarChar, -1);
                oConfiguration.Value = oViewID.Configuration;
                oParms.Add(oConfiguration);
            }

            base.ExecuteNonQuery("UpdateView", oParms);
        }
    }
}
