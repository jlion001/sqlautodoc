using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SQLAutoDocLib.SCAN.WORKER
{
    class ScanAllViews:ScanBase
    {
        public event ScanCountOfObjectsToScan ScanCount;
        public event ScanObjectBegin ScanBegin;
        public event ScanObjectChanged ScanChanged;
        public event ScanObjectNotFound ScanNotFound;
        public event ScanObjectDeleted ScanDeleted;

        private Guid m_DBID;
        private string m_VersionID = "";

        public bool ChangesFound { get; set; }

        public ScanAllViews(Guid sDBID, string sVersionID)
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
            DBL.DBView_Factory oFactory = new DBL.DBView_Factory();

            //Delete any View history records associated with incomplete versions.
            //These records would be left over from a previously uncompleted scan.
            oFactory.DeleteIncompleteHistory(sDBID: m_DBID);

            //Indicate that all known Views (in the database) are being re-verified, in case a table has been dropped.
            oFactory.MarkScanningInProgress(sDBID: m_DBID);

            //Get list of Views from database
            using (AGENT.DBViewAgent_Factory oViewAgent = new AGENT.DBViewAgent_Factory(sConnectionString: sConnectionString))
            {
                DataTable oAllViews = oViewAgent.GetList();
                ScanCount(SQLAutoDocLib.UTIL.Constants.VIEWNODE, oAllViews.Rows.Count);

                //For each View...
                foreach (DataRow oView in oAllViews.Rows)
                {
                    string sViewName = oView[0].ToString();

                    //Alert that we're beginning the scan
                    ScanBegin(SQLAutoDocLib.UTIL.Constants.VIEWNODE, sViewName);

                    //Open a connection to the target server
                    oViewAgent.OpenConnection();

                    //Create temporary stored Views
                    //  These will go away when the connection is closed
                    oViewAgent.make_TemporaryStoredViews();

                    //Get an xml summary of table schema
                    string sXML = oViewAgent.GetViewSchema(oView[0].ToString());

                    //Close the connection to the target server
                    oViewAgent.CloseConnection();

                    //Record with same name and xml summary already exists in database?
                    bool bIsMatch = false;

                    Guid oFoundView = oFactory.FindWithSchemaMatch(
                                                DBID:m_DBID,
                                                sName: sViewName,
                                                sSchemaXML: sXML,
                                                IsMatch: out bIsMatch);

                    if (oFoundView == Guid.Empty)
                    {
                        // table not found 

                        // create new View record
                        BLL.View oNewView = new BLL.View(DBID: m_DBID);
                        oNewView.Name = sViewName;
                        oNewView.Configuration = sXML;
                        oNewView.FindFirstVersion = m_VersionID;
                        oNewView.FindLastVersion = m_VersionID;
                        oNewView.ScanInProgress = false;
                        oNewView.ChangedInLastScan = true;

                        oNewView.Save();

                        // create new View version record
                        BLL.ViewHistory oNewTableHistory = new BLL.ViewHistory(ViewID: oNewView.ViewID, VersionID: m_VersionID);
                        oNewTableHistory.Configuration = sXML;

                        oNewTableHistory.Save();
                        this.ChangesFound = true;

                        ScanNotFound(SQLAutoDocLib.UTIL.Constants.VIEWNODE,sViewName);
                    }
                    else if (bIsMatch == false)
                    {
                        // View found but schema has changed

                        // update View record
                        BLL.View oNewView = new BLL.View(DBID: m_DBID, ViewID: oFoundView);
                        oNewView.Load();

                        oNewView.Configuration = sXML;
                        oNewView.FindLastVersion = m_VersionID;
                        oNewView.ScanInProgress = false;
                        oNewView.ChangedInLastScan = true;

                        oNewView.Save();

                        // create new table version record
                        BLL.ViewHistory oNewViewHistory = new BLL.ViewHistory(ViewID: oNewView.ViewID, VersionID: m_VersionID);
                        oNewViewHistory.Configuration = sXML;

                        oNewViewHistory.Save();
                        this.ChangesFound = true;

                        ScanChanged(SQLAutoDocLib.UTIL.Constants.VIEWNODE, sViewName);
                    }
                    else
                    {
                        // View found and schema is the same.

                        // update View record
                        BLL.View oNewView = new BLL.View(DBID: m_DBID, ViewID: oFoundView);
                        oNewView.Load();

                        oNewView.FindLastVersion = m_VersionID;
                        oNewView.ScanInProgress = false;
                        oNewView.ChangedInLastScan = false;

                        oNewView.Save();
                    }
                }
            }

            //After scan is complete...
            //Note that any tables not found have been dropped
            DataTable oDT = oFactory.FindUnfound(DBID: m_DBID);

            foreach (DataRow oDroppedView in oDT.Rows)
            {
                Guid oViewID = (Guid)oDroppedView["Viewid"];

                // update View record
                BLL.View oNewView = new BLL.View(DBID: m_DBID, ViewID: oViewID);
                oNewView.Load();

                oNewView.FindLastVersion = m_VersionID;
                oNewView.ScanInProgress = false;
                oNewView.ChangedInLastScan = true;
                oNewView.CurrentlyExists = false;

                oNewView.Save();

                // create new View version record
                BLL.ViewHistory oNewViewHistory = new BLL.ViewHistory(ViewID: oNewView.ViewID, VersionID: m_VersionID);
                oNewViewHistory.Configuration = "";
                oNewViewHistory.CurrentlyExists = false;

                oNewViewHistory.Save();
                this.ChangesFound = true;

                ScanDeleted(SQLAutoDocLib.UTIL.Constants.VIEWNODE, oNewView.Name);
            }
        }
    }
}
