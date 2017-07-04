using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SQLAutoDocLib.UTIL;

namespace SQLAutoDocLib.DBL
{
    class DBTrigger_Factory : shared.DBLBase, IDisposable
    {
       public DBTrigger_Factory()
            : base(UTIL.ConnectionSchemaManager.ConnectionString)
        {
        }

        void IDisposable.Dispose()
        {
        }

        public DataTable GetTriggerByName(Guid DBID, string TriggerName)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select a.[Triggerid]")
                .AppendLine("   from")
                .AppendLine("   	dbTrigger a")
                .AppendLine("   where")
                .AppendFormat("   	a.[DBID]='{0}'", DBID)
                .AppendFormat("     and a.[name]='{0}'", TriggerName)
                .AppendLine("   	and a.[CurrentlyExists]=1")
                .AppendLine(" order by")
                .AppendLine("   a.[name]");

            return base.ExecuteSQLToDatatable(sSQL.ToString());
        }

        //TODO: p_GetTriggerForVersion proc not returning rows.
        public DataTable GetTriggerByName(Guid DBID, string TriggerName,string VersionID)
        {
            List<SqlParameter> oParms = new List<SqlParameter>();
            oParms.Add(new SqlParameter(parameterName: "@dbid", value: DBID));
            oParms.Add(new SqlParameter(parameterName: "@name", value: TriggerName));
            oParms.Add(new SqlParameter(parameterName: "@versionid", value: VersionID));

            return base.ExecuteProcToDatatable(ProcName: "p_GetTriggerForVersion", oParms: oParms);

        }

        public DataTable AllTriggers(Guid DBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select a.[Triggerid]")
                .AppendLine("   from")
                .AppendLine("   	dbTrigger a")
                .AppendLine("   where")
                .AppendFormat("   	a.[DBID]='{0}'", DBID)
                .AppendLine("   	and a.[CurrentlyExists]=1")
                .AppendLine(" order by")
                .AppendLine("   a.[name]");

            return base.ExecuteSQLToDatatable(sSQL.ToString());
        }

        public DataTable AllTriggers(Guid DBID, string VersionID, bool ChangedOnly)
        {
            List<SqlParameter> oParms = new List<SqlParameter>();
            oParms.Add(new SqlParameter(parameterName: "@dbid", value: DBID));
            oParms.Add(new SqlParameter(parameterName: "@versionid", value: VersionID));
            oParms.Add(new SqlParameter("@changed_only", ChangedOnly));

            return base.ExecuteProcToDatatable(ProcName: "p_GetAllTriggersForVersion", oParms: oParms);
        }

        public void DeleteIncompleteHistory(Guid sDBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("delete b")
                .AppendLine("   from")
                .AppendLine("   	dbTrigger a")
                .AppendLine("   		join dbTriggerHistory b on b.[TriggerID]=a.[TriggerID]")
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
            .AppendLine("	dbTrigger a")
            .AppendLine("where")
            .AppendFormat("  a.[DBID]='{0}'", sDBID);

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public DataTable FindUnfound(Guid DBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	a.[triggerid]")
            .AppendLine("from")
            .AppendLine("	dbTrigger a")
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
            .AppendLine("	dbTrigger a")
            .AppendLine("where")
            .AppendFormat(" a.[DBID]='{0}'", sDBID)
            .AppendLine("	and a.[ScanInProgress]=1")
            .AppendLine("	and a.[CurrentlyExists]=1");

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public Guid FindWithSchemaMatch(Guid DBID, string sName, string sSchemaXML, out bool IsMatch)
        {
            Guid oFoundTrigger = Guid.Empty;

            List<SqlParameter> oList = new List<SqlParameter>();

            oList.Add(new SqlParameter(parameterName: "@dbid", value: DBID));
            oList.Add(new SqlParameter(parameterName: "@name", value: sName));
            oList.Add(new SqlParameter(parameterName: "@schema", value: sSchemaXML));

            SqlParameter oIsMatch = new SqlParameter("@IsMatch", false);
            oIsMatch.DbType = DbType.Boolean;
            oIsMatch.Direction = ParameterDirection.Output;
            oList.Add(oIsMatch);

            DataTable oDT = base.ExecuteProcToDatatable(ProcName: "pCompareTriggerSchema", oParms: oList);

            if (oDT.Rows.Count > 0 && oDT.Rows[0][0] != System.DBNull.Value)
            {
                oFoundTrigger = (Guid)oDT.Rows[0][0];

                IsMatch = (bool)oIsMatch.Value;
            }
            else
                IsMatch = false;

            return oFoundTrigger;
        }

        public void LoadSingleTrigger(BLL.Trigger oTriggerID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	*")
            .AppendLine(" from")
            .AppendLine("   [dbTrigger] a")
            .AppendLine(" where")
            .AppendFormat("	a.[TriggerID]='{0}'", oTriggerID.TriggerID);

            DataTable oDT = base.ExecuteSQLToDatatable(sSQL.ToString());

            if (oDT.Rows.Count > 0)
            {
                oTriggerID.DBID = (Guid)oDT.Rows[0]["DBID"];
                oTriggerID.Name = oDT.Rows[0]["name"].ToString();
                oTriggerID.Description = DBHelper.NullToString(oDT.Rows[0]["description"]);
                oTriggerID.Configuration = DBHelper.NullToString(oDT.Rows[0]["configuration"]);
                oTriggerID.FindFirstVersion = DBHelper.NullToString(oDT.Rows[0]["firstfindversion"]);
                oTriggerID.FindLastVersion = DBHelper.NullToString(oDT.Rows[0]["lastfindversion"]);
                oTriggerID.ScanInProgress = (bool)(oDT.Rows[0]["scaninprogress"]);
                oTriggerID.CurrentlyExists = (bool)(oDT.Rows[0]["currentlyexists"]);
                oTriggerID.ChangedInLastScan = (bool)(oDT.Rows[0]["changedinlastscan"]);

                oTriggerID.ChangeDateTime = oDT.Rows[0]["change_date_time"].ToString();
                oTriggerID.ChangeUID = oDT.Rows[0]["change_uid"].ToString();

                oTriggerID.ConfigurationUpdated = false;
            }
            else
                oTriggerID = null;
        }

        public void InsertSingleTrigger(BLL.Trigger oTriggerID)
        {
            List<SqlParameter> oParms = new List<SqlParameter>();
            oParms.Add(new SqlParameter("@DBID", oTriggerID.DBID));
            oParms.Add(new SqlParameter("@TriggerID", oTriggerID.TriggerID));
            oParms.Add(new SqlParameter("@Name", oTriggerID.Name));
            oParms.Add(new SqlParameter("@Description", oTriggerID.Description));
            oParms.Add(new SqlParameter("@Configuration", oTriggerID.Configuration));
            oParms.Add(new SqlParameter("@FirstFindVersion", oTriggerID.FindFirstVersion));
            oParms.Add(new SqlParameter("@LastFindVersion", oTriggerID.FindLastVersion));
            oParms.Add(new SqlParameter("@ScanInProgress", oTriggerID.ScanInProgress));
            oParms.Add(new SqlParameter("@CurrentlyExists", oTriggerID.CurrentlyExists));
            oParms.Add(new SqlParameter("@ChangedInLastScan", oTriggerID.ChangedInLastScan));
            oParms.Add(new SqlParameter("@ChangeDateTime", oTriggerID.ChangeDateTime));
            oParms.Add(new SqlParameter("@ChangeUID", oTriggerID.ChangeUID));

            base.ExecuteNonQuery("InsertTrigger", oParms);
        }

        public void UpdateSingleTrigger(BLL.Trigger oTriggerID)
        {
            List<SqlParameter> oParms = new List<SqlParameter>();
            oParms.Add(new SqlParameter("@TriggerID", oTriggerID.TriggerID));
            oParms.Add(new SqlParameter("@Name", oTriggerID.Name));
            oParms.Add(new SqlParameter("@Description", oTriggerID.Description));
            oParms.Add(new SqlParameter("@FirstFindVersion", oTriggerID.FindFirstVersion));
            oParms.Add(new SqlParameter("@LastFindVersion", oTriggerID.FindLastVersion));
            oParms.Add(new SqlParameter("@ScanInProgress", oTriggerID.ScanInProgress));
            oParms.Add(new SqlParameter("@CurrentlyExists", oTriggerID.CurrentlyExists));
            oParms.Add(new SqlParameter("@ChangedInLastScan", oTriggerID.ChangedInLastScan));
            oParms.Add(new SqlParameter("@ChangeDateTime", oTriggerID.ChangeDateTime));
            oParms.Add(new SqlParameter("@ChangeUID", oTriggerID.ChangeUID));

            if (oTriggerID.ConfigurationUpdated == true)
            {
                SqlParameter oConfiguration = new SqlParameter("@configuration", SqlDbType.NVarChar, -1);
                oConfiguration.Value = oTriggerID.Configuration;
                oParms.Add(oConfiguration);
            }

            base.ExecuteNonQuery("UpdateTrigger", oParms);
        }
    }
}
