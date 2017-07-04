using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SQLAutoDocLib.SCAN.WORKER
{
    class ScanAllTriggers : ScanBase
    {
        public event ScanCountOfObjectsToScan ScanCount;
        public event ScanObjectBegin ScanBegin;
        public event ScanObjectNotFound ScanNotFound;
        public event ScanObjectChanged ScanChanged;
        public event ScanObjectDeleted ScanDeleted;

        private Guid m_DBID;
        private string m_VersionID = "";

        public bool ChangesFound { get; set; }

        public ScanAllTriggers(Guid sDBID, string sVersionID)
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
            DBL.DBTrigger_Factory oFactory = new DBL.DBTrigger_Factory();

            //Delete any Trigger history records associated with incomplete versions.
            //These records would be left over from a previously uncompleted scan.
            oFactory.DeleteIncompleteHistory(sDBID: m_DBID);

            //Indicate that all known Triggers (in the database) are being re-verified, in case a table has been dropped.
            oFactory.MarkScanningInProgress(sDBID: m_DBID);

            //Get list of Triggers from database
            using (AGENT.DBTriggerAgent_Factory oTriggerAgent = new AGENT.DBTriggerAgent_Factory(sConnectionString: sConnectionString))
            {
                DataTable oAllTriggers = oTriggerAgent.GetList();
                ScanCount(SQLAutoDocLib.UTIL.Constants.TRIGGERNODE, oAllTriggers.Rows.Count);

                //For each Trigger...
                foreach (DataRow oTrigger in oAllTriggers.Rows)
                {
                    string sTriggerName = oTrigger[0].ToString();

                    //Alert that we're beginning the scan
                    ScanBegin(SQLAutoDocLib.UTIL.Constants.TRIGGERNODE, sTriggerName);

                    //Open a connection to the target server
                    oTriggerAgent.OpenConnection();

                    //Create temporary stored Triggers
                    //  These will go away when the connection is closed
                    oTriggerAgent.make_TemporaryStoredTriggers();

                    //Get an xml summary of table schema
                    string sXML = oTriggerAgent.GetTriggerSchema(oTrigger[0].ToString());

                    //Close the connection to the target server
                    oTriggerAgent.CloseConnection();

                    //Record with same name and xml summary already exists in database?
                    bool bIsMatch = false;

                    Guid oFoundTrigger = oFactory.FindWithSchemaMatch(
                                                DBID: m_DBID,
                                                sName: sTriggerName,
                                                sSchemaXML: sXML,
                                                IsMatch: out bIsMatch);

                    if (oFoundTrigger == Guid.Empty)
                    {
                        // table not found 

                        // create new Trigger record
                        BLL.Trigger oNewTrigger = new BLL.Trigger(DBID: m_DBID);
                        oNewTrigger.Name = sTriggerName;
                        oNewTrigger.Configuration = sXML;
                        oNewTrigger.FindFirstVersion = m_VersionID;
                        oNewTrigger.FindLastVersion = m_VersionID;
                        oNewTrigger.ScanInProgress = false;
                        oNewTrigger.ChangedInLastScan = true;

                        oNewTrigger.Save();

                        // create new Trigger version record
                        BLL.TriggerHistory oNewTableHistory = new BLL.TriggerHistory(TriggerID: oNewTrigger.TriggerID, VersionID: m_VersionID);
                        oNewTableHistory.Configuration = sXML;

                        oNewTableHistory.Save();
                        this.ChangesFound = true;

                        ScanNotFound(SQLAutoDocLib.UTIL.Constants.TRIGGERNODE, sTriggerName);
                    }
                    else if (bIsMatch == false)
                    {
                        // Trigger found but schema has changed

                        // update Trigger record
                        BLL.Trigger oNewTrigger = new BLL.Trigger(DBID: m_DBID, TriggerID: oFoundTrigger);
                        oNewTrigger.Load();

                        oNewTrigger.Configuration = sXML;
                        oNewTrigger.FindLastVersion = m_VersionID;
                        oNewTrigger.ScanInProgress = false;
                        oNewTrigger.ChangedInLastScan = true;

                        oNewTrigger.Save();

                        // create new table version record
                        BLL.TriggerHistory oNewTriggerHistory = new BLL.TriggerHistory(TriggerID: oNewTrigger.TriggerID, VersionID: m_VersionID);
                        oNewTriggerHistory.Configuration = sXML;

                        oNewTriggerHistory.Save();
                        this.ChangesFound = true;

                        ScanChanged(SQLAutoDocLib.UTIL.Constants.TRIGGERNODE, sTriggerName);
                    }
                    else
                    {
                        // Trigger found and schema is the same.

                        // update Trigger record
                        BLL.Trigger oNewTrigger = new BLL.Trigger(DBID: m_DBID, TriggerID: oFoundTrigger);
                        oNewTrigger.Load();

                        oNewTrigger.FindLastVersion = m_VersionID;
                        oNewTrigger.ScanInProgress = false;
                        oNewTrigger.ChangedInLastScan = false;

                        oNewTrigger.Save();
                    }
                }
            }

            //After scan is complete...
            //Note that any tables not found have been dropped
            DataTable oDT = oFactory.FindUnfound(DBID: m_DBID);

            foreach (DataRow oDroppedTrigger in oDT.Rows)
            {
                Guid oTriggerID = (Guid)oDroppedTrigger["Triggerid"];

                // update Trigger record
                BLL.Trigger oNewTrigger = new BLL.Trigger(DBID: m_DBID, TriggerID: oTriggerID);
                oNewTrigger.Load();

                oNewTrigger.FindLastVersion = m_VersionID;
                oNewTrigger.ScanInProgress = false;
                oNewTrigger.ChangedInLastScan = true;
                oNewTrigger.CurrentlyExists = false;

                oNewTrigger.Save();

                // create new Trigger version record
                BLL.TriggerHistory oNewTriggerHistory = new BLL.TriggerHistory(TriggerID: oNewTrigger.TriggerID, VersionID: m_VersionID);
                oNewTriggerHistory.Configuration = "";
                oNewTriggerHistory.CurrentlyExists = false;

                oNewTriggerHistory.Save();
                this.ChangesFound = true;

                ScanDeleted(SQLAutoDocLib.UTIL.Constants.TRIGGERNODE, oNewTrigger.Name);
            }
        }
    }
}
