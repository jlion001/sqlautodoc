using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SQLAutoDocLib.UTIL;

namespace SQLAutoDocLib.DBL
{
    class DBFunction_Factory : shared.DBLBase, IDisposable
    {
        public DBFunction_Factory()
            : base(UTIL.ConnectionSchemaManager.ConnectionString)
        {
        }

        void IDisposable.Dispose()
        {
        }

        public DataTable GetFunctionByName(Guid DBID, string FunctionName)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select a.[Functionid]")
                .AppendLine("   from")
                .AppendLine("   	dbFunction a")
                .AppendLine("   where")
                .AppendFormat("   	a.[DBID]='{0}'", DBID)
                .AppendFormat("     and a.[name]='{0}'", FunctionName)
                .AppendLine("   	and a.[CurrentlyExists]=1")
                .AppendLine(" order by")
                .AppendLine("   a.[name]");

            return base.ExecuteSQLToDatatable(sSQL.ToString());
        }

        //TODO: p_GetFunctionForVersion proc not returning rows.
        public DataTable GetFunctionByName(Guid DBID, string FunctionName, string VersionID)
        {
            List<SqlParameter> oParms = new List<SqlParameter>();
            oParms.Add(new SqlParameter(parameterName: "@dbid", value: DBID));
            oParms.Add(new SqlParameter(parameterName: "@name", value: FunctionName));
            oParms.Add(new SqlParameter(parameterName: "@versionid", value: VersionID));

            return base.ExecuteProcToDatatable(ProcName: "p_GetFunctionForVersion", oParms: oParms);
        }

        public DataTable AllFunctions(Guid DBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select a.[functionid]")
                .AppendLine("   from")
                .AppendLine("   	dbFunction a")
                .AppendLine("   where")
                .AppendFormat("   	a.[DBID]='{0}'", DBID)
                .AppendLine("   	and a.[CurrentlyExists]=1")
                .AppendLine(" order by")
                .AppendLine("   a.[name]");

            return base.ExecuteSQLToDatatable(sSQL.ToString());
        }

        public DataTable AllFunctions(Guid DBID, string VersionID, bool ChangedOnly)
        {
            List<SqlParameter> oList = new List<SqlParameter>();
            oList.Add(new SqlParameter(parameterName: "@dbid", value: DBID));
            oList.Add(new SqlParameter("@versionid", VersionID));
            oList.Add(new SqlParameter("@changed_only", ChangedOnly));

            return base.ExecuteProcToDatatable(ProcName: "p_GetAllFunctionsforVersion", oParms: oList);
        }

        public void DeleteIncompleteHistory(Guid sDBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("delete b")
                .AppendLine("   from")
                .AppendLine("   	dbFunction a")
                .AppendLine("   		join dbFunctionHistory b on b.[FunctionID]=a.[FunctionID]")
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
            .AppendLine("	dbFunction a")
            .AppendLine("where")
            .AppendFormat("   	a.[DBID]='{0}'", sDBID);

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public DataTable FindUnfound(Guid DBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	a.[functionid]")
            .AppendLine("from")
            .AppendLine("	dbFunction a")
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
            .AppendLine("	dbFunction a")
            .AppendLine("where")
            .AppendFormat(" a.[DBID]='{0}'", sDBID)
            .AppendLine("	and a.[ScanInProgress]=1")
            .AppendLine("	and a.[CurrentlyExists]=1");

            base.ExecuteNonQuery(sSQL.ToString());
        }


        public Guid FindWithSchemaMatch(Guid DBID, string sName, string sSchemaXML, out bool IsMatch)
        {
            Guid oFoundFunction = Guid.Empty;

            List<SqlParameter> oList = new List<SqlParameter>();

            oList.Add(new SqlParameter(parameterName: "@dbid", value: DBID));
            oList.Add(new SqlParameter(parameterName: "@name", value: sName));
            oList.Add(new SqlParameter(parameterName: "@schema", value: sSchemaXML));

            SqlParameter oIsMatch = new SqlParameter("@IsMatch", false);
            oIsMatch.DbType = DbType.Boolean;
            oIsMatch.Direction = ParameterDirection.Output;
            oList.Add(oIsMatch);

            DataTable oDT = base.ExecuteProcToDatatable(ProcName: "pCompareFunctionSchema", oParms: oList);

            if (oDT.Rows.Count > 0 && oDT.Rows[0][0] != System.DBNull.Value)
            {
                oFoundFunction = (Guid)oDT.Rows[0][0];

                IsMatch = (bool)oIsMatch.Value;
            }
            else
                IsMatch = false;

            return oFoundFunction;
        }

        public void LoadSingleFunction(BLL.Function oFunctionID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	*")
            .AppendLine(" from")
            .AppendLine("   [dbFunction] a")
            .AppendLine(" where")
            .AppendFormat("	a.[FunctionID]='{0}'", oFunctionID.FunctionID);

            DataTable oDT = base.ExecuteSQLToDatatable(sSQL.ToString());

            if (oDT.Rows.Count > 0)
            {
                oFunctionID.DBID = (Guid)oDT.Rows[0]["DBID"];
                oFunctionID.Name = oDT.Rows[0]["name"].ToString();
                oFunctionID.Description = DBHelper.NullToString(oDT.Rows[0]["description"]);
                oFunctionID.Configuration = DBHelper.NullToString(oDT.Rows[0]["configuration"]);
                oFunctionID.FindFirstVersion = DBHelper.NullToString(oDT.Rows[0]["firstfindversion"]);
                oFunctionID.FindLastVersion = DBHelper.NullToString(oDT.Rows[0]["lastfindversion"]);
                oFunctionID.ScanInProgress = (bool)(oDT.Rows[0]["scaninprogress"]);
                oFunctionID.CurrentlyExists = (bool)(oDT.Rows[0]["currentlyexists"]);
                oFunctionID.ChangedInLastScan = (bool)(oDT.Rows[0]["changedinlastscan"]);

                oFunctionID.ChangeDateTime = oDT.Rows[0]["change_date_time"].ToString();
                oFunctionID.ChangeUID = oDT.Rows[0]["change_uid"].ToString();

                oFunctionID.ConfigurationUpdated = false;
            }
            else
                oFunctionID = null;
        }

        public void InsertSingleFunction(BLL.Function oFunctionID)
        {
            List<SqlParameter> oParms = new List<SqlParameter>();
            oParms.Add(new SqlParameter("@DBID", oFunctionID.DBID));
            oParms.Add(new SqlParameter("@FunctionID", oFunctionID.FunctionID));
            oParms.Add(new SqlParameter("@Name", oFunctionID.Name));
            oParms.Add(new SqlParameter("@Description", oFunctionID.Description));
            oParms.Add(new SqlParameter("@Configuration", oFunctionID.Configuration));
            oParms.Add(new SqlParameter("@FirstFindVersion", oFunctionID.FindFirstVersion));
            oParms.Add(new SqlParameter("@LastFindVersion", oFunctionID.FindLastVersion));
            oParms.Add(new SqlParameter("@ScanInProgress", oFunctionID.ScanInProgress));
            oParms.Add(new SqlParameter("@CurrentlyExists", oFunctionID.CurrentlyExists));
            oParms.Add(new SqlParameter("@ChangedInLastScan", oFunctionID.ChangedInLastScan));
            oParms.Add(new SqlParameter("@ChangeDateTime", oFunctionID.ChangeDateTime));
            oParms.Add(new SqlParameter("@ChangeUID", oFunctionID.ChangeUID));

            base.ExecuteNonQuery("InsertFunction", oParms);
        }

        public void UpdateSingleFunction(BLL.Function oFunctionID)
        {
            List<SqlParameter> oParms = new List<SqlParameter>();
            oParms.Add(new SqlParameter("@FunctionID", oFunctionID.FunctionID));
            oParms.Add(new SqlParameter("@Name", oFunctionID.Name));
            oParms.Add(new SqlParameter("@Description", oFunctionID.Description));
            oParms.Add(new SqlParameter("@FirstFindVersion", oFunctionID.FindFirstVersion));
            oParms.Add(new SqlParameter("@LastFindVersion", oFunctionID.FindLastVersion));
            oParms.Add(new SqlParameter("@ScanInProgress", oFunctionID.ScanInProgress));
            oParms.Add(new SqlParameter("@CurrentlyExists", oFunctionID.CurrentlyExists));
            oParms.Add(new SqlParameter("@ChangedInLastScan", oFunctionID.ChangedInLastScan));
            oParms.Add(new SqlParameter("@ChangeDateTime", oFunctionID.ChangeDateTime));
            oParms.Add(new SqlParameter("@ChangeUID", oFunctionID.ChangeUID));

            if (oFunctionID.ConfigurationUpdated == true)
            {
                SqlParameter oConfiguration = new SqlParameter("@configuration", SqlDbType.NVarChar, -1);
                oConfiguration.Value = oFunctionID.Configuration;
                oParms.Add(oConfiguration);
            }

            base.ExecuteNonQuery("UpdateFunction", oParms);
        }
    }
}
