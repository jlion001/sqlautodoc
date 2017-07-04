using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SQLAutoDocLib.SCAN.WORKER
{
    class ScanAllProcedures : ScanBase
    {
        public event ScanCountOfObjectsToScan ScanCount;
        public event ScanObjectBegin ScanBegin;
        public event ScanObjectNotFound ScanNotFound;
        public event ScanObjectChanged ScanChanged;
        public event ScanObjectDeleted ScanDeleted;

        private Guid m_DBID;
        private string m_VersionID = "";

        public bool ChangesFound {get;set;}

        public ScanAllProcedures(Guid sDBID, string sVersionID)
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
            DBL.DBProcedure_Factory oFactory = new DBL.DBProcedure_Factory();

            //Delete any procedure history records associated with incomplete versions.
            //These records would be left over from a previously uncompleted scan.
            oFactory.DeleteIncompleteHistory(sDBID: m_DBID);

            //Indicate that all known procedures (in the database) are being re-verified, in case a table has been dropped.
            oFactory.MarkScanningInProgress(sDBID: m_DBID);

            //Get list of procedures from database
            using (AGENT.DBProcedureAgent_Factory oProcedureAgent = new AGENT.DBProcedureAgent_Factory(sConnectionString: sConnectionString))
            {
                DataTable oAllProcedures = oProcedureAgent.GetList();
                ScanCount(SQLAutoDocLib.UTIL.Constants.PROCEDURENODE, oAllProcedures.Rows.Count);

                //For each procedure...
                foreach (DataRow oProcedure in oAllProcedures.Rows)
                {
                    string sProcedureName = oProcedure[0].ToString();

                    //Alert that we're beginning the scan
                    ScanBegin(SQLAutoDocLib.UTIL.Constants.PROCEDURENODE, sProcedureName);

                    //Open a connection to the target server
                    oProcedureAgent.OpenConnection();

                    //Create temporary stored procedures
                    //  These will go away when the connection is closed
                    oProcedureAgent.make_TemporaryStoredProcedures();

                    //Get an xml summary of table schema
                    string sXML = oProcedureAgent.GetProcedureSchema(oProcedure[0].ToString());

                    //Close the connection to the target server
                    oProcedureAgent.CloseConnection();

                    //Record with same name and xml summary already exists in database?
                    bool bIsMatch = false;

                    Guid oFoundProcedure = oFactory.FindWithSchemaMatch(
                                                DBID: m_DBID,
                                                sName: sProcedureName,
                                                sSchemaXML: sXML,
                                                IsMatch: out bIsMatch);

                    if (oFoundProcedure == Guid.Empty)
                    {
                        // table not found 

                        // create new procedure record
                        BLL.Procedure oNewProcedure = new BLL.Procedure(DBID: m_DBID);
                        oNewProcedure.Name = sProcedureName;
                        oNewProcedure.Configuration = sXML;
                        oNewProcedure.FindFirstVersion = m_VersionID;
                        oNewProcedure.FindLastVersion = m_VersionID;
                        oNewProcedure.ScanInProgress = false;
                        oNewProcedure.ChangedInLastScan = true;

                        oNewProcedure.Save();

                        // create new procedure version record
                        BLL.ProcedureHistory oNewTableHistory = new BLL.ProcedureHistory(ProcedureID: oNewProcedure.ProcedureID, VersionID: m_VersionID);
                        oNewTableHistory.Configuration = sXML;

                        oNewTableHistory.Save();
                        this.ChangesFound = true;

                        ScanNotFound(SQLAutoDocLib.UTIL.Constants.PROCEDURENODE, sProcedureName);
                    }
                    else if (bIsMatch == false)
                    {
                        // procedure found but schema has changed

                        // update procedure record
                        BLL.Procedure oNewProcedure = new BLL.Procedure(DBID: m_DBID, ProcedureID: oFoundProcedure);
                        oNewProcedure.Load();

                        oNewProcedure.Configuration = sXML;
                        oNewProcedure.FindLastVersion = m_VersionID;
                        oNewProcedure.ScanInProgress = false;
                        oNewProcedure.ChangedInLastScan = true;

                        oNewProcedure.Save();
                        this.ChangesFound = true;

                        // create new table version record
                        BLL.ProcedureHistory oNewProcedureHistory = new BLL.ProcedureHistory(ProcedureID: oNewProcedure.ProcedureID, VersionID: m_VersionID);
                        oNewProcedureHistory.Configuration = sXML;

                        oNewProcedureHistory.Save();

                        ScanChanged(SQLAutoDocLib.UTIL.Constants.PROCEDURENODE, sProcedureName);
                    }
                    else
                    {
                        // procedure found and schema is the same.

                        // update procedure record
                        BLL.Procedure oNewProcedure = new BLL.Procedure(DBID: m_DBID, ProcedureID: oFoundProcedure);
                        oNewProcedure.Load();

                        oNewProcedure.FindLastVersion = m_VersionID;
                        oNewProcedure.ScanInProgress = false;
                        oNewProcedure.ChangedInLastScan = false;

                        oNewProcedure.Save();
                    }
                }
            }

            //After scan is complete...
            //Note that any tables not found have been dropped
            DataTable oDT = oFactory.FindUnfound(DBID: m_DBID);

            foreach (DataRow oDroppedProcedure in oDT.Rows)
            {
                Guid oProcedureID = (Guid)oDroppedProcedure["Procedureid"];

                // update Procedure record
                BLL.Procedure oNewProcedure = new BLL.Procedure(DBID: m_DBID, ProcedureID: oProcedureID);
                oNewProcedure.Load();

                oNewProcedure.FindLastVersion = m_VersionID;
                oNewProcedure.ScanInProgress = false;
                oNewProcedure.ChangedInLastScan = true;
                oNewProcedure.CurrentlyExists = false;

                oNewProcedure.Save();

                // create new Procedure version record
                BLL.ProcedureHistory oNewProcedureHistory = new BLL.ProcedureHistory(ProcedureID: oNewProcedure.ProcedureID, VersionID: m_VersionID);
                oNewProcedureHistory.Configuration = "";
                oNewProcedureHistory.CurrentlyExists = false;

                oNewProcedureHistory.Save();
                this.ChangesFound = true;

                ScanDeleted(SQLAutoDocLib.UTIL.Constants.PROCEDURENODE, oNewProcedure.Name);
            }
        }
    }
}
