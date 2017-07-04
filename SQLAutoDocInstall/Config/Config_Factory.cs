using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace SQLAutoDocInstall.Config
{
     /// <summary>
    /// If registry key exists, retrieve path to config info about SQLAutoDoc
    /// for this user. If registry key doesn't exist, create it.
    /// </summary>
    public class Config_Factory
    {
        private string m_ConnectionString="";

        /// <summary>
        /// The root location of sql auto doc registry entries
        /// </summary>
        private const string USERROOT = "HKEY_CURRENT_USER";

        /// <summary>
        /// Registry path to SQLAutoDoc folder in registry. Folder is expected to be
        /// found under HKEY_LOCAL_USER
        /// </summary>
        private const string REGISTRY_PATH=USERROOT + "\\" +
                                    "Software\\JLION\\SQLAutoDoc";

        /// <summary>
        /// Registry key for SQLAutoDoc server name
        /// </summary>
        private const string SQLAUTODOC_SERVER = "ServerName";

        /// <summary>
        /// Registry key for SQLAutoDoc database name.
        /// </summary>
        private const string SQLAUTODOCDB = "DatabaseName";

        /// <summary>
        /// Get or set the name of the SERVER where the sql autodoc data is stored.
        /// </summary>
        public string ServerName
        {
            get { return (string)Microsoft.Win32.Registry.GetValue(REGISTRY_PATH, SQLAUTODOC_SERVER, ""); }
            set { 
                    Microsoft.Win32.Registry.SetValue(REGISTRY_PATH, SQLAUTODOC_SERVER, value);
                    setConnectionString();
            }
        }

        /// <summary>
        /// Get or set the name of the DATABASE where the sql autodoc data is stored
        /// </summary>
        public string DatabaseName
        {
            get { return (string)Microsoft.Win32.Registry.GetValue(REGISTRY_PATH, SQLAUTODOCDB, ""); }
            set { 
                    Microsoft.Win32.Registry.SetValue(REGISTRY_PATH, SQLAUTODOCDB, value);
                    setConnectionString();
            }
        }

        public string ConnectionString
        {
            get { return m_ConnectionString;}
        }

        private void setConnectionString()
        {
            m_ConnectionString = "Server=" + ServerName + ";Database=" + DatabaseName + ";Trusted_Connection=True;";
        }
    }
}
