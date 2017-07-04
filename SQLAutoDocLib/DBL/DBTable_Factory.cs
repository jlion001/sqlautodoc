using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SQLAutoDocLib.UTIL;

namespace SQLAutoDocLib.DBL
{
    class DBTable_Factory : shared.DBLBase,IDisposable
    {
        public DBTable_Factory()
            : base(UTIL.ConnectionSchemaManager.ConnectionString)
        {
        }

        void IDisposable.Dispose()
        {
        }

        public DataTable GetTableByName(Guid DBID,string TableName)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select a.[tableid]")
                .AppendLine("   from")
                .AppendLine("   	dbTable a")
                .AppendLine("   where")
                .AppendFormat("   	a.[DBID]='{0}'", DBID)
                .AppendFormat("     and a.[name]='{0}'",TableName)
                .AppendLine(" order by")
                .AppendLine("   a.[name]");

            return base.ExecuteSQLToDatatable(sSQL.ToString());
        }

        //TODO: p_GetTableForVersion proc not returning rows.
        public DataTable GetTableByName(Guid DBID, string TableName, string VersionID)
        {
            List<SqlParameter> oParms = new List<SqlParameter>();
            oParms.Add(new SqlParameter(parameterName: "@dbid", value: DBID));
            oParms.Add(new SqlParameter(parameterName: "@name", value: TableName));
            oParms.Add(new SqlParameter(parameterName: "@versionid", value: VersionID));

            return base.ExecuteProcToDatatable(ProcName: "p_GetTableForVersion", oParms: oParms);

        }

        public DataTable AllTables(Guid DBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select a.[tableid]")
                .AppendLine("   from")
                .AppendLine("   	dbTable a")
                .AppendLine("   where")
                .AppendFormat("   	a.[DBID]='{0}'", DBID)
                .AppendLine("   	and a.[CurrentlyExists]=1")
                .AppendLine(" order by")
                .AppendLine("   a.[name]");

            return base.ExecuteSQLToDatatable(sSQL.ToString());
        }

        public DataTable AllTables(Guid DBID,string VersionID,bool ChangedOnly)
        {
            List<SqlParameter> oList = new List<SqlParameter>();
            oList.Add(new SqlParameter(parameterName: "@dbid", value: DBID));
            oList.Add(new SqlParameter("@versionid", VersionID));
            oList.Add(new SqlParameter("@changed_only", ChangedOnly));

            return base.ExecuteProcToDatatable(ProcName: "p_GetAllTablesforVersion", oParms: oList);
        }

        public void MarkUnfoundAsDeleted(Guid DBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("update a set")
            .AppendLine("	a.[CurrentlyExists]=0,")
            .AppendLine("	a.[ScanInProgress]=0")
            .AppendLine("from")
            .AppendLine("	dbTable a")
            .AppendLine("where")
            .AppendFormat(" a.[DBID]='{0}'", DBID)
            .AppendLine("	and a.[ScanInProgress]=1")
            .AppendLine("	and a.[CurrentlyExists]=1");

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public void DeleteIncompleteHistory(Guid DBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("delete b")
                .AppendLine("   from")
                .AppendLine("   	dbTable a")
                .AppendLine("   		join dbTableHistory b on b.[TableID]=a.[TableID]")
                .AppendLine("   		join [version] c on c.[VersionID]=b.[VersionID]")
                .AppendLine("   							and c.[ScanComplete] is null")
                .AppendLine("   where")
                .AppendFormat("   	a.[DBID]='{0}'", DBID)
                .AppendLine("   	and a.[ScanInProgress]=1");

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public void MarkScanningInProgress(Guid DBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("update a set")
            .AppendLine("	a.[ScanInProgress]=1")
            .AppendLine("from")
            .AppendLine("	dbTable a")
            .AppendLine("where")
            .AppendFormat("   	a.[DBID]='{0}'", DBID);

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public DataTable FindUnfound(Guid DBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	a.[tableid]")
            .AppendLine("from")
            .AppendLine("	dbTable a")
            .AppendLine("where")
            .AppendFormat(" a.[DBID]='{0}'", DBID)
            .AppendLine("	and a.[ScanInProgress]=1")
		    .AppendLine("	and a.[CurrentlyExists]=1");

            return base.ExecuteSQLToDatatable(sSQL.ToString());
        }

        public Guid FindWithSchemaMatch(Guid DBID,string sName, string sSchemaXML, out bool IsMatch)
        {
            Guid oFoundTable = Guid.Empty;

            List<SqlParameter> oList = new List<SqlParameter>();

            oList.Add(new SqlParameter(parameterName: "@dbid", value: DBID));
            oList.Add(new SqlParameter(parameterName: "@name", value: sName));
            oList.Add(new SqlParameter(parameterName: "@schema", value: sSchemaXML));

            SqlParameter oIsMatch = new SqlParameter("@IsMatch",false);
            oIsMatch.DbType = DbType.Boolean;
            oIsMatch.Direction=ParameterDirection.Output;
            oList.Add(oIsMatch);

            DataTable oDT=base.ExecuteProcToDatatable(ProcName: "pCompareTableSchema", oParms: oList);

            if (oDT.Rows.Count > 0 && oDT.Rows[0][0]!=System.DBNull.Value)
            {
                oFoundTable = (Guid)oDT.Rows[0][0];

                IsMatch=(bool)oIsMatch.Value;
            }
            else
                IsMatch = false;

            return oFoundTable;
        }

        public void LoadSingleTable(BLL.Table oTableID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	*")
            .AppendLine(" from")
            .AppendLine("   [dbtable] a")
            .AppendLine(" where")
            .AppendFormat("	a.[TableID]='{0}'", oTableID.TableID);

            DataTable oDT = base.ExecuteSQLToDatatable(sSQL.ToString());

            if (oDT.Rows.Count > 0)
            {
                oTableID.LoadFromRow(oDT.Rows[0]);
            }
            else
                oTableID = null;
        }

        public void InsertSingleTable(BLL.Table oTableID)
        {
            List<SqlParameter> oParms = new List<SqlParameter>();
            oParms.Add(new SqlParameter("@DBID", oTableID.DBID));
            oParms.Add(new SqlParameter("@TableID", oTableID.TableID));
            oParms.Add(new SqlParameter("@Name", oTableID.Name));
            oParms.Add(new SqlParameter("@Description", oTableID.Description));
            oParms.Add(new SqlParameter("@Configuration", oTableID.Configuration));
            oParms.Add(new SqlParameter("@FirstFindVersion", oTableID.FindFirstVersion));
            oParms.Add(new SqlParameter("@LastFindVersion", oTableID.FindLastVersion));
            oParms.Add(new SqlParameter("@ScanInProgress", oTableID.ScanInProgress));
            oParms.Add(new SqlParameter("@CurrentlyExists", oTableID.CurrentlyExists));
            oParms.Add(new SqlParameter("@ChangedInLastScan", oTableID.ChangedInLastScan));
            oParms.Add(new SqlParameter("@ChangeDateTime", oTableID.ChangeDateTime));
            oParms.Add(new SqlParameter("@ChangeUID", oTableID.ChangeUID));

            base.ExecuteNonQuery("InsertTable", oParms);
        }

        public void UpdateSingleTable(BLL.Table oTableID)
        {
            List<SqlParameter> oParms = new List<SqlParameter>();
            oParms.Add(new SqlParameter("@TableID", oTableID.TableID));
            oParms.Add(new SqlParameter("@Name", oTableID.Name));
            oParms.Add(new SqlParameter("@Description", oTableID.Description));
            oParms.Add(new SqlParameter("@FirstFindVersion", oTableID.FindFirstVersion));
            oParms.Add(new SqlParameter("@LastFindVersion", oTableID.FindLastVersion));
            oParms.Add(new SqlParameter("@ScanInProgress", oTableID.ScanInProgress));
            oParms.Add(new SqlParameter("@CurrentlyExists", oTableID.CurrentlyExists));
            oParms.Add(new SqlParameter("@ChangedInLastScan", oTableID.ChangedInLastScan));
            oParms.Add(new SqlParameter("@ChangeDateTime", oTableID.ChangeDateTime));
            oParms.Add(new SqlParameter("@ChangeUID", oTableID.ChangeUID));

            if (oTableID.ConfigurationUpdated == true)
            {
                SqlParameter oConfiguration = new SqlParameter("@configuration", SqlDbType.NVarChar, -1);
                oConfiguration.Value = oTableID.Configuration;
                oParms.Add(oConfiguration);
            }

            base.ExecuteNonQuery("UpdateTable", oParms);
        }
    }
}
