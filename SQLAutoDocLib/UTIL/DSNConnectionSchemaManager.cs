using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SQLAutoDocLib.UTIL
{
    public static class DSNConnectionSchemaManager
    {
        private static string m_DSN = "";
        private static string m_ConnectionString = "";
        private static string m_TempPath = System.IO.Path.GetTempPath();

        private static string m_SQLDatabase = "";
        private static string m_SQLServer = "";

        private static string m_UserID = "";

        public static string UserID
        {
            get { return m_UserID; }
        }

        public static string DSN
        {
            get { return m_DSN; }
            set
            {
                m_DSN = value;
                m_ConnectionString = "DSN=" + ParseConnection(m_DSN);
                GetServerInfo();
            }
        }

        private static void GetServerInfo()
        {
            SCAN.AGENT.DBServerAgent_Factory oServerAgent = new SCAN.AGENT.DBServerAgent_Factory(m_ConnectionString);
            m_SQLServer = oServerAgent.GetServerName();

            SCAN.AGENT.DBDatabaseAgent_Factory oDatabaseAgent = new SCAN.AGENT.DBDatabaseAgent_Factory(m_ConnectionString);
            m_SQLDatabase = oDatabaseAgent.GetDatabaseName();

            m_UserID = Environment.UserName;
        }

        public static string TempPath
        {
            get { return m_TempPath; }
            set
            {
                m_TempPath = value;
            }
        }

        public static string SQLDatabase
        {
            get { return m_SQLDatabase; }
        }


        public static string SQLServer
        {
            get { return m_SQLServer; }
        }

        public static string ConnectionString
        {
            get { return m_ConnectionString; }
            set { m_ConnectionString = value; }
        }

        public static string ParseConnection(string sConnectionName)
        {
            int iIndex = sConnectionName.LastIndexOf("-");         
            string sConnTemp = sConnectionName.Substring(0, iIndex - 1);

            return sConnTemp;
        }
    }
}
