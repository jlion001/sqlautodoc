using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.UTIL
{
    public static class ConnectionSchemaManager
    {
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
            set { 
                m_ConnectionString = value;
                ParseConnection(m_ConnectionString);
            }
        }

        private static void ParseConnection(string sConnectionName)
        {
            m_SQLServer = "";
            m_SQLDatabase = "";

            //text from the word server= to ; is the server name.
            //IE: Server=bth-lyndsjoe-d;
            int iServerStart = sConnectionName.ToLower().IndexOf("server=");
            if (iServerStart > -1)
            {
                int iServerEnd = sConnectionName.IndexOf(";", iServerStart);

                if (iServerEnd > -1)
                    m_SQLServer = sConnectionName.Substring(iServerStart + 7, iServerEnd - (iServerStart + 7));
            }

            //text from the word database= to ; is the database name.
            //IE: Database=sqladoc;
            int iDatabaseStart = sConnectionName.ToLower().IndexOf("database=");
            if (iDatabaseStart > -1)
            {
                int iDatabaseEnd = sConnectionName.IndexOf(";", iDatabaseStart);

                if (iDatabaseEnd > -1)
                    m_SQLDatabase = sConnectionName.Substring(iDatabaseStart + 9, iDatabaseEnd - (iServerStart + 7));
            }
        }
    }
}
