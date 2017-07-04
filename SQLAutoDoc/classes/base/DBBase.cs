using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;

namespace SQLAutoDoc.classes
{
    abstract public class DBBase
    {
        protected int miTimeoutPeriod = 1000;

        protected string m_ConnectionString = "";

        public DBBase(string sConnectionString)
        {
            m_ConnectionString = sConnectionString;
        }

        public string ConnectionString
        {
            get { return m_ConnectionString; }
            set { m_ConnectionString = value; }
        }

        protected DataSet ExecuteSQLToDataset(string sSQL)
        {
            DataSet oDS = new DataSet();

            OdbcConnection oCon = new OdbcConnection(m_ConnectionString);
            oCon.Open();

            OdbcCommand oCMD = new OdbcCommand(sSQL, oCon);
            oCMD.CommandType = CommandType.Text;
            oCMD.CommandTimeout = miTimeoutPeriod;

            OdbcDataAdapter oDA = new OdbcDataAdapter(oCMD);
            oDA.Fill(oDS);

            oDA.Dispose();

            oCon.Close();
            oCon.Dispose();

            return oDS;
        }

        protected DataTable ExecuteSQLToDatatable(string sSQL)
        {
            DataTable oDT = new DataTable();

            OdbcConnection oCon = new OdbcConnection(m_ConnectionString);
            oCon.Open();

            OdbcCommand oCMD = new OdbcCommand(sSQL, oCon);
            oCMD.CommandType = CommandType.Text;
            oCMD.CommandTimeout = miTimeoutPeriod;

            OdbcDataAdapter oDA = new OdbcDataAdapter(oCMD);
            oDA.Fill(oDT);

            oDA.Dispose();

            oCon.Close();
            oCon.Dispose();

            return oDT;
        }

        protected void ExecuteNonQuery(string sSQL)
        {
            OdbcConnection oCon = new OdbcConnection(m_ConnectionString);
            oCon.Open();

            OdbcCommand oCMD = new OdbcCommand(sSQL, oCon);
            oCMD.CommandType = CommandType.Text;
            oCMD.CommandTimeout = miTimeoutPeriod;

            oCMD.ExecuteNonQuery();

            oCon.Close();
            oCon.Dispose();
        }

        protected string PadApostrophes(string sValue)
        {
            return "'" + sValue.Replace("'", "''") + "'";
        }

        protected string EmptyStringToNull(string sValue)
        {
            string sRetValue = "";

            if (sValue=="" || sValue.Length==0)
                sRetValue = "NULL";
            else
                sRetValue = "'" + sValue.Replace("'","''") + "'";

            return sRetValue;
        }

        protected string NullToString(object oValue)
        {
            string sRetValue = "";

            if (oValue != null && oValue != System.DBNull.Value)
                sRetValue = oValue.ToString();

            return sRetValue;
        }
    }
}
