using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SQLAutoDocLib.DBL.shared
{
    abstract class DBLBase
    {
       protected int miTimeoutPeriod = 1000;

        protected string m_ConnectionString = "";

        protected SqlConnection m_Con = null;

        public DBLBase(string sConnectionString)
        {
            m_ConnectionString = sConnectionString;
        }

        public string ConnectionString
        {
            get { return m_ConnectionString; }
            set { m_ConnectionString = value; }
        }

        public void OpenConnection()
        {
            m_Con = new SqlConnection(m_ConnectionString);
            m_Con.Open();
        }

        public void CloseConnection()
        {
            m_Con.Close();
            m_Con.Dispose();
            m_Con=null;
        }

        #region ExecuteSQLToDataset
        protected DataSet ExecuteSQLToDataset(string sSQL)
        {
            DataSet oDT = null;

            if (m_Con == null)
            {
                //Single use connection
                SqlConnection oCon = new SqlConnection(m_ConnectionString);
                oCon.Open();

                oDT = ExecuteSQLToDataset(oCon: oCon, sSQL: sSQL);

                oCon.Close();
                oCon.Dispose();
            }
            else
            {
                //Reusable connection
                oDT = ExecuteSQLToDataset(oCon: m_Con, sSQL: sSQL);
            }

            return oDT;
        }

        protected DataSet ExecuteSQLToDataset(SqlConnection oCon, string sSQL)
        {
            DataSet oDS = new DataSet();

            SqlCommand oCMD = new SqlCommand(sSQL, oCon);
            oCMD.CommandType = CommandType.Text;
            oCMD.CommandTimeout = miTimeoutPeriod;

            SqlDataAdapter oDA = new SqlDataAdapter(oCMD);
            oDA.Fill(oDS);

            oDA.Dispose();

            oCon.Close();
            oCon.Dispose();

            return oDS;
        }
        #endregion

        #region ExecuteSQLToDatatable
        protected DataTable ExecuteSQLToDatatable(string sSQL)
        {
            DataTable oDT = null;

            if (m_Con == null)
            {
                //Single use connection
                SqlConnection oCon = new SqlConnection(m_ConnectionString);
                oCon.Open();

                oDT = ExecuteSQLToDatatable(oCon: oCon, sSQL: sSQL);

                oCon.Close();
                oCon.Dispose();
            }
            else
            {
                //Reusable connection
                oDT = ExecuteSQLToDatatable(oCon: m_Con, sSQL: sSQL);
            }

            return oDT;
        }

        protected DataTable ExecuteSQLToDatatable(SqlConnection oCon, string sSQL)
        {
            DataTable oDT = new DataTable();

            SqlCommand oCMD = new SqlCommand(sSQL, oCon);
            oCMD.CommandType = CommandType.Text;
            oCMD.CommandTimeout = miTimeoutPeriod;

            SqlDataAdapter oDA = new SqlDataAdapter(oCMD);
            oDA.Fill(oDT);

            oDA.Dispose();

            return oDT;
        }
        #endregion

        #region ExecuteProcToDatatable
        protected DataTable ExecuteProcToDatatable(string ProcName, List<SqlParameter> oParms)
        {
            DataTable oDT = null;

            if (m_Con == null)
            {
                //Single use connection
                SqlConnection oCon = new SqlConnection(m_ConnectionString);
                oCon.Open();

                oDT = ExecuteProcToDatatable(oCon: oCon, ProcName: ProcName, oParms: oParms);

                oCon.Close();
                oCon.Dispose();
            }
            else
            {
                //Reusable connection
                oDT = ExecuteProcToDatatable(oCon: m_Con, ProcName: ProcName, oParms: oParms);
            }

            return oDT;
        }

        protected DataTable ExecuteProcToDatatable(SqlConnection oCon, string ProcName, List<SqlParameter> oParms)
        {
            DataTable oDT = new DataTable();

            SqlCommand oCMD = new SqlCommand(ProcName, oCon);
            oCMD.CommandType = CommandType.StoredProcedure;
            oCMD.CommandTimeout = miTimeoutPeriod;

            foreach (SqlParameter oParm in oParms)
                oCMD.Parameters.Add(oParm);

            SqlDataAdapter oDA = new SqlDataAdapter(oCMD);
            oDA.Fill(oDT);

            oDA.Dispose();

            return oDT;
        }
        #endregion

        #region ExecuteSQLtoScalar
        protected object ExecuteSQLToScalar(string sSQL)
        {
            object oValue = null;

            if (m_Con == null)
            {
                //Single use connection
                SqlConnection oCon = new SqlConnection(m_ConnectionString);
                oCon.Open();

                oValue = ExecuteSQLToScalar(oCon: oCon, sSQL: sSQL);

                oCon.Close();
                oCon.Dispose();
            }
            else
            {
                //Reusable connection
                oValue = ExecuteSQLToScalar(oCon: m_Con, sSQL: sSQL);
            }

            return oValue;
        }

        protected object ExecuteSQLToScalar(SqlConnection oCon, string sSQL)
        {
            string sResult = "";

            SqlCommand oCMD = new SqlCommand(sSQL, oCon);
            oCMD.CommandType = CommandType.Text;
            oCMD.CommandTimeout = miTimeoutPeriod;

            Object oValue = oCMD.ExecuteScalar();

            return sResult;
        }
        #endregion

        #region ExecuteProcToScalar
        protected object ExecuteProcToScalar(string ProcName, ref List<SqlParameter> oParms)
        {
            object oValue = null;

            if (m_Con == null)
            {
                //Single use connection
                SqlConnection oCon = new SqlConnection(m_ConnectionString);
                oCon.Open();

                oValue = ExecuteProcToScalar(oCon: oCon, ProcName: ProcName, oParms: ref oParms);

                oCon.Close();
                oCon.Dispose();
            }
            else
            {
                //Reusable connection
                oValue = ExecuteProcToScalar(oCon: m_Con, ProcName: ProcName,  oParms: ref oParms);
            }

            return oValue;
        }

        protected object ExecuteProcToScalar(SqlConnection oCon, string ProcName, ref List<SqlParameter> oParms)
        {
            SqlCommand oCMD = new SqlCommand(ProcName, oCon);
            oCMD.CommandType = CommandType.StoredProcedure;
            oCMD.CommandTimeout = miTimeoutPeriod;

            foreach (SqlParameter oParm in oParms)
                oCMD.Parameters.Add(oParm);

            Object oValue = oCMD.ExecuteScalar();

            return oValue;
        }
        #endregion


        #region ExecuteNonQuery
        protected DataSet ExecuteNonQuery(string sSQL)
        {
            DataSet oDT = null;

            if (m_Con == null)
            {
                //Single use connection
                SqlConnection oCon = new SqlConnection(m_ConnectionString);
                oCon.Open();

                ExecuteNonQuery(oCon: oCon, sSQL: sSQL);

                oCon.Close();
                oCon.Dispose();
            }
            else
            {
                //Reusable connection
                ExecuteNonQuery(oCon: m_Con, sSQL: sSQL);
            }

            return oDT;
        }

        protected void ExecuteNonQuery(SqlConnection oCon, string sSQL)
        {
            SqlCommand oCMD = new SqlCommand(sSQL, oCon);
            oCMD.CommandType = CommandType.Text;
            oCMD.CommandTimeout = miTimeoutPeriod;

            oCMD.ExecuteNonQuery();
        }
        #endregion

        #region ExecuteProcNonQuery
        protected void ExecuteNonQuery(string sProcName, List<SqlParameter> oParms)
        {
            bool bCloseConnectionOnExit=false;

            SqlCommand oCMD = null; 

            if (m_Con == null)
            {
                //Single use connection
                bCloseConnectionOnExit = true;

                SqlConnection oCon = new SqlConnection(m_ConnectionString);
                oCon.Open();

                oCMD=new SqlCommand(sProcName, oCon);
            }
            else
            {
                //Reusable connection
                oCMD = new SqlCommand(sProcName, m_Con);
            }

            oCMD.CommandType = CommandType.StoredProcedure;
            oCMD.CommandTimeout = miTimeoutPeriod;

            foreach (SqlParameter oParm in oParms)
                oCMD.Parameters.Add(oParm);

            oCMD.ExecuteNonQuery();

            if (bCloseConnectionOnExit)
            {
                oCMD.Connection.Close();
                oCMD.Connection.Dispose();
            }

            oCMD.Dispose();
        }

        protected void ExecuteNonQuery(SqlConnection oCon, string sProcName, List<SqlParameter> oParms)
        {
            SqlCommand oCMD = new SqlCommand(sProcName, oCon);
            oCMD.CommandType = CommandType.StoredProcedure;
            oCMD.CommandTimeout = miTimeoutPeriod;

            foreach (SqlParameter oParm in oParms)
                oCMD.Parameters.Add(oParm);

            oCMD.ExecuteNonQuery();
        }
        #endregion


    }
}
