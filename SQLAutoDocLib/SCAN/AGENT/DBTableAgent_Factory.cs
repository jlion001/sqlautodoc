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
    class DBTableAgent_Factory : SQLAutoDocLib.SCAN.shared.DBLBase, IDisposable
    {
        public DBTableAgent_Factory()
            :base("")
        {

        }

        public DBTableAgent_Factory(string sConnectionString)
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
                .AppendLine("	a.type='U'")
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
        public string GetTableSchema(string TableName)
        {
            string sResult = "";

            List<OdbcParameter> oParms = new List<OdbcParameter>();
            oParms.Add(new OdbcParameter(name:"@name",value:TableName));

            object oResult = base.ExecuteProcToScalar(sProcName: "{call #pGetTableSchema (?)}", oParms: oParms);

            if (oResult!=null && oResult != System.DBNull.Value)
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

            sSQL.AppendLine("create proc #pGetTableSchema(@name varchar(255)) as")
            .AppendLine(" declare @result nvarchar(max)")
            .AppendLine(" set @result=(")
            .AppendLine(" select")
            .AppendLine("	a.[name] '@name',")
            .AppendLine("	(")
            .AppendLine("	select")
            .AppendLine("		b.[name] '@colname',")
            .AppendLine("		b.[column_id] '@colid',")
            .AppendLine("		t.[name] '@type',")
            .AppendLine("		b.[is_identity] '@is_identity',")
            .AppendLine("		b.[is_nullable] '@is_nullable',")
            .AppendLine("		b.[max_length] '@max_length',")
            .AppendLine("		b.[precision] '@precision',")
            .AppendLine("		b.[scale] '@scale',")
            .AppendLine("		d.[name] '@defaultname',")
            .AppendLine("		d.[definition] '@definition',")
		    .AppendLine("		e.[seed_value] '@seedvalue',")
            .AppendLine("		e.[increment_value] '@incrementvalue'")
            .AppendLine("	from")
            .AppendLine("		sys.columns b")
            .AppendLine("			join sys.types AS t ON t.user_type_id=b.user_type_id")
            .AppendLine("			left outer join sys.default_constraints d on ")
            .AppendLine("					d.[parent_object_id] = a.[object_id]")
            .AppendLine("					and d.[parent_column_id]=b.[column_id]")
            .AppendLine("			left outer join sys.identity_columns e on")
			.AppendLine("					e.[object_id]=a.[object_id]")
			.AppendLine("					and e.[column_id]=b.[column_id]")
            .AppendLine("	where")
            .AppendLine("		b.[object_id]=a.[OBJECT_ID]")
            .AppendLine("	order by")
            .AppendLine("		b.[column_id]")
            .AppendLine("	for xml path('column'),type")
            .AppendLine("	) 'columns',")
            .AppendLine("	(")
            .AppendLine("		select")
            .AppendLine("			b.[name] '@indexname',")
            .AppendLine("			b.[type_desc] '@indextype',")
            .AppendLine("			b.[is_unique] '@isunique',")
            .AppendLine("			b.[is_primary_key] '@isprimarykey',")
            .AppendLine("			b.[is_unique_constraint] '@isuniqueconstraint',")
            .AppendLine("			(")
            .AppendLine("				select ")
            .AppendLine("					d.[name] '@name',")
            .AppendLine("					c.[key_ordinal] '@colid',")
            .AppendLine("					c.[is_descending_key] '@isdescendingkey'")
            .AppendLine("				from")
            .AppendLine("					sys.index_columns c ")
            .AppendLine("						join sys.columns d on d.[object_id]=a.[OBJECT_ID]")
            .AppendLine("											  and d.[column_id]=c.[column_id]")
            .AppendLine("				where")
            .AppendLine("					c.[object_id]=a.[OBJECT_ID]")
            .AppendLine("					and c.[index_id]=b.[index_id]")
            .AppendLine("				order by")
            .AppendLine("					c.[index_column_id]	")
            .AppendLine("				for xml path('column'),type")
            .AppendLine("			) ")

            .AppendLine("		from")
            .AppendLine("			sys.indexes b")
            .AppendLine("		where")
            .AppendLine("           b.[type]>0")
            .AppendLine("			and b.[object_id]=a.[OBJECT_ID]")

            .AppendLine("		order by")
            .AppendLine("			b.[index_id]")
            .AppendLine("		for xml path('index'),type")
            .AppendLine("	) 'indexes'")
            .AppendLine(" from")
            .AppendLine("	sys.tables a")
            .AppendLine(" where")
            .AppendLine("	a.[name]=@name")
            .AppendLine("	and a.[type]='u'	")
            .AppendLine(" for xml path('table'),root('schema'),elements xsinil")
            .AppendLine(")")
            .AppendLine(" select @result;");

            base.ExecuteNonQuery(sSQL.ToString());
        }

        #endregion

        public string Reconstitute(BLL.Table oTable)
        {
            StringBuilder SQL = new StringBuilder();

            XmlDocument oDoc = new XmlDocument();
            oDoc.LoadXml(oTable.Configuration);

            SQL.AppendLine("if exists(select 1 from sys.objects where type='u' and name='" + oTable.Name + "') drop proc " + oTable.Name);
            SQL.AppendLine("GO");
            SQL.AppendLine("create table [" + oTable.Name + "] (");

            //Add columns
            XmlNodeList oColumns = oDoc.SelectNodes("/schema/table/columns/column");
            for(int i=0; i<oColumns.Count; i++)
            {
                XmlNode oColumn=oColumns[i];

                SQL.Append(FormatColumn(oColumn));

                if (i < oColumns.Count-1)
                    SQL.AppendLine(",");
                else
                    SQL.AppendLine();
            }
           
            SQL.AppendLine(")");

            //indexes
            XmlNodeList oIndexes= oDoc.SelectNodes("/schema/table/indexes/index");
            foreach (XmlNode oIndex in oIndexes)
            {
                //index attributes
                SQL.AppendLine(FormatIndex(oTable.Name,oIndex));
            }

            SQL.AppendLine("GO");

            return SQL.ToString();
        }

        private string FormatColumn(XmlNode oColumn)
        {
            StringBuilder SQL = new StringBuilder();

            //Attributes
            string sColName = oColumn.Attributes["colname"].Value.ToString();
            string sDatatype = oColumn.Attributes["type"].Value.ToString();
            string sIsIdentity = oColumn.Attributes["is_identity"].Value.ToString();
            string sIsNullable = oColumn.Attributes["is_nullable"].Value.ToString();
            string sMaxLength = oColumn.Attributes["max_length"].Value.ToString();
            string sPrecision = oColumn.Attributes["precision"].Value.ToString();
            string sScale = oColumn.Attributes["scale"].Value.ToString();
            string sSeedValue = ""; 
            string sIncrementValue = ""; 

            if (sIsIdentity == "1")
            {
                sSeedValue = oColumn.Attributes["seedvalue"].Value.ToString();
                sIncrementValue = oColumn.Attributes["incrementvalue"].Value.ToString();
            }

            string sDefaultName = "";
            string sDefaultValue = "";
            if (oColumn.Attributes["defaultname"] != null)
            {
                sDefaultName = oColumn.Attributes["defaultname"].Value.ToString();
                sDefaultValue = oColumn.Attributes["definition"].Value.ToString();
            }

            //Column name
            SQL.Append("    [" + sColName + "]");

            //Column datatype
            SQL.Append(
                     FormatDataType(sDatatype, sMaxLength, sScale, sPrecision)
                     );

            //identity?
            if (sIsIdentity == "1")
                SQL.Append(" IDENTITY(" + sSeedValue + "," + sIncrementValue + ")");

            //nullable?
            if (sIsNullable == "1")
                SQL.Append(" NULL");
            else
                SQL.Append(" NOT NULL");

            //default?
            if (sDefaultName.Length > 0)
                SQL.Append(" DEFAULT " + sDefaultName + " " + sDefaultValue);

            return SQL.ToString();
        }

        private string FormatDataType(
                            string sDatatype,
                            string sMaxLength,
                            string sScale,
                            string sPrecision)
        {
            string SQL = "";

            switch (sDatatype)
            {
                case "bigint":
                    SQL=" " + sDatatype;
                    break;

                case "binary":
                    SQL=" " + sDatatype;
                    break;

                case "bit":
                    SQL=" " + sDatatype;
                    break;

                case "char":
                    SQL=" " + sDatatype + "(" + sMaxLength + ")";
                    break;

                case "datetime":
                    SQL=" " + sDatatype;
                    break;

                case "decimal":
                    SQL=" " + sDatatype + "(" + sPrecision + "," + sScale + ")";
                    break;

                case "float":
                    SQL=" " + sDatatype;
                    break;

                case "image":
                    SQL=" " + sDatatype;
                    break;

                case "int":
                    SQL=" " + sDatatype;
                    break;

                case "nchar":
                    SQL=" " + sDatatype + "(" + sMaxLength + ")";
                    break;

                case "numeric":
                    SQL=" " + sDatatype + "(" + sPrecision + "," + sScale + ")";
                    break;

                case "nvarchar":
                    if (sMaxLength == "-1")
                        SQL=" " + sDatatype + "(max)";
                    else
                        SQL=" " + sDatatype + "(" + sMaxLength + ")";
                    break;

                case "smalldatetime":
                    SQL=" " + sDatatype;
                    break;

                case "smallint":
                    SQL=" " + sDatatype;
                    break;

                case "text":
                    SQL=" " + sDatatype;
                    break;

                case "tinyint":
                    SQL=" " + sDatatype;
                    break;

                case "uniqueidentifier":
                    SQL=" " + sDatatype;
                    break;

                case "varbinary":
                    SQL=" " + sDatatype;
                    break;

                case "varchar":
                    if (sMaxLength == "-1")
                        SQL=" " + sDatatype + "(max)";
                    else
                        SQL=" " + sDatatype + "(" + sMaxLength + ")";
                    break;

                case "xml":
                    SQL=" " + sDatatype;
                    break;

                default:
                    throw new ApplicationException("Unrecognized data type: " + sDatatype);
            }

            return SQL;
        }

        private string FormatIndex(string sTableName,XmlNode oIndex)
        {
            StringBuilder SQL = new StringBuilder();

            if (oIndex.Attributes["indexname"] != null)
            {
                string sIndexName = oIndex.Attributes["indexname"].Value.ToString();
                string sIndexType = oIndex.Attributes["indextype"].Value.ToString();
                string sIsUnique = oIndex.Attributes["isunique"].Value.ToString();
                string sIsPrimaryKey = oIndex.Attributes["isprimarykey"].Value.ToString();
                string sIsUniqueConstraint = oIndex.Attributes["isuniqueconstraint"].Value.ToString();

                string sIndexColumns = FormatIndexColumns(oIndex);

                if (sIsPrimaryKey == "1")
                {
                    //add this as a constraint
                    SQL.Append("alter table [" + sTableName + "] "
                                    + " add constraint " + sIndexName
                                    + " primary key (" + sIndexColumns + ")");

                    if (sIndexType.ToLower() == "clustered")
                        SQL.AppendLine(" clustered");
                    else
                        SQL.AppendLine();
                }
                else
                {
                    //add this as an index
                    SQL.Append("create");
                    if (sIsUnique == "1") SQL.Append(" unique");
                    if (sIndexType.ToLower() == "clustered")
                        SQL.Append(" clustered");
                    SQL.Append(" index " + sIndexName
                                + " on [" + sTableName + "] (");
                    SQL.Append(sIndexColumns);
                    SQL.AppendLine(")");
                }
            }

            return SQL.ToString();
        }

        private string FormatIndexColumns(XmlNode oIndex)
        {
            StringBuilder SQL = new StringBuilder();

            XmlNodeList oColumns = oIndex.SelectNodes("column");
            for (int i = 0; i < oColumns.Count; i++)
            {
                XmlNode oColumn = oColumns[i];

                if (i > 0) SQL.Append(",");
                SQL.Append("[" + oColumn.Attributes["name"].Value.ToString() + "]");
                if (oColumn.Attributes["isdescendingkey"].Value.ToString() == "1")
                    SQL.Append(" DESC");
            }

            return SQL.ToString();
        }
    }
}
