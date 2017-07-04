using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using SQLAutoDocLib.UTIL;

namespace SQLAutoDocLib.DBL
{
    class DBProcedure_Factory : shared.DBLBase, IDisposable
    {
        public DBProcedure_Factory()
            : base(UTIL.ConnectionSchemaManager.ConnectionString)
        {
        }

        void IDisposable.Dispose()
        {
        }

        public DataTable GetProcedureByName(Guid DBID, string ProcedureName)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select a.[Procedureid]")
                .AppendLine("   from")
                .AppendLine("   	dbProcedure a")
                .AppendLine("   where")
                .AppendFormat("   	a.[DBID]='{0}'", DBID)
                .AppendFormat("     and a.[name]='{0}'", ProcedureName)
                .AppendLine(" order by")
                .AppendLine("   a.[name]");

            return base.ExecuteSQLToDatatable(sSQL.ToString());
        }

        //TODO: p_GetProcedureForVersion proc not returning rows.
        public DataTable GetProcedureByName(Guid DBID, string ProcedureName,string VersionID)
        {
            List<SqlParameter> oParms = new List<SqlParameter>();
            oParms.Add(new SqlParameter(parameterName: "@dbid", value: DBID));
            oParms.Add(new SqlParameter(parameterName: "@name", value: ProcedureName));
            oParms.Add(new SqlParameter(parameterName: "@versionid", value: VersionID));

            return base.ExecuteProcToDatatable(ProcName: "p_GetProcedureForVersion", oParms: oParms);
        }

        public DataTable AllProcedures(Guid DBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select a.[procedureid]")
                .AppendLine("   from")
                .AppendLine("   	dbProcedure a")
                .AppendLine("   where")
                .AppendFormat("   	a.[DBID]='{0}'", DBID)
                .AppendLine("   	and a.[CurrentlyExists]=1")
                .AppendLine(" order by")
                .AppendLine("   a.[name]");

            return base.ExecuteSQLToDatatable(sSQL.ToString());
        }

        public DataTable AllProcedures(Guid DBID, string VersionID, bool ChangedOnly)
        {
            List<SqlParameter> oParms = new List<SqlParameter>();
            oParms.Add(new SqlParameter(parameterName: "@dbid", value: DBID));
            oParms.Add(new SqlParameter(parameterName: "@versionid", value: VersionID));
            oParms.Add(new SqlParameter("@changed_only", ChangedOnly));

            return base.ExecuteProcToDatatable(ProcName: "p_GetAllProceduresForVersion", oParms: oParms);
        }

        public void DeleteIncompleteHistory(Guid sDBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("delete b")
                .AppendLine("   from")
                .AppendLine("   	dbProcedure a")
                .AppendLine("   		join dbProcedureHistory b on b.[ProcedureID]=a.[ProcedureID]")
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
            .AppendLine("	dbProcedure a")
            .AppendLine("where")
            .AppendFormat("   	a.[DBID]='{0}'", sDBID);

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public DataTable FindUnfound(Guid DBID)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	a.[Procedureid]")
            .AppendLine("from")
            .AppendLine("	dbProcedure a")
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
            .AppendLine("	dbProcedure a")
            .AppendLine("where")
            .AppendFormat(" a.[DBID]='{0}'", sDBID)
            .AppendLine("	and a.[ScanInProgress]=1")
            .AppendLine("	and a.[CurrentlyExists]=1");

            base.ExecuteNonQuery(sSQL.ToString());
        }

        public Guid FindWithSchemaMatch(Guid DBID, string sName, string sSchemaXML, out bool IsMatch)
        {
            Guid oFoundProcedure = Guid.Empty;

            List<SqlParameter> oList = new List<SqlParameter>();

            oList.Add(new SqlParameter(parameterName: "@dbid", value: DBID));
            oList.Add(new SqlParameter(parameterName: "@name", value: sName));

            SqlParameter oXML = new SqlParameter("@schema", SqlDbType.NVarChar, -1);
            oXML.Value = sSchemaXML;
            oList.Add(oXML);

            SqlParameter oIsMatch = new SqlParameter("@IsMatch", false);
            oIsMatch.DbType = DbType.Boolean;
            oIsMatch.Direction = ParameterDirection.Output;
            oList.Add(oIsMatch);

            object oValue = base.ExecuteProcToScalar(ProcName: "pCompareProcedureSchema", oParms: ref oList);

            if (oValue != System.DBNull.Value)
            {
                oFoundProcedure = (Guid)oValue;

                IsMatch = (bool)oIsMatch.Value;
            }
            else
                IsMatch = false;


            return oFoundProcedure;
        }

        public void LoadSingleProcedure(BLL.Procedure oProcedure)
        {
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select")
            .AppendLine("	*")
            .AppendLine(" from")
            .AppendLine("   [dbProcedure] a")
            .AppendLine(" where")
            .AppendFormat("	a.[ProcedureID]='{0}'", oProcedure.ProcedureID);

            DataTable oDT = base.ExecuteSQLToDatatable(sSQL.ToString());

            if (oDT.Rows.Count > 0)
            {
                oProcedure.LoadFromRow(oDT.Rows[0]);
            }
            else
                oProcedure = null;
        }

        public void InsertSingleProcedure(BLL.Procedure oProcedureID)
        {
            List<SqlParameter> oParms = new List<SqlParameter>();
            oParms.Add(new SqlParameter("@DBID", oProcedureID.DBID));
            oParms.Add(new SqlParameter("@ProcedureID", oProcedureID.ProcedureID));
            oParms.Add(new SqlParameter("@Name", oProcedureID.Name));
            oParms.Add(new SqlParameter("@Description", oProcedureID.Description));          
            oParms.Add(new SqlParameter("@FirstFindVersion", oProcedureID.FindFirstVersion));
            oParms.Add(new SqlParameter("@LastFindVersion", oProcedureID.FindLastVersion));
            oParms.Add(new SqlParameter("@ScanInProgress", oProcedureID.ScanInProgress));
            oParms.Add(new SqlParameter("@CurrentlyExists", oProcedureID.CurrentlyExists));
            oParms.Add(new SqlParameter("@ChangedInLastScan", oProcedureID.ChangedInLastScan));
            oParms.Add(new SqlParameter("@ChangeDateTime", oProcedureID.ChangeDateTime));
            oParms.Add(new SqlParameter("@ChangeUID", oProcedureID.ChangeUID));

            SqlParameter oConfiguration = new SqlParameter("@configuration", SqlDbType.NVarChar, -1);
            oConfiguration.Value = oProcedureID.Configuration;
            oParms.Add(oConfiguration);

            base.ExecuteNonQuery("InsertProcedure", oParms);
        }

        public void UpdateSingleProcedure(BLL.Procedure oProcedureID)
        {
            List<SqlParameter> oParms = new List<SqlParameter>();
            oParms.Add(new SqlParameter("@ProcedureID", oProcedureID.ProcedureID));
            oParms.Add(new SqlParameter("@Name", oProcedureID.Name));
            oParms.Add(new SqlParameter("@Description", oProcedureID.Description));
            oParms.Add(new SqlParameter("@FirstFindVersion", oProcedureID.FindFirstVersion));
            oParms.Add(new SqlParameter("@LastFindVersion", oProcedureID.FindLastVersion));
            oParms.Add(new SqlParameter("@ScanInProgress", oProcedureID.ScanInProgress));
            oParms.Add(new SqlParameter("@CurrentlyExists", oProcedureID.CurrentlyExists));
            oParms.Add(new SqlParameter("@ChangedInLastScan", oProcedureID.ChangedInLastScan));
            oParms.Add(new SqlParameter("@ChangeDateTime", oProcedureID.ChangeDateTime));
            oParms.Add(new SqlParameter("@ChangeUID", oProcedureID.ChangeUID));

            if (oProcedureID.ConfigurationUpdated == true)
            {
                SqlParameter oConfiguration = new SqlParameter("@configuration", SqlDbType.NVarChar, -1);
                oConfiguration.Value = oProcedureID.Configuration;
                oParms.Add(oConfiguration);
            }

            base.ExecuteNonQuery("UpdateProcedure", oParms);
        }
    }
}
