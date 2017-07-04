using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SQLAutoDocLib.SCAN.WORKER
{
    class ScanAllTables : ScanBase
    {
        public event ScanCountOfObjectsToScan ScanCount;
        public event ScanObjectBegin ScanBegin;
        public event ScanObjectNotFound ScanNotFound;
        public event ScanObjectChanged ScanChanged;
        public event ScanObjectDeleted ScanDeleted;

        private Guid m_DBID;
        private string m_VersionID = "";

        public bool ChangesFound { get; set; }

        public ScanAllTables(Guid sDBID, string sVersionID)
        {
            m_DBID = sDBID;
            m_VersionID = sVersionID;
            this.ChangesFound = false;
        }

        /// <summary>
        /// Scan the database associated with a given connection string.
        /// If no data exists in the current sqlautodoc database for this 
        /// server and database, then records are created, otherwise
        /// the current records are used.
        /// </summary>
        /// <param name="sConnectionString"></param>
        public void Scan(string sConnectionString)
        {
            DBL.DBTable_Factory oFactory = new DBL.DBTable_Factory();

            //Delete any table history records associated with incomplete versions.
            //These records would be left over from a previously uncompleted scan.
            oFactory.DeleteIncompleteHistory(DBID:m_DBID);

            //Indicate that all known tables (in the database) are being re-verified, in case a table has been dropped.
            oFactory.MarkScanningInProgress(DBID: m_DBID);
            
            //Get list of tables from database
            using (AGENT.DBTableAgent_Factory oTableAgent = new AGENT.DBTableAgent_Factory(sConnectionString:sConnectionString))
            {
                DataTable oAllTables = oTableAgent.GetList();
                ScanCount(SQLAutoDocLib.UTIL.Constants.TABLENODE, oAllTables.Rows.Count);

                //For each table...
                foreach (DataRow oTable in oAllTables.Rows)
                {
                    string sTableName = oTable[0].ToString();

                    //Alert that we're beginning the scan
                    ScanBegin(SQLAutoDocLib.UTIL.Constants.TABLENODE, sTableName);

                    //Open a connection to the target server
                    oTableAgent.OpenConnection();

                    //Create temporary stored procedures
                    //  These will go away when the connection is closed
                    oTableAgent.make_TemporaryStoredProcedures();

                    //Get an xml summary of table schema
                    string sXML = oTableAgent.GetTableSchema(sTableName);

                    //Close the connection to the target server
                    oTableAgent.CloseConnection();

                    //Record with same name and xml summary already exists in database?
                    bool bIsMatch=false;

                    Guid oFoundTable = oFactory.FindWithSchemaMatch(
                                                DBID: m_DBID,
                                                sName: sTableName, 
                                                sSchemaXML: sXML,
                                                IsMatch: out bIsMatch);

                    if (oFoundTable == Guid.Empty)
                    {
                        // table not found 

                        // create new table record
                        BLL.Table oNewTable = new BLL.Table(DBID: m_DBID);
                        oNewTable.Name = sTableName;
                        oNewTable.Configuration = sXML;
                        oNewTable.FindFirstVersion = m_VersionID;
                        oNewTable.FindLastVersion = m_VersionID;
                        oNewTable.ScanInProgress = false;
                        oNewTable.ChangedInLastScan = true;
                        oNewTable.Save();

                        // create new table version record
                        BLL.TableHistory oNewTableHistory = new BLL.TableHistory(TableID: oNewTable.TableID, VersionID: m_VersionID);
                        oNewTableHistory.Configuration = sXML;
                        oNewTableHistory.CurrentlyExists = true;

                        oNewTableHistory.Save();
                        this.ChangesFound = true;

                        ScanNotFound(SQLAutoDocLib.UTIL.Constants.TABLENODE,sTableName);
                    }
                    else if (bIsMatch == false)
                    {
                        // table found but schema has changed

                        // update table record
                        BLL.Table oNewTable = new BLL.Table(DBID: m_DBID,TableID:oFoundTable);
                        oNewTable.Load();

                        oNewTable.Configuration = sXML;
                        oNewTable.FindLastVersion = m_VersionID;
                        oNewTable.ScanInProgress = false;
                        oNewTable.ChangedInLastScan = true;

                        oNewTable.Save();

                        // create new table version record
                        BLL.TableHistory oNewTableHistory = new BLL.TableHistory(TableID: oNewTable.TableID, VersionID: m_VersionID);
                        oNewTableHistory.Configuration = sXML;
                        oNewTableHistory.CurrentlyExists = true;

                        oNewTableHistory.Save();
                        this.ChangesFound = true;

                        ScanChanged(SQLAutoDocLib.UTIL.Constants.TABLENODE, sTableName);
                    }
                    else
                    {
                        // table found and schema is the same.

                        // update table record
                        BLL.Table oNewTable = new BLL.Table(DBID: m_DBID, TableID: oFoundTable);
                        oNewTable.Load();

                        oNewTable.FindLastVersion = m_VersionID;
                        oNewTable.ScanInProgress = false;
                        oNewTable.ChangedInLastScan = false;

                        oNewTable.Save();
                    }
                }
            }

            //After scan is complete...
            //Note that any tables not found have been dropped
            DataTable oDT = oFactory.FindUnfound(DBID: m_DBID);

            foreach (DataRow oDroppedTable in oDT.Rows)
            {
                Guid oTableID=(Guid)oDroppedTable["tableid"];

                // update table record
                BLL.Table oNewTable = new BLL.Table(DBID: m_DBID, TableID: oTableID);
                oNewTable.Load();

                oNewTable.FindLastVersion = m_VersionID;
                oNewTable.ScanInProgress = false;
                oNewTable.ChangedInLastScan = true;
                oNewTable.CurrentlyExists = false;

                oNewTable.Save();

                // create new table version record
                BLL.TableHistory oNewTableHistory = new BLL.TableHistory(TableID: oNewTable.TableID, VersionID: m_VersionID);
                oNewTableHistory.Configuration = "";
                oNewTableHistory.CurrentlyExists = false;

                oNewTableHistory.Save();
                this.ChangesFound = true;

                ScanDeleted(SQLAutoDocLib.UTIL.Constants.TABLENODE, oNewTable.Name);
            }
        }
    }
}
