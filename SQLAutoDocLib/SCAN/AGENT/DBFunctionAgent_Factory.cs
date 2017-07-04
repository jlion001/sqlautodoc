using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Text.RegularExpressions;
using System.Xml;

namespace SQLAutoDocLib.SCAN.AGENT
{
    class DBFunctionAgent_Factory : SQLAutoDocLib.SCAN.shared.DBLBase, IDisposable
    {
        public DBFunctionAgent_Factory()
            :base("")
        {

        }

        public DBFunctionAgent_Factory(string sConnectionString)
            : base(sConnectionString)
        {
        }

        void IDisposable.Dispose()
        {
        }

        public DataTable GetList()
        {
            StringBuilder sSQL=new StringBuilder();
            sSQL.AppendLine("select")
                .AppendLine("	a.[name]")
                .AppendLine(" from")
                .AppendLine("	sys.all_objects a")
                .AppendLine(" where")
                .AppendLine("	a.type in ('FN','TF','AF')")
                .AppendLine("   and a.is_ms_shipped=0")
                .AppendLine(" order by")
                .AppendLine("	a.[name]");

            return ExecuteSQLToDatatable(sSQL.ToString());
        }

        /// <summary>
        /// Get an xml representation of the schema of a specified table.
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public string GetFunctionSchema(string FunctionName)
        {
            string sResult = "";

            List<OdbcParameter> oParms = new List<OdbcParameter>();
            oParms.Add(new OdbcParameter(name: "@name", value: FunctionName));

            object oResult = base.ExecuteProcToScalar(sProcName: "{call #pGetFunctionSchema (?)}", oParms: oParms);

            if (oResult != null && oResult != System.DBNull.Value)
            {
                sResult = oResult.ToString();
                sResult = Regex.Replace(sResult, @"[^\u0000-\u007F]", string.Empty);
            }
            
            return sResult;
        }

        #region TemporaryStoredProcedures
        public void make_TemporaryStoredProcedures()
        {
            make_pGetSchema();
        }

        private void make_pGetSchema()
        {
            StringBuilder sSQL = new StringBuilder();

            sSQL.AppendLine("create proc #pGetFunctionSchema(@name varchar(255)) as")
            .AppendLine(" declare @result nvarchar(max)")
            .AppendLine(" set @result=convert(nvarchar(max),(")
            .AppendLine(" select")
            .AppendLine(" 	a.[name] '@name',")
            .AppendLine(" 	(")
            .AppendLine(" 	select")
            .AppendLine(" 		b.[name] '@colname',")
            .AppendLine(" 		b.[parameter_id] '@parmid',")
            .AppendLine(" 		t.[name] '@type',")
            .AppendLine(" 		b.[is_output] '@is_output',")
            .AppendLine(" 		b.[has_default_value] '@has_default',")
            .AppendLine(" 		b.[default_value] '@default_value',")
            .AppendLine(" 		b.[max_length] '@max_length',")
            .AppendLine(" 		b.[precision] '@precision',")
            .AppendLine(" 		b.[scale] '@scale'")
            .AppendLine(" 	from")
            .AppendLine(" 		sys.parameters b")
            .AppendLine(" 			join sys.types AS t ON t.user_type_id=b.user_type_id")
            .AppendLine(" 	where")
            .AppendLine(" 		b.[object_id]=a.[OBJECT_ID]")
            .AppendLine(" 	order by")
            .AppendLine(" 		b.[parameter_id]")
            .AppendLine(" 	for xml path('column'),type")
            .AppendLine(" 	) 'parameters',")
            .AppendLine(" 	(")
            .AppendLine(" 		select 1")	
            .AppendLine(" 	) 'definition'")
            .AppendLine("  from")
            .AppendLine(" 	sys.objects a")
            .AppendLine("  where")
            .AppendLine(" 	a.[name]=@name")
            .AppendLine(" 	and a.[type] in ('fn','tf')")	
            .AppendLine("  for xml path('function'),root('schema'),elements xsinil")
            .AppendLine(" ))")
            .AppendLine(" declare @definition nvarchar(max)")
            .AppendLine(" select")
            .AppendLine(" 	@definition=b.[definition]")
            .AppendLine(" from")
            .AppendLine(" 	sys.objects a")
            .AppendLine(" 		join sys.all_sql_modules b")
            .AppendLine(" 			on b.[object_id]=a.[object_id]")
            .AppendLine(" where")
            .AppendLine(" 	a.[name]=@name")
            .AppendLine(" 	and a.[type] in ('fn','tf')")
            .AppendLine(" select")
            .AppendLine(" 	replace(@result,'<definition>1</definition>','<definition><![CDATA[' + @definition + ']]></definition>') 'result'");
 
            base.ExecuteNonQuery(sSQL.ToString());
        }

        #endregion

        public string Reconstitute(BLL.Function oFunction)
        {
            StringBuilder SQL = new StringBuilder();

            XmlDocument oDoc = new XmlDocument();
            oDoc.LoadXml(oFunction.Configuration);

            SQL.AppendLine("if exists(select 1 from sys.objects where type in ('fn','tf') and name='" + oFunction.Name + "') drop function " + oFunction.Name);
            SQL.AppendLine("GO");

            //Get procedure definition
            XmlNode oDef = oDoc.SelectSingleNode("/schema/function/definition");
            string sViewDef = oDef.InnerText;

            SQL.AppendLine(sViewDef);

            SQL.AppendLine("GO");

            return SQL.ToString();
        }
    }
}
