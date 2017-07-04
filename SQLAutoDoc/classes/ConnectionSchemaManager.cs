using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SQLAutoDoc.classes
{
    static class ConnectionSchemaManager
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
            set { m_UserID = value; }
        }

        public static string DSN
        {
            get { return m_DSN; }
            set
            {
                m_DSN = value;
                m_ConnectionString = "DSN=" + ParseConnection(m_DSN);
            }
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
            set
            {
                m_SQLDatabase = value;
            }
        }


        public static string SQLServer
        {
            get { return m_SQLServer; }
            set
            {
                m_SQLServer = value;
            }
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
