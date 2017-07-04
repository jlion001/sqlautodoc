using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SQLAutoDocLib.BLL
{
    public class Table_Factory : shared.BLLFactoryBase
    {
        private DBL.DBTable_Factory m_DBFactory = new DBL.DBTable_Factory();

        public List<SQLAutoDocLib.BLL.Table> ListAllTablesInDatabase(Guid DBID)
        {
            List<SQLAutoDocLib.BLL.Table> oList = new List<SQLAutoDocLib.BLL.Table>();

            DataTable oIDs = m_DBFactory.AllTables(DBID: DBID);
            foreach (DataRow oID in oIDs.Rows)
            {
                BLL.Table oTable= new BLL.Table(DBID: DBID, TableID: (Guid)oID["TableID"]);
                oTable.Load();

                oList.Add(oTable);
            }
            return oList;
        }

        public List<SQLAutoDocLib.BLL.Table> ListAllTablesInDatabase(Guid DBID,string VersionID,bool ChangedOnly)
        {
            List<SQLAutoDocLib.BLL.Table> oList = new List<SQLAutoDocLib.BLL.Table>();

            DataTable oIDs = m_DBFactory.AllTables(DBID: DBID, VersionID: VersionID, ChangedOnly: ChangedOnly);
            foreach (DataRow oID in oIDs.Rows)
            {
                BLL.Table oTable = new BLL.Table(DBID: DBID, TableID: (Guid)oID["TableID"]);
                oTable.LoadFromRow(oID);

                oList.Add(oTable);
            }
            return oList;
        }

        public Table GetTableByName(Guid DBID,string TableName)
        {
            Table oReturn = null;

            DataTable oTableID=m_DBFactory.GetTableByName(DBID,TableName);
            oReturn = new Table(DBID: DBID, TableID: (Guid)oTableID.Rows[0]["TableID"]);
            oReturn.Load();

            return oReturn;
        }

        public Table GetTableByName(Guid DBID, string TableName, string VersionID)
        {
            Table oReturn = null;

            DataTable oTableID = m_DBFactory.GetTableByName(DBID:DBID, TableName: TableName, VersionID: VersionID);
            oReturn = new Table(DBID: DBID, TableID: (Guid)oTableID.Rows[0]["TableID"]);
            oReturn.Load();

            return oReturn;
        }

        public string Reconstitute(BLL.Table oTable)
        {
            SCAN.AGENT.DBTableAgent_Factory oFactory = new SCAN.AGENT.DBTableAgent_Factory();
            return oFactory.Reconstitute(oTable);
        }
    }
}
