using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Data;

namespace SQLAutoDocLib.SCAN.shared
{
    abstract class DBLBase
    {
       protected int miTimeoutPeriod = 1000;

        protected string m_ConnectionString = "";

        protected OdbcConnection m_Con = null;

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
            m_Con = new OdbcConnection(m_ConnectionString);
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
                OdbcConnection oCon = new OdbcConnection(m_ConnectionString);
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

        protected DataSet ExecuteSQLToDataset(OdbcConnection oCon,string sSQL)
        {
            DataSet oDS = new DataSet();

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
        #endregion

        #region ExecuteSQLToDatatable
        protected DataTable ExecuteSQLToDatatable(string sSQL)
        {
            DataTable oDT = null;

            if (m_Con == null)
            {
                //Single use connection
                OdbcConnection oCon = new OdbcConnection(m_ConnectionString);
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

        protected DataTable ExecuteSQLToDatatable(OdbcConnection oCon, string sSQL)
        {
            DataTable oDT = new DataTable();

            OdbcCommand oCMD = new OdbcCommand(sSQL, oCon);
            oCMD.CommandType = CommandType.Text;
            oCMD.CommandTimeout = miTimeoutPeriod;

            OdbcDataAdapter oDA = new OdbcDataAdapter(oCMD);
            oDA.Fill(oDT);

            oDA.Dispose();

            return oDT;
        }
        #endregion

        #region ExecuteProcToDatatable
        protected DataTable ExecuteProcToDatatable(string ProcName, List<OdbcParameter> oParms)
        {
            DataTable oDT = null;

            if (m_Con == null)
            {
                //Single use connection
                OdbcConnection oCon = new OdbcConnection(m_ConnectionString);
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

        protected DataTable ExecuteProcToDatatable(OdbcConnection oCon, string ProcName, List<OdbcParameter> oParms)
        {
            DataTable oDT = new DataTable();

            OdbcCommand oCMD = new OdbcCommand(ProcName, oCon);
            oCMD.CommandType = CommandType.StoredProcedure;
            oCMD.CommandTimeout = miTimeoutPeriod;

            foreach (OdbcParameter oParm in oParms)
                oCMD.Parameters.Add(oParm);

            OdbcDataAdapter oDA = new OdbcDataAdapter(oCMD);
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
                OdbcConnection oCon = new OdbcConnection(m_ConnectionString);
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

        protected object ExecuteSQLToScalar(OdbcConnection oCon, string sSQL)
        {
            string sResult = "";

            OdbcCommand oCMD = new OdbcCommand(sSQL, oCon);
            oCMD.CommandType = CommandType.Text;
            oCMD.CommandTimeout = miTimeoutPeriod;

            Object oValue = oCMD.ExecuteScalar();

            return sResult;
        }
        #endregion

        #region ExecuteProcToScalar
        protected object ExecuteProcToScalar(string sProcName, List<OdbcParameter> oParms)
        {
            object oValue = null;

            if (m_Con == null)
            {
                //Single use connection
                OdbcConnection oCon = new OdbcConnection(m_ConnectionString);
                oCon.Open();

                oValue = ExecuteProcToScalar(oCon: oCon, sProcName: sProcName, oParms: oParms);

                oCon.Close();
                oCon.Dispose();
            }
            else
            {
                //Reusable connection
                oValue = ExecuteProcToScalar(oCon: m_Con, sProcName: sProcName, oParms: oParms);
            }

            return oValue;
        }

        protected object ExecuteProcToScalar(OdbcConnection oCon, string sProcName, List<OdbcParameter> oParms)
        {
            OdbcCommand oCMD = new OdbcCommand(sProcName, oCon);
            oCMD.CommandType = CommandType.Text;
            oCMD.CommandTimeout = miTimeoutPeriod;

            foreach (OdbcParameter oParm in oParms)
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
                OdbcConnection oCon = new OdbcConnection(m_ConnectionString);
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

        protected void ExecuteNonQuery(OdbcConnection oCon, string sSQL)
        {
            OdbcCommand oCMD = new OdbcCommand(sSQL, oCon);
            oCMD.CommandType = CommandType.Text;
            oCMD.CommandTimeout = miTimeoutPeriod;

            oCMD.ExecuteNonQuery();
        }
        #endregion

        #region ExecuteProcNonQuery
        protected DataSet ExecuteNonQuery(string sSQL, string sProcName, List<OdbcParameter> oParms)
        {
            DataSet oDT = null;

            if (m_Con == null)
            {
                //Single use connection
                OdbcConnection oCon = new OdbcConnection(m_ConnectionString);
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

        protected void ExecuteNonQuery(OdbcConnection oCon, string sProcName, List<OdbcParameter> oParms)
        {
            OdbcCommand oCMD = new OdbcCommand(sProcName, oCon);
            oCMD.CommandType = CommandType.StoredProcedure;
            oCMD.CommandTimeout = miTimeoutPeriod;

            foreach (OdbcParameter oParm in oParms)
                oCMD.Parameters.Add(oParm);

            oCMD.ExecuteNonQuery();
        }
        #endregion

        #region ExtendedProperties
        /// <summary>
        /// Adds or updates an MS_DESCRIPTION extended property to a database element.
        /// </summary>
        /// <param name="Description">Text of description</param>
        /// <param name="Level0Type">Typically schema</param>
        /// <param name="Level0Name">Typically DBO</param>
        /// <param name="Level1Type">object type (IE: function, table, etc.)</param>
        /// <param name="Level1Name">object name</param>
        /// <param name="Level2Type">column (if specified)</param>
        /// <param name="Level2Name">column name (if specified)</param>
        public void UpdateProperty(
                        string Description,
                        string Level0Type = "Schema",
                        string Level0Name = "dbo",
                        string Level1Type = "default",
                        string Level1Name = "default")
        {
            UpdateExtendedProperty(
               Description: Description,
               Level0Name: Level0Name,
               Level0Type: Level0Type,
               Level1Name: Level1Name,
               Level1Type: Level1Type);
        }

        private void UpdateExtendedProperty(
                        string Description,
                        string Level0Type,
                        string Level0Name,
                        string Level1Type,
                        string Level1Name
            )
        {
            if (!ExecuteExtendedProperty(
                    Command: "sp_updateextendedproperty",
                    Value: Description,
                    Level0Name: Level0Name,
                    Level0Type: Level0Type,
                    Level1Name: Level1Name,
                    Level1Type: Level1Type)
                )

                ExecuteExtendedProperty(
                    Command: "sp_addextendedproperty",
                    Value: Description,
                    Level0Name: Level0Name,
                    Level0Type: Level0Type,
                    Level1Name: Level1Name,
                    Level1Type: Level1Type);
        }

        private bool ExecuteExtendedProperty(
                        string Command,
                        string Value,
                        string Level0Type,
                        string Level0Name,
                        string Level1Type,
                        string Level1Name
            )
        {
            bool Success = true;

            StringBuilder SQL = new StringBuilder("exec " + Command);

            SQL.AppendLine(" @name='MS_Description',")
               .AppendLine(" @value='" + Value.Replace("'", "''") + "',")
               .AppendLine(" @level0type=" + ExtendedPropertiesDefault(Level0Type) + ",")
               .AppendLine(" @level0name=" + ExtendedPropertiesDefault(Level0Name) + ",")

               .AppendLine(" @level1type=" + ExtendedPropertiesDefault(Level1Type) + ",")
               .AppendLine(" @level1name=" + ExtendedPropertiesDefault(Level1Name));

            try
            {
                ExecuteNonQuery(SQL.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Success = false;
            }

            return Success;
        }


        private string ExtendedPropertiesDefault(string Value)
        {
            if (Value.ToLower() == "default")
                return Value;
            else
                return UTIL.DBHelper.PadApostrophes(Value);

        }

        private string GetProperty(
                        string Level0Type = "default",
                        string Level0Name = "default",
                        string Level1Type = "default",
                        string Level1Name = "default",
                        string Level2Type = "default",
                        string Level2Name = "default")
        {
            string RetValue = "";

            StringBuilder SQL = new StringBuilder("select");
            SQL.AppendLine(" [objname],")
                .AppendLine(" [value]")
                .AppendLine(" from")
                .AppendLine(" ::fn_listextendedproperty (")
                .AppendLine("'MS_Description',")
                .AppendLine("'USER',")
                .AppendLine("default,")
                .AppendLine("default,")
                .AppendLine("default,")
                .AppendLine("default,")
                .AppendLine("default)");

            return RetValue;
        }

        #endregion
    }
}
