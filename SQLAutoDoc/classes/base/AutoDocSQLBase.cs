using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Data;

namespace SQLAutoDoc.classes
{
    public class AutoDocSQLBase : AutoDocBase
	{


        public AutoDocSQLBase(string sUserID, string sConnectionString)
            : base(sUserID, sConnectionString)
        {
            m_UserID = sUserID;
        }

        public bool GetServerInfo(out string sServerName, out string sDatabaseName)
        {
            bool bResult = true;

            sServerName = "";
            sDatabaseName = "";

            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine(" select")
	            .AppendLine(" @@SERVERNAME 'server',")
	            .AppendLine(" DB_NAME() 'database'");

            try
            {
                System.Data.DataTable oDT = ExecuteSQLToDatatable(sSQL.ToString());

                sServerName = oDT.Rows[0]["server"].ToString();
                sDatabaseName = oDT.Rows[0]["database"].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());

                bResult = false;
            }

            return bResult;
        }

        private string RemoveExtraSQLText(string sSQL, ScriptType iType)
        {
            string sSearchText = "";
            switch (iType)
            {
                case ScriptType.FUNCTION:
                    sSearchText = "CREATE FUNCTION";
                    break;

                case ScriptType.PROC:
                    sSearchText = "CREATE PROC";
                    break;

                case ScriptType.TABLE:
                    sSearchText = "CREATE TABLE";
                    break;

                case ScriptType.VIEW:
                    sSearchText = "CREATE VIEW";
                    break;
            }

            int iStartPos = sSQL.ToUpper().IndexOf(sSearchText);

            return sSQL.Substring(iStartPos,sSQL.Length-iStartPos);
        }

        public List<string> GetViewsInDatabase()
        {
            List<string> oList = new List<string>();

            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine(" select")
                .AppendLine(" 	a.[name]")
                .AppendLine(" from")
                .AppendLine(" 	sysobjects a")
                .AppendLine(" where")
                .AppendLine(" 	a.[type]='v'")
                .AppendLine(" order by")
                .AppendLine(" 	a.[name]");

            return oList;
        }

        public List<string> GetFunctionsInDatabase()
        {
            List<string> oList = new List<string>();

            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine(" select")
                .AppendLine(" 	a.[name]")
                .AppendLine(" from")
                .AppendLine(" 	sysobjects a")
                .AppendLine(" where")
                .AppendLine(" 	a.[type] in ('tf','fn')")
                .AppendLine(" order by")
                .AppendLine(" 	a.[name]");

            return oList;
        }

        public DataRow GetInfoForTableField(
                                    string sTable,
                                    string sField)
        {
            DataRow oDR = null;

            StringBuilder sSQL = new StringBuilder();

            sSQL.AppendLine(" select")
                .AppendLine(" 	t.[name] 'Type',")
                .AppendLine(" 	b.[max_length] 'Size'")
                .AppendLine(" from")
                .AppendLine(" 	sys.objects a")
                .AppendLine(" 		join sys.columns b on b.[object_id]=a.[object_id]")
                .AppendLine(" 		join sys.types AS t ON t.user_type_id=b.user_type_id")
                .AppendLine(" where")
                .AppendLine(" 	a.[name]='" + sTable + "'")
                .AppendLine(" 	and b.[name]='" + sField + "'");


            DataTable oDT = base.ExecuteSQLToDatatable(sSQL.ToString());

            if (oDT.Rows.Count > 0)
                oDR = oDT.Rows[0];

            return oDR;
        }
	}
}
