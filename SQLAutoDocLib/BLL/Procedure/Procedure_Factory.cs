using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SQLAutoDocLib.BLL
{
    public class Procedure_Factory : shared.BLLFactoryBase
    {
        private DBL.DBProcedure_Factory m_DBFactory = new DBL.DBProcedure_Factory();

        public List<SQLAutoDocLib.BLL.Procedure> ListAllProceduresInDatabase(Guid DBID)
        {
            List<SQLAutoDocLib.BLL.Procedure> oList = new List<SQLAutoDocLib.BLL.Procedure>();

            DataTable oIDs = m_DBFactory.AllProcedures(DBID: DBID);
            foreach (DataRow oID in oIDs.Rows)
            {
                BLL.Procedure oProcedure = new BLL.Procedure(DBID: DBID, ProcedureID: (Guid)oID["ProcedureID"]);
                oProcedure.Load();

                oList.Add(oProcedure);
            }
            return oList;
        }

        public List<SQLAutoDocLib.BLL.Procedure> ListAllProceduresInDatabase(Guid DBID, string VersionID, bool ChangedOnly)
        {
            List<SQLAutoDocLib.BLL.Procedure> oList = new List<SQLAutoDocLib.BLL.Procedure>();

            DataTable oIDs = m_DBFactory.AllProcedures(DBID: DBID, VersionID: VersionID, ChangedOnly: ChangedOnly);
            foreach (DataRow oID in oIDs.Rows)
            {
                BLL.Procedure oProcedure = new BLL.Procedure(DBID: DBID, ProcedureID: (Guid)oID["ProcedureID"]);
                oProcedure.LoadFromRow(oID);

                oList.Add(oProcedure);
            }
            return oList;
        }

        public Procedure GetProcedureByName(Guid DBID, string ProcedureName)
        {
            Procedure oReturn = null;

            DataTable oProcedureID = m_DBFactory.GetProcedureByName(DBID, ProcedureName);
            oReturn = new Procedure(DBID: DBID, ProcedureID: (Guid)oProcedureID.Rows[0]["ProcedureID"]);
            oReturn.Load();

            return oReturn;
        }

        public Procedure GetProcedureByName(Guid DBID, string ProcedureName,string VersionID)
        {
            Procedure oReturn = null;

            DataTable oProcedureID = m_DBFactory.GetProcedureByName(DBID, ProcedureName,VersionID:VersionID);
            oReturn = new Procedure(DBID: DBID, ProcedureID: (Guid)oProcedureID.Rows[0]["ProcedureID"]);
            oReturn.Load();

            return oReturn;
        }

        public string Reconstitute(BLL.Procedure oProcedure)
        {
            SCAN.AGENT.DBProcedureAgent_Factory oFactory = new SCAN.AGENT.DBProcedureAgent_Factory();
            return oFactory.Reconstitute(oProcedure);
        }
    }
}
