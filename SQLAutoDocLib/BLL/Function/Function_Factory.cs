using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SQLAutoDocLib.BLL
{
    public class Function_Factory : shared.BLLFactoryBase
    {
        private DBL.DBFunction_Factory m_DBFactory = new DBL.DBFunction_Factory();

        public List<SQLAutoDocLib.BLL.Function> ListAllFunctionsInDatabase(Guid DBID)
        {
            List<SQLAutoDocLib.BLL.Function> oList = new List<SQLAutoDocLib.BLL.Function>();

            DataTable oIDs = m_DBFactory.AllFunctions(DBID: DBID);
            foreach (DataRow oID in oIDs.Rows)
            {
                BLL.Function oFunction = new BLL.Function(DBID: DBID, FunctionID: (Guid)oID["FunctionID"]);
                oFunction.Load();

                oList.Add(oFunction);
            }
            return oList;
        }

        public List<SQLAutoDocLib.BLL.Function> ListAllFunctionsInDatabase(Guid DBID, string VersionID, bool ChangedOnly)
        {
            List<SQLAutoDocLib.BLL.Function> oList = new List<SQLAutoDocLib.BLL.Function>();

            DataTable oIDs = m_DBFactory.AllFunctions(DBID: DBID, VersionID: VersionID, ChangedOnly: ChangedOnly);
            foreach (DataRow oID in oIDs.Rows)
            {
                BLL.Function oFunction = new BLL.Function(DBID: DBID, FunctionID: (Guid)oID["FunctionID"]);
                oFunction.Load();

                oList.Add(oFunction);
            }
            return oList;
        }

        public Function GetFunctionByName(Guid DBID, string FunctionName)
        {
            Function oReturn = null;

            DataTable oFunctionID = m_DBFactory.GetFunctionByName(DBID, FunctionName);
            oReturn = new Function(DBID: DBID, FunctionID: (Guid)oFunctionID.Rows[0]["FunctionID"]);
            oReturn.Load();

            return oReturn;
        }

        public Function GetFunctionByName(Guid DBID, string FunctionName, string VersionID)
        {
            Function oReturn = null;

            DataTable oFunctionID = m_DBFactory.GetFunctionByName(DBID, FunctionName,VersionID);
            oReturn = new Function(DBID: DBID, FunctionID: (Guid)oFunctionID.Rows[0]["FunctionID"]);
            oReturn.Load();

            return oReturn;
        }

        public string Reconstitute(BLL.Function oFunction)
        {
            SCAN.AGENT.DBFunctionAgent_Factory oFactory = new SCAN.AGENT.DBFunctionAgent_Factory();
            return oFactory.Reconstitute(oFunction);
        }
    }
}
