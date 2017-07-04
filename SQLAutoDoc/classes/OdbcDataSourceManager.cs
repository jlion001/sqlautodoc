using System;
using System.Collections.Generic;
using System.Text;

namespace SQLAutoDoc.classes
{
    /// <summary>
    /// The types of data sources that can be set.
    /// </summary>
    public enum DataSourceType { 
        /// <summary>
        /// This is a system DSN
        /// </summary>
        System, 
        /// <summary>
        /// This is a user DSN
        /// </summary>
        User }

    /// <summary>
    /// Provides methods and tools to manage the Odbc data sources on the machine.
    /// </summary>
    public class OdbcDataSourceManager
    {
        /// <summary>
        /// Returns a list of data source names from the local machine.
        /// </summary>
        /// <returns>SortedList of DSN names</returns>
        public System.Collections.SortedList GetAllDataSourceNames()
        {
            // Get the list of user DSN's first.
            System.Collections.SortedList dsnList = GetUserDataSourceNames();

            // Get list of System DSN's and add them to the first list.
            System.Collections.SortedList systemDsnList = GetSystemDataSourceNames();
            for (int i = 0; i < systemDsnList.Count; i++)
            {
                string sName = systemDsnList.GetKey(i) as string;
                DataSourceType type = (DataSourceType)systemDsnList.GetByIndex(i);
                try
                {
                    // This dsn to the master list
                    dsnList.Add(sName, type);
                }
                catch 
                { 
                    // An exception can be thrown if the key being added is a duplicate so 
                    // we just catch it here and have to ignore it.
                }
            }

            return dsnList;
        }

        /// <summary>
        /// Gets all System data source names for the local machine.
        /// </summary>
        /// <returns>SortedList of DSN names</returns>
        public System.Collections.SortedList GetSystemDataSourceNames()
        {
            System.Collections.SortedList dsnList = new System.Collections.SortedList();

            // get system dsn's
            Microsoft.Win32.RegistryKey reg = (Microsoft.Win32.Registry.LocalMachine).OpenSubKey("Software");
            if (reg != null)
            {
                reg = reg.OpenSubKey("ODBC");
                if (reg != null)
                {
                    reg = reg.OpenSubKey("ODBC.INI");
                    if (reg != null)
                    {
                        reg = reg.OpenSubKey("ODBC Data Sources");
                        if (reg != null)
                        {
                            // Get all DSN entries defined in DSN_LOC_IN_REGISTRY.
                            foreach (string sName in reg.GetValueNames())
                            {
                                dsnList.Add(sName, DataSourceType.System);
                            }
                        }
                        try
                        {
                            reg.Close();
                        }
                        catch { /* ignore this exception if we couldn't close */ }
                    }
                }
            }

            return dsnList;
        }

        /// <summary>
        /// Gets all User data source names for the local machine.
        /// </summary>
        /// <returns>SortedList of DSN names</returns>
        public System.Collections.SortedList GetUserDataSourceNames()
        {
            System.Collections.SortedList dsnList = new System.Collections.SortedList();

            // get user dsn's
            Microsoft.Win32.RegistryKey reg = (Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software");
            if (reg != null)
            {
                reg = reg.OpenSubKey("ODBC");
                if (reg != null)
                {
                    reg = reg.OpenSubKey("ODBC.INI");
                    if (reg != null)
                    {
                        reg = reg.OpenSubKey("ODBC Data Sources");
                        if (reg != null)
                        {
                            // Get all DSN entries defined in DSN_LOC_IN_REGISTRY.
                            foreach (string sName in reg.GetValueNames())
                            {
                                dsnList.Add(sName, DataSourceType.User);
                            }
                        }
                        try
                        {
                            reg.Close();
                        }
                        catch { /* ignore this exception if we couldn't close */ }
                    }
                }
            }

            return dsnList;
        }
    }
}
