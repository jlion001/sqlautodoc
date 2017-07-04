using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAutoDocLib.SCAN.WORKER
{
    public class ScanDatabase
    {
        public delegate void ScanDatabaseBeginEvent(DateTime e);
        public delegate void ScanDatabaseEndEvent(DateTime e);

        public delegate void ScanObjectTypeBeginEvent(string type, DateTime e);
        public delegate void ScanObjectTypeEndEvent(string type,DateTime e);

        public delegate void ScanObjectCountEvent(string type, int count);
        public delegate void ScanObjectBeginEvent(string type, string name, DateTime e);
        public delegate void ScanObjectNotFoundEvent(string type, string name, DateTime e);
        public delegate void ScanObjectChangedEvent(string type, string name, DateTime e);
        public delegate void ScanObjectDeletedEvent(string type, string name, DateTime e);

        public event ScanDatabaseBeginEvent ScanDatabaseBegin;
        public event ScanDatabaseEndEvent ScanDatabaseEnd;

        public event ScanObjectTypeBeginEvent ScanObjectTypeBegin;
        public event ScanObjectTypeEndEvent ScanObjectTypeEnd;

        public event ScanObjectCountEvent ScanObjectCount;
        public event ScanObjectBeginEvent ScanObjectBegin;
        public event ScanObjectNotFoundEvent ScanObjectNotFound;
        public event ScanObjectChangedEvent ScanObjectChanged;
        public event ScanObjectDeletedEvent ScanObjectDeleted;

        private string m_ConnectionString = "";
        private Guid m_ServerID = Guid.Empty;
        private Guid m_DBID=Guid.Empty;

        public ScanDatabase(string sConnectionString)
        {
            m_ConnectionString = sConnectionString;
            SetDB();
        }

        public Guid DBID
        {
            get { return m_DBID; }
        }

        private void SetDB()
        {
            //What server are we scanning?
            AGENT.DBServerAgent_Factory oServerAgent = new AGENT.DBServerAgent_Factory(m_ConnectionString);
            string sServerName = oServerAgent.GetServerName();

            //Does this server exist? If not, create it.
            BLL.Server oServer = null;

            DBL.DBServer_Factory oServerFactory = new DBL.DBServer_Factory();
            m_ServerID = oServerFactory.GetServerID(sServerName);
            if (m_ServerID == Guid.Empty)
            {
                //Create new server record.
                oServer = new BLL.Server();
                oServer.Name = sServerName;
                oServer.Save();

                m_ServerID = oServer.ServerID;
            }
            else
            {
                //load existing server
                oServer = new BLL.Server(m_ServerID);
                oServer.Load();
            }

            //What database are we scanning?
            AGENT.DBDatabaseAgent_Factory oDatabaseAgent = new AGENT.DBDatabaseAgent_Factory(m_ConnectionString);
            string sDatabaseName = oDatabaseAgent.GetDatabaseName();

            //Does this database exist? If not, create it.
            BLL.Database oDatabase = null;

            DBL.DBDatabase_Factory oDatabaseFactory = new DBL.DBDatabase_Factory();
            m_DBID = oDatabaseFactory.GetDatabaseID(m_ServerID, sDatabaseName);
            if (m_DBID == Guid.Empty)
            {
                //Create new database record
                oDatabase = new BLL.Database(oServer.ServerID);
                oDatabase.Name = sDatabaseName;
                oDatabase.Save();

                m_DBID = oDatabase.DBID;
            }
            else
            {
                //Load existing database
                oDatabase = new BLL.Database(m_ServerID, m_DBID);
                oDatabase.Load();
            }
        }

        public void Scan()
        {
            bool ChangesFound=false;

            ScanDatabaseBegin(DateTime.Now);
             
            //create version record
            SQLAutoDocLib.BLL.Version oVersion = new SQLAutoDocLib.BLL.Version(m_DBID);

            //Scan beginning
            oVersion.ScanStart = DateTime.Now.ToString();

            oVersion.Save();

            //scan all functions
            ScanObjectTypeBegin(SQLAutoDocLib.UTIL.Constants.FUNCTIONNODE, DateTime.Now);
            ScanAllFunctions oFunctionScanner = new ScanAllFunctions(m_DBID, oVersion.VersionID);
            oFunctionScanner.ScanBegin += new ScanAllFunctions.ScanObjectBegin(ScanObjectBegin_Handler);
            oFunctionScanner.ScanChanged += new ScanAllFunctions.ScanObjectChanged(ScanObjectChanged_Handler);
            oFunctionScanner.ScanNotFound += new ScanAllFunctions.ScanObjectNotFound(ScanObjectNotFound_Handler);
            oFunctionScanner.ScanDeleted += new ScanAllFunctions.ScanObjectDeleted(ScanObjectDeleted_Handler);
            oFunctionScanner.ScanCount += new ScanAllFunctions.ScanCountOfObjectsToScan(ScanCountOfObjectsToScan_Handler);
            oFunctionScanner.Scan(m_ConnectionString);
            oFunctionScanner.ScanCount += new ScanAllFunctions.ScanCountOfObjectsToScan(ScanCountOfObjectsToScan_Handler);
            oFunctionScanner.ScanDeleted += new ScanAllFunctions.ScanObjectDeleted(ScanObjectDeleted_Handler);
            oFunctionScanner.ScanNotFound -= new ScanAllFunctions.ScanObjectNotFound(ScanObjectNotFound_Handler);
            oFunctionScanner.ScanChanged -= new ScanAllFunctions.ScanObjectChanged(ScanObjectChanged_Handler);
            oFunctionScanner.ScanBegin -= new ScanAllFunctions.ScanObjectBegin(ScanObjectBegin_Handler);
            ScanObjectTypeEnd(SQLAutoDocLib.UTIL.Constants.FUNCTIONNODE, DateTime.Now);
            if (oFunctionScanner.ChangesFound) ChangesFound = true;

            //scan all procedures
            ScanObjectTypeBegin(SQLAutoDocLib.UTIL.Constants.PROCEDURENODE, DateTime.Now);
            ScanAllProcedures oProcedureScanner = new ScanAllProcedures(m_DBID, oVersion.VersionID);
            oProcedureScanner.ScanBegin += new ScanAllProcedures.ScanObjectBegin(ScanObjectBegin_Handler);
            oProcedureScanner.ScanChanged += new ScanAllProcedures.ScanObjectChanged(ScanObjectChanged_Handler);
            oProcedureScanner.ScanNotFound += new ScanAllProcedures.ScanObjectNotFound(ScanObjectNotFound_Handler);
            oProcedureScanner.ScanDeleted += new ScanAllProcedures.ScanObjectDeleted(ScanObjectDeleted_Handler);
            oProcedureScanner.ScanCount += new ScanAllFunctions.ScanCountOfObjectsToScan(ScanCountOfObjectsToScan_Handler);
            oProcedureScanner.Scan(m_ConnectionString);
            oProcedureScanner.ScanCount += new ScanAllFunctions.ScanCountOfObjectsToScan(ScanCountOfObjectsToScan_Handler);
            oProcedureScanner.ScanDeleted += new ScanAllProcedures.ScanObjectDeleted(ScanObjectDeleted_Handler);
            oProcedureScanner.ScanNotFound -= new ScanAllProcedures.ScanObjectNotFound(ScanObjectNotFound_Handler);
            oProcedureScanner.ScanChanged -= new ScanAllProcedures.ScanObjectChanged(ScanObjectChanged_Handler);
            oProcedureScanner.ScanBegin -= new ScanAllProcedures.ScanObjectBegin(ScanObjectBegin_Handler);
            ScanObjectTypeEnd(SQLAutoDocLib.UTIL.Constants.PROCEDURENODE, DateTime.Now);
            if (oProcedureScanner.ChangesFound) ChangesFound = true;

            //scan all tables
            ScanObjectTypeBegin(SQLAutoDocLib.UTIL.Constants.TABLENODE, DateTime.Now);
            ScanAllTables oTableScanner = new ScanAllTables(m_DBID, oVersion.VersionID);
            oTableScanner.ScanBegin += new ScanAllTables.ScanObjectBegin(ScanObjectBegin_Handler);
            oTableScanner.ScanChanged += new ScanAllTables.ScanObjectChanged(ScanObjectChanged_Handler);
            oTableScanner.ScanNotFound += new ScanAllTables.ScanObjectNotFound(ScanObjectNotFound_Handler);
            oTableScanner.ScanDeleted += new ScanAllTables.ScanObjectDeleted(ScanObjectDeleted_Handler);
            oTableScanner.ScanCount += new ScanAllFunctions.ScanCountOfObjectsToScan(ScanCountOfObjectsToScan_Handler);
            oTableScanner.Scan(m_ConnectionString);
            oTableScanner.ScanCount += new ScanAllFunctions.ScanCountOfObjectsToScan(ScanCountOfObjectsToScan_Handler);
            oTableScanner.ScanDeleted -= new ScanAllTables.ScanObjectDeleted(ScanObjectDeleted_Handler);
            oTableScanner.ScanNotFound -= new ScanAllTables.ScanObjectNotFound(ScanObjectNotFound_Handler);
            oTableScanner.ScanChanged -= new ScanAllTables.ScanObjectChanged(ScanObjectChanged_Handler);
            oTableScanner.ScanBegin -= new ScanAllTables.ScanObjectBegin(ScanObjectBegin_Handler);
            ScanObjectTypeEnd(SQLAutoDocLib.UTIL.Constants.TABLENODE, DateTime.Now);
            if (oTableScanner.ChangesFound) ChangesFound = true;

            //scan all triggers
            ScanObjectTypeBegin(SQLAutoDocLib.UTIL.Constants.TRIGGERNODE, DateTime.Now);
            ScanAllTriggers oTriggerScanner = new ScanAllTriggers(m_DBID, oVersion.VersionID);
            oTriggerScanner.ScanBegin += new ScanAllTriggers.ScanObjectBegin(ScanObjectBegin_Handler);
            oTriggerScanner.ScanChanged += new ScanAllTriggers.ScanObjectChanged(ScanObjectChanged_Handler);
            oTriggerScanner.ScanNotFound += new ScanAllTriggers.ScanObjectNotFound(ScanObjectNotFound_Handler);
            oTriggerScanner.ScanDeleted += new ScanAllTriggers.ScanObjectDeleted(ScanObjectDeleted_Handler);
            oTriggerScanner.ScanCount += new ScanAllFunctions.ScanCountOfObjectsToScan(ScanCountOfObjectsToScan_Handler);
            oTriggerScanner.Scan(m_ConnectionString);
            oTriggerScanner.ScanCount += new ScanAllFunctions.ScanCountOfObjectsToScan(ScanCountOfObjectsToScan_Handler);
            oTriggerScanner.ScanDeleted += new ScanAllTriggers.ScanObjectDeleted(ScanObjectDeleted_Handler);
            oTriggerScanner.ScanNotFound -= new ScanAllTriggers.ScanObjectNotFound(ScanObjectNotFound_Handler);
            oTriggerScanner.ScanChanged -= new ScanAllTriggers.ScanObjectChanged(ScanObjectChanged_Handler);
            oTriggerScanner.ScanBegin -= new ScanAllTriggers.ScanObjectBegin(ScanObjectBegin_Handler);
            ScanObjectTypeEnd(SQLAutoDocLib.UTIL.Constants.TRIGGERNODE, DateTime.Now);
            if (oTriggerScanner.ChangesFound) ChangesFound = true;

            //scan all views
            ScanObjectTypeBegin(SQLAutoDocLib.UTIL.Constants.VIEWNODE, DateTime.Now);
            ScanAllViews oViewScanner = new ScanAllViews(m_DBID, oVersion.VersionID);
            oViewScanner.ScanBegin += new ScanAllViews.ScanObjectBegin(ScanObjectBegin_Handler);
            oViewScanner.ScanChanged += new ScanAllViews.ScanObjectChanged(ScanObjectChanged_Handler);
            oViewScanner.ScanNotFound += new ScanAllViews.ScanObjectNotFound(ScanObjectNotFound_Handler);
            oViewScanner.ScanDeleted += new ScanAllViews.ScanObjectDeleted(ScanObjectDeleted_Handler);
            oViewScanner.ScanCount += new ScanAllFunctions.ScanCountOfObjectsToScan(ScanCountOfObjectsToScan_Handler);
            oViewScanner.Scan(m_ConnectionString);
            oViewScanner.ScanCount += new ScanAllFunctions.ScanCountOfObjectsToScan(ScanCountOfObjectsToScan_Handler);
            oViewScanner.ScanDeleted += new ScanAllViews.ScanObjectDeleted(ScanObjectDeleted_Handler);
            oViewScanner.ScanNotFound -= new ScanAllViews.ScanObjectNotFound(ScanObjectNotFound_Handler);
            oViewScanner.ScanChanged -= new ScanAllViews.ScanObjectChanged(ScanObjectChanged_Handler);
            oViewScanner.ScanBegin -= new ScanAllViews.ScanObjectBegin(ScanObjectBegin_Handler);
            ScanObjectTypeEnd(SQLAutoDocLib.UTIL.Constants.VIEWNODE, DateTime.Now);
            if (oViewScanner.ChangesFound) ChangesFound = true;

            //Scan complete
            oVersion.ScanComplete = DateTime.Now.ToString();
            oVersion.ChangesFound = ChangesFound;
            oVersion.Save();

            ScanDatabaseEnd(DateTime.Now);
        }

        public void ScanObjectTypeBeginEvent_Handler(string type, DateTime e)
        {
            ScanObjectTypeBegin.Invoke(type: type, e: e);
        }

        public void ScanObjectTypeEndEvent_Handler(string type, DateTime e)
        {
            ScanObjectTypeEnd(type: type, e: e);
        }

        public void ScanObjectBegin_Handler(string type,string name)
        {
            ScanObjectBegin(type, name, DateTime.Now);
        }

        public void ScanCountOfObjectsToScan_Handler(string type, int count)
        {
            ScanObjectCount(type, count);
        }

        public void ScanObjectChanged_Handler(string type,string name)
        {
            ScanObjectChanged(type, name, DateTime.Now);
        }

        public void ScanObjectNotFound_Handler(string type, string name)
        {
            ScanObjectNotFound(type, name, DateTime.Now);
        }

        public void ScanObjectDeleted_Handler(string type, string name)
        {
            ScanObjectDeleted(type, name, DateTime.Now);
        }
    }
}
