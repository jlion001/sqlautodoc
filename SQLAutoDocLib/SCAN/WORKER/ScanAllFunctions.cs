using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SQLAutoDocLib.SCAN.WORKER
{
    class ScanAllFunctions : ScanBase
    {
        public event ScanCountOfObjectsToScan ScanCount;
        public event ScanObjectBegin ScanBegin;
        public event ScanObjectNotFound ScanNotFound;
        public event ScanObjectChanged ScanChanged;
        public event ScanObjectDeleted ScanDeleted;

        private Guid m_DBID;
        private string m_VersionID = "";

        public bool ChangesFound { get; set; }

        public ScanAllFunctions(Guid sDBID, string sVersionID)
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
            DBL.DBFunction_Factory oFactory = new DBL.DBFunction_Factory();

            //Delete any Function history records associated with incomplete versions.
            //These records would be left over from a previously uncompleted scan.
            oFactory.DeleteIncompleteHistory(sDBID: m_DBID);

            //Indicate that all known Functions (in the database) are being re-verified, in case a table has been dropped.
            oFactory.MarkScanningInProgress(sDBID: m_DBID);

            //Get list of Functions from database
            using (AGENT.DBFunctionAgent_Factory oFunctionAgent = new AGENT.DBFunctionAgent_Factory(sConnectionString: sConnectionString))
            {
                DataTable oAllFunctions = oFunctionAgent.GetList();
                ScanCount(SQLAutoDocLib.UTIL.Constants.FUNCTIONNODE, oAllFunctions.Rows.Count);

                //For each Function...
                foreach (DataRow oFunction in oAllFunctions.Rows)
                {
                    string sFunctionName = oFunction[0].ToString();

                    //Alert that we're beginning the scan
                    ScanBegin(SQLAutoDocLib.UTIL.Constants.FUNCTIONNODE, sFunctionName);

                    //Open a connection to the target server
                    oFunctionAgent.OpenConnection();

                    //Create temporary stored Functions
                    //  These will go away when the connection is closed
                    oFunctionAgent.make_TemporaryStoredProcedures();

                    //Get an xml summary of table schema
                    string sXML = oFunctionAgent.GetFunctionSchema(oFunction[0].ToString());

                    //Close the connection to the target server
                    oFunctionAgent.CloseConnection();

                    //Record with same name and xml summary already exists in database?
                    bool bIsMatch = false;

                    Guid oFoundFunction = oFactory.FindWithSchemaMatch(
                                                DBID: m_DBID,
                                                sName: sFunctionName,
                                                sSchemaXML: sXML,
                                                IsMatch: out bIsMatch);

                    if (oFoundFunction == Guid.Empty)
                    {
                        // table not found 

                        // create new Function record
                        BLL.Function oNewFunction = new BLL.Function(DBID: m_DBID);
                        oNewFunction.Name = sFunctionName;
                        oNewFunction.Configuration = sXML;
                        oNewFunction.FindFirstVersion = m_VersionID;
                        oNewFunction.FindLastVersion = m_VersionID;
                        oNewFunction.ScanInProgress = false;
                        oNewFunction.ChangedInLastScan = true;

                        oNewFunction.Save();

                        // create new Function version record
                        BLL.FunctionHistory oNewFunctionHistory = new BLL.FunctionHistory(FunctionID: oNewFunction.FunctionID, VersionID: m_VersionID);
                        oNewFunctionHistory.Configuration = sXML;

                        oNewFunctionHistory.Save();
                        this.ChangesFound = true;

                        ScanNotFound(SQLAutoDocLib.UTIL.Constants.FUNCTIONNODE, sFunctionName);

                    }
                    else if (bIsMatch == false)
                    {
                        // Function found but schema has changed

                        // update Function record
                        BLL.Function oNewFunction = new BLL.Function(DBID: m_DBID, FunctionID: oFoundFunction);
                        oNewFunction.Load();

                        oNewFunction.Configuration = sXML;
                        oNewFunction.FindLastVersion = m_VersionID;
                        oNewFunction.ScanInProgress = false;
                        oNewFunction.ChangedInLastScan = true;

                        oNewFunction.Save();

                        // create new table version record
                        BLL.FunctionHistory oNewFunctionHistory = new BLL.FunctionHistory(FunctionID: oNewFunction.FunctionID, VersionID: m_VersionID);
                        oNewFunctionHistory.Configuration = sXML;

                        oNewFunctionHistory.Save();
                        this.ChangesFound = true;

                        ScanChanged(SQLAutoDocLib.UTIL.Constants.FUNCTIONNODE, sFunctionName);
                    }
                    else
                    {
                        // Function found and schema is the same.

                        // update Function record
                        BLL.Function oNewFunction = new BLL.Function(DBID: m_DBID, FunctionID: oFoundFunction);
                        oNewFunction.Load();

                        oNewFunction.FindLastVersion = m_VersionID;
                        oNewFunction.ScanInProgress = false;
                        oNewFunction.ChangedInLastScan = false;

                        oNewFunction.Save();
                    }
                }
            }

            //After scan is complete...
            //Note that any tables not found have been dropped
            DataTable oDT = oFactory.FindUnfound(DBID: m_DBID);

            foreach (DataRow oDroppedFunction in oDT.Rows)
            {
                Guid oFunctionID = (Guid)oDroppedFunction["functionid"];

                // update table record
                BLL.Function oNewFunction = new BLL.Function(DBID: m_DBID, FunctionID: oFunctionID);
                oNewFunction.Load();

                oNewFunction.FindLastVersion = m_VersionID;
                oNewFunction.ScanInProgress = false;
                oNewFunction.ChangedInLastScan = true;
                oNewFunction.CurrentlyExists = false;

                oNewFunction.Save();

                // create new table version record
                BLL.FunctionHistory oNewFunctionHistory = new BLL.FunctionHistory(FunctionID: oNewFunction.FunctionID, VersionID: m_VersionID);
                oNewFunctionHistory.Configuration = "";
                oNewFunctionHistory.CurrentlyExists = false;

                oNewFunctionHistory.Save();
                this.ChangesFound = true;

                ScanDeleted(SQLAutoDocLib.UTIL.Constants.FUNCTIONNODE, oNewFunction.Name);
            }
        }
    }
}
